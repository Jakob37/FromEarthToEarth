using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MovementExplorer : MonoBehaviour {

    public enum Adjustment {
        up,
        down,
        none
    }

    public Text gui_text;

    private Player player;
    private Rigidbody2D player_rigi;

    public float mass_delta;
    public float linear_drag_delta;
    public float angular_drag_delta;
    public float gravity_scale_delta;

    public float move_force_delta;
    public float max_speed_delta;
    public float jump_force_delta;
    public int air_jumps_delta;

    private KeyCode mass_key = KeyCode.Z;
    private KeyCode gravity_scale_key = KeyCode.X;
    private KeyCode linear_drag_key = KeyCode.C;
    private KeyCode angular_drag_key = KeyCode.V;

    private KeyCode move_force_key = KeyCode.A;
    private KeyCode max_speed_key = KeyCode.S;
    private KeyCode jump_force_key = KeyCode.D;
    private KeyCode air_jumps_key = KeyCode.F;

	void Start () {
        player = FindObjectOfType<Player>();
        player_rigi = player.gameObject.GetComponent<Rigidbody2D>();

        gui_text.text = "TEST";
	}
	
	void Update () {

        UpdateParametersLogic();

        gui_text.text = GetGUIString();

        var curr_pos = gui_text.transform.position;
        gui_text.transform.position = new Vector3(Camera.main.transform.position.x - 2, curr_pos.y, curr_pos.z);
	}

    private string GetGUIString() {

        var gui_string = "";
        gui_string += string.Format("Mass: {0} (Z)\n", player_rigi.mass.ToString());
        gui_string += string.Format("Gravity scale: {0} (X)\n", player_rigi.gravityScale.ToString());
        gui_string += string.Format("Linear drag: {0} (C)\n", player_rigi.drag.ToString());
        gui_string += string.Format("Angular drag: {0} (V)\n", player_rigi.angularDrag.ToString());

        gui_string += string.Format("Move force: {0} (A)\n", player.move_force.ToString());
        gui_string += string.Format("Max speed: {0} (S)\n", player.max_speed.ToString());
        gui_string += string.Format("Jump force: {0} (D)\n", player.jump_force.ToString());
        gui_string += string.Format("Air jumps: {0} (F)\n", player.air_jumps.ToString());

        return gui_string;
    }

    private void UpdateParametersLogic() {

        Adjustment adj = CheckAdjustment();
        if (adj == Adjustment.none) {
            return;
        }

        if (Input.GetKey(mass_key)) {
            UpdateMass(adj, mass_delta);
        }
        else if (Input.GetKey(gravity_scale_key)) {
            UpdateGravity(adj, gravity_scale_delta);
        }
        else if (Input.GetKey(linear_drag_key)) {
            UpdateLinearDrag(adj, linear_drag_delta);
        }
        else if (Input.GetKey(angular_drag_key)) {
            UpdateAngularDrag(adj, angular_drag_delta);
        }
        else if (Input.GetKey(move_force_key)) {
            UpdateMoveForce(adj, move_force_delta);
        }
        else if (Input.GetKey(jump_force_key)) {
            UpdateJumpForce(adj, jump_force_delta);
        }
        else if (Input.GetKey(max_speed_key)) {
            UpdateMaxSpeed(adj, max_speed_delta);
        }
        else if (Input.GetKey(air_jumps_key)) {
            UpdateAirJumps(adj, air_jumps_delta);
        }
    }

    private Adjustment CheckAdjustment() {
        if (Input.GetKeyDown(KeyCode.UpArrow)) return Adjustment.up;
        else if (Input.GetKeyDown(KeyCode.DownArrow)) return Adjustment.down;
        else return Adjustment.none;
    }

    // Update player movement

    private void UpdateMass(Adjustment adj, float delta) {
        if (adj == Adjustment.up) player_rigi.mass += delta;
        else if (adj == Adjustment.down) player_rigi.mass -= delta;
    }

    private void UpdateGravity(Adjustment adj, float delta) {
        if (adj == Adjustment.up) player_rigi.gravityScale += delta;
        else if (adj == Adjustment.down) player_rigi.gravityScale -= delta;
    }

    private void UpdateLinearDrag(Adjustment adj, float delta) {
        if (adj == Adjustment.up) player_rigi.drag += delta;
        else if (adj == Adjustment.down) player_rigi.drag -= delta;
    }

    private void UpdateAngularDrag(Adjustment adj, float delta) {
        if (adj == Adjustment.up) player_rigi.angularDrag += delta;
        else if (adj == Adjustment.down) player_rigi.angularDrag -= delta;
    }

    private void UpdateMoveForce(Adjustment adj, float delta) {
        if (adj == Adjustment.up) player.move_force += delta;
        else if (adj == Adjustment.down) player.move_force -= delta;
    }

    private void UpdateMaxSpeed(Adjustment adj, float delta) {
        if (adj == Adjustment.up) player.max_speed += delta;
        else if (adj == Adjustment.down) player.max_speed -= delta;
    }

    private void UpdateJumpForce(Adjustment adj, float delta) {
        if (adj == Adjustment.up) player.jump_force += delta;
        else if (adj == Adjustment.down) player.jump_force -= delta;
    }

    private void UpdateAirJumps(Adjustment adj, int delta) {
        if (adj == Adjustment.up) player.air_jumps += delta;
        else if (adj == Adjustment.down) player.air_jumps -= delta;
    }

}
