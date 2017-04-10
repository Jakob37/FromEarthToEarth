using UnityEngine;
using System.Collections;

public class Lever : MonoBehaviour {

    public Switch my_switch;

    private SpriteRenderer sprite_renderer;

    private Vector3 orig_pos;
    // private float initial_length;
    private float current_scale_factor;
    private float scale_factor = 4;
    private float SCALE_OFFSET_FACTOR = 1.25f;  // TODO: Calculate from real values

    public float lever_speed = 0.1f;

    private LeverPole pole;
    private LeverPlatform platform;

    private GameObject pole_object;
    private GameObject platform_object;

    private Vector2 local_sprite_size;

    private float GetHeight() {
        return sprite_renderer.sprite.rect.size.y;
    }

    public static Vector2 CalculateLocalSpriteSize(SpriteRenderer sprite_renderer) {
        Vector2 sprite_size = sprite_renderer.sprite.rect.size;
        Vector2 local_sprite_size = sprite_size / sprite_renderer.sprite.pixelsPerUnit;
        //Vector3 world_size = local_sprite_size;
        return local_sprite_size;
    }

    void Awake() {
        orig_pos = gameObject.transform.localPosition;

        // TODO: Move this essential logic to some kind of globally accessible utility class

        local_sprite_size = Lever.CalculateLocalSpriteSize(GetComponent<SpriteRenderer>());

        pole = GetComponentInChildren<LeverPole>();
        platform = GetComponentInChildren<LeverPlatform>();
        pole_object = pole.gameObject;
        platform_object = platform.gameObject;
        pole_object.transform.rotation = gameObject.transform.rotation;
        platform_object.transform.rotation = gameObject.transform.rotation;
    }

    void Start () {
        sprite_renderer = gameObject.GetComponent<SpriteRenderer>();
        current_scale_factor = 1;
	}
	
	void Update () {

        if (my_switch.IsPressed) {

            if (current_scale_factor + lever_speed < scale_factor) {
                current_scale_factor += lever_speed;
            }
            else {
                current_scale_factor = scale_factor;
            }
        }
        else {

            if (current_scale_factor - lever_speed > 1) {
                current_scale_factor -= lever_speed;
            }
            else {
                current_scale_factor = 1;
            }
        }

        pole_object.transform.localScale = new Vector2(1, current_scale_factor);
        //gameObject.transform.localScale = new Vector2(1, current_scale_factor);

        //float sprite_frac = 1 + local_sprite_size.y / 2;
        //var effective_scaling = (current_scale_factor - 1) / scale_factor;
        //gameObject.transform.localPosition = orig_pos + (transform.up * sprite_frac) * effective_scaling;

        float sprite_frac = 1 + pole.LocalSpriteSize.y / 2;
        var effective_scaling = (current_scale_factor - 1) / scale_factor;
        pole_object.transform.localPosition = pole.OrigPos + (gameObject.transform.up * sprite_frac) * effective_scaling;

        float platform_pos_scale = 2 + pole.LocalSpriteSize.y / 2;

        print("up vector: " + platform_object.transform.localRotation * platform_object.transform.up + " position: " + platform_object.transform.localRotation * platform_object.transform.position);
        // print("platform_pos_scale: " + platform_pos_scale);
        //print("transform: " + (gameObject.transform.up * platform_pos_scale) * effective_scaling);



        //platform_object.transform.localPosition = platform.OrigPos + platform_object.transform.forward;
        platform_object.transform.localPosition = platform.OrigPos + (gameObject.transform.up * platform_pos_scale) * effective_scaling;
    }
}
