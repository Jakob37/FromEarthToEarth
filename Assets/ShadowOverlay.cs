using UnityEngine;
using System.Collections;
using Tiled2Unity;

public class ShadowOverlay : MonoBehaviour {

    private GameObject[,] shadow_boxes;

    public GameObject shadow_box_prefab;
    public GameObject level_tiles_object;

    private LeftEdge left_edge;
    private RightEdge right_edge;

    private float box_size;

    private Vector2 Origin {
        get {
            return new Vector2(level_tiles_object.transform.position.x, -4);
        }
    }

    private Vector2 GetLevelSizeInPixels() {
        TiledMap tiled_map = GameObject.FindObjectOfType<TiledMap>();
        return new Vector2(tiled_map.MapWidthInPixels, tiled_map.MapHeightInPixels);
    }

    private float GetTileSizeInPixels() {
        TiledMap tiled_map = GameObject.FindObjectOfType<TiledMap>();
        return tiled_map.TileWidth;
    }

    private Vector2 GetTilesDimensions() {
        Vector2 level_size_pixels = GetLevelSizeInPixels();
        float tile_size_pixels = GetTileSizeInPixels();
        return new Vector2(level_size_pixels.x / tile_size_pixels, level_size_pixels.y / tile_size_pixels);
    }

	void Start () {

        GameObject template_object = (GameObject)Instantiate(shadow_box_prefab);
        SpriteRenderer box_renderer = template_object.GetComponent<SpriteRenderer>();
        box_size = box_renderer.sprite.rect.width / box_renderer.sprite.pixelsPerUnit * box_renderer.transform.lossyScale.x;

        left_edge = GameObject.FindObjectOfType<LeftEdge>();
        right_edge = GameObject.FindObjectOfType<RightEdge>();

        Vector2 level_size_pixels = GetLevelSizeInPixels();
        float tile_size_pixels = GetTileSizeInPixels();
        Vector2 tiles_dim = GetTilesDimensions();

        shadow_boxes = new GameObject[(int)tiles_dim.x, (int)tiles_dim.y];

        for (int x = 0; x < shadow_boxes.GetLength(0); x++) {
            for (int y = 0; y < shadow_boxes.GetLength(1); y++) {
                shadow_boxes[x,y] = (GameObject)Instantiate(shadow_box_prefab);

                float x_pos = Origin.x + box_size / 2 + x * box_size;
                float y_pos = Origin.y + box_size / 2 + y * box_size;

                shadow_boxes[x,y].transform.position = new Vector2(x_pos, y_pos);

                // TEST ASSIGNING TRANSPARENCY
                if (x < 10) {
                    shadow_boxes[x, y].GetComponent<ShadowBox>().AssignTransparency(0.1f);
                }
            }
        }
	}
	
	void Update () {
	
	}
}
