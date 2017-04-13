using UnityEngine;
using System.Collections;

public class LeverPole : MonoBehaviour {

    public Vector3 orig_pos;
    public Vector3 OrigPos { get { return orig_pos; } }

    private Vector3 orig_scale;

    private float scale_factor = 4f;
    private float current_scale_factor;

    public float y_shift;

	void Start () {
        orig_pos = gameObject.transform.localPosition;
        orig_scale = transform.localScale;
        current_scale_factor = 1;
	}

    void Update() {
        transform.localScale = new Vector2(orig_scale.x, current_scale_factor);
        y_shift = (transform.localScale.y - orig_scale.y) / (scale_factor - 1);
        transform.localPosition = orig_pos + new Vector3(0, y_shift);
    }

    public void AssignCurrentScale(float current_scale) {
        current_scale_factor = current_scale * orig_scale.y;
    }
}
