using UnityEngine;
using System.Collections;
using Tiled2Unity;

public class ShadowBox : MonoBehaviour {

    private SpriteRenderer rend;

    private float tile_size;
    private float scale_factor;
    public float Size {
        get {
            return tile_size * scale_factor;
        }
    }

    void Awake() {
        rend = GetComponent<SpriteRenderer>();
        TiledMap tiled_map = GameObject.FindObjectOfType<TiledMap>();
        tile_size = tiled_map.TileWidth;
        scale_factor = rend.sprite.pixelsPerUnit;
        rend.transform.localScale = new Vector2(64/scale_factor, 64/scale_factor);
    }

    public void AssignTransparency(float alpha) {
        Color curr_color = rend.color;
        rend.color = new Color(curr_color.r, curr_color.g, curr_color.b, alpha);
    }

	void Start () {

	}
	
	void Update () {
	    
	}
}
