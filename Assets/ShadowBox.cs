using UnityEngine;
using System.Collections;
using Tiled2Unity;

public class ShadowBox : MonoBehaviour {

    private SpriteRenderer renderer;

    private float tile_size;
    private float scale_factor;
    public float Size {
        get {
            return tile_size * scale_factor;
        }
    }

    void Awake() {
        renderer = GetComponent<SpriteRenderer>();
        TiledMap tiled_map = GameObject.FindObjectOfType<TiledMap>();
        tile_size = tiled_map.TileWidth;
        scale_factor = renderer.sprite.pixelsPerUnit;
        renderer.transform.localScale = new Vector2(64/scale_factor, 64/scale_factor);
    }

	void Start () {

	}
	
	void Update () {
	    
	}
}
