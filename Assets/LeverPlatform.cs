using UnityEngine;
using System.Collections;

public class LeverPlatform : MonoBehaviour {

    private Vector2 local_sprite_size;
    public Vector2 SpriteHeight { get { return local_sprite_size; } }

    public Vector3 orig_pos;
    public Vector3 OrigPos { get { return orig_pos; } }

    private LeverPole lever_pole;
    private Lever lever;

    private Transform lever_pole_transform;

    private float scale_factor = 4f;

    private Vector3 orig_scale;

    void Start() {
        local_sprite_size = Lever.CalculateLocalSpriteSize(GetComponent<SpriteRenderer>());
        orig_pos = gameObject.transform.localPosition;

        orig_scale = transform.localScale;

        // lever = transform.parent.GetComponent<Lever>();
        lever_pole = transform.parent.GetComponentInChildren<LeverPole>();
    }

    void Update() {
        //var y_shift = (transform.localScale.y - orig_scale.y) / (scale_factor - 1);
        var y_shift = lever_pole.y_shift * 2;
        transform.localPosition = orig_pos + new Vector3(0, y_shift);
    }
}
