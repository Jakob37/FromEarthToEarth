using UnityEngine;
using System.Collections;

public class LeverPole : MonoBehaviour {

    // private Vector2 local_sprite_size;
    // public Vector2 LocalSpriteSize { get { return local_sprite_size; } }

    public Vector3 orig_pos;
    public Vector3 OrigPos { get { return orig_pos; } }

    private Vector3 orig_scale;

    private float scale_factor = 4f;

    public float y_shift;

	void Start () {
        // local_sprite_size = Lever.CalculateLocalSpriteSize(GetComponent<SpriteRenderer>());
        orig_pos = gameObject.transform.localPosition;
        orig_scale = transform.localScale;
	}

    void Update() {
        //transform.localPosition = orig_pos + new Vector3(0, (transform.localScale.y - orig_scale.y) / 2);

        y_shift = (transform.localScale.y - orig_scale.y) / (scale_factor - 1);
        transform.localPosition = orig_pos + new Vector3(0, y_shift);
    }
}
