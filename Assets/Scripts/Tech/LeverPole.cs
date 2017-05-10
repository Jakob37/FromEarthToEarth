using UnityEngine;
using System.Collections;

public class LeverPole : MonoBehaviour {

    public Vector3 orig_pos;
    public Vector3 OrigPos { get { return orig_pos; } }

    private float current_offset;
    private BoxCollider2D coll;

    private Vector2 orig_coll_size;
    private Vector2 orig_coll_offset;

	void Start () {
        orig_pos = gameObject.transform.localPosition;
        coll = GetComponent<BoxCollider2D>();
        orig_coll_size = coll.size;
        orig_coll_offset = (Vector2)coll.offset;
    }

    void Update() {

        transform.localPosition = orig_pos + new Vector3(0, current_offset);
        coll.size = new Vector2(orig_coll_size.x, orig_coll_size.y + current_offset);
        coll.offset = new Vector2(0, orig_coll_offset.y - current_offset / 2);
    }

    public void AssignOffset(float offset) {
        current_offset = offset;
    }
}
