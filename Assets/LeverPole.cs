using UnityEngine;
using System.Collections;

public class LeverPole : MonoBehaviour {

    private Vector2 local_sprite_size;
    public Vector2 LocalSpriteSize { get { return local_sprite_size; } }

    public Vector3 orig_pos;
    public Vector3 OrigPos { get { return orig_pos; } }

	void Start () {
        local_sprite_size = Lever.CalculateLocalSpriteSize(GetComponent<SpriteRenderer>());
        orig_pos = gameObject.transform.localPosition;
	}
}
