using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stranger : MonoBehaviour {

    private enum GoalStatus {
        Walking,
        AwaitingNewGoal,
        AwaitingNextWalk
    }

    private GoalStatus current_goal_status;

    private float left_bound;
    private float right_bound;

    private Animator anim;

    private float min_paus_time = 1;
    private float max_paus_time = 5;
    private float remain_paus_time;
    private float walk_speed = 0.01f;

    private Vector3 velocity;
    private bool facing_right;

    private StoryBoard story_board;

    private float goal_x;
    private bool is_forward_right;

    private bool IsWalking { get { return !IsGoalReached(); } }
    private float XPos { get { return transform.position.x; } }

    void Awake() {
        left_bound = GetComponentInChildren<LeftBound>().gameObject.transform.position.x;
        right_bound = GetComponentInChildren<RightBound>().gameObject.transform.position.x;
        story_board = GetComponent<StoryBoard>();
        anim = GetComponent<Animator>();
    }

    void Start() {

        remain_paus_time = 0;
        goal_x = GetNewGoalPosition();
        UpdateFlip();
        current_goal_status = GoalStatus.Walking;
    }

    void Update() {

        bool goal_reached = IsGoalReached();

        if (current_goal_status == GoalStatus.Walking && goal_reached) {
            current_goal_status = GoalStatus.AwaitingNewGoal;
        }

        if (story_board.CurrentActive) {
            current_goal_status = GoalStatus.AwaitingNextWalk;
            StopMovement();
        }
        else if (current_goal_status == GoalStatus.Walking) {
            WalkTowardsGoalPosition();
        }
        else if (current_goal_status == GoalStatus.AwaitingNewGoal) {
            StopMovement();
            GenerateNewGoalPosition();
            remain_paus_time = Random.Range(min_paus_time, max_paus_time);
            current_goal_status = GoalStatus.AwaitingNextWalk;
        }
        else if (current_goal_status == GoalStatus.AwaitingNextWalk && remain_paus_time > 0) {
            remain_paus_time -= Time.deltaTime;
        }
        else if (current_goal_status == GoalStatus.AwaitingNextWalk && remain_paus_time <= 0) {
            current_goal_status = GoalStatus.Walking;
        }

        SetAnimParams();
        transform.position += velocity;
    }

    private void GenerateNewGoalPosition() {
        goal_x = GetNewGoalPosition();
        is_forward_right = (goal_x > XPos);
        UpdateFlip();
    }

    private void WalkTowardsGoalPosition() {
        
        if (is_forward_right) {
            velocity = new Vector2(walk_speed, 0);
        }
        else {
            velocity = new Vector2(-walk_speed, 0);
        }
    }

    private void StopMovement() {
        velocity = new Vector2(0, 0);
    }

    private bool IsGoalReached() {
        if (is_forward_right && XPos > goal_x) {
            return true;
        }
        else if (!is_forward_right && XPos < goal_x) {
            return true;
        }
        return false;
    }

    private float GetNewGoalPosition() {

        float low_x = left_bound;
        float high_x = right_bound;
        float new_target_x = Random.Range(low_x, high_x);

        return new_target_x;
    }

    private void UpdateFlip() {

        if (is_forward_right && !facing_right) {
            Flip();
        }
        else if (!is_forward_right && facing_right) {
            Flip();
        }
    }

    private void Flip() {

        facing_right = !facing_right;
        Vector3 new_scale = transform.localScale;
        new_scale *= -1;
        transform.localScale = new Vector3(new_scale.x, transform.localScale.y);
    }

    private void SetAnimParams() {
        anim.SetBool("is_walking", velocity != new Vector3(0, 0, 0));
    }
}