using UnityEngine;
using System.Collections;

public class ShadowOverlay : MonoBehaviour {

    private GameObject[] shadow_boxes;

    public GameObject shadow_box_prefab;
    public GameObject level_tiles_object;

    private LeftEdge left_edge;
    private RightEdge right_edge;

    private float box_size;

    private Vector2 Origin {
        get {
            return new Vector2(level_tiles_object.transform.position.x, 0);
            // return level_tiles_object.transform.position;
        }
    }

	void Start () {

        SpriteRenderer box_renderer = shadow_box_prefab.GetComponent<SpriteRenderer>();

        // box_size = box_renderer.sprite.texture.width * shadow_box_prefab.transform.localScale.x / box_renderer.sprite.pixelsPerUnit;
        //box_size = shadow_box_prefab.GetComponent<SpriteRenderer>().sprite.texture.width / shadow_box_prefab.GetComponent<SpriteRenderer>().sprite.pixelsPerUnit;

        ShadowBox shadow_box_script = box_renderer.gameObject.GetComponent<ShadowBox>();

        box_size = box_renderer.gameObject.GetComponent<ShadowBox>().Size;
        box_size = 32f / 100f;

        left_edge = GameObject.FindObjectOfType<LeftEdge>();
        right_edge = GameObject.FindObjectOfType<RightEdge>();

        shadow_boxes = new GameObject[10];

        for (int i = 0; i < shadow_boxes.Length; i++) {
            shadow_boxes[i] = (GameObject)Instantiate(shadow_box_prefab);

            // float box_size = 10;

            // shadow_boxes[i].transform.position = new Vector2(left_edge.transform.position.x + i * box_size, 0);
            shadow_boxes[i].transform.position = new Vector2(Origin.x + i * box_size, 0);
            print(shadow_boxes[i].transform.position);
            print("Created at: " + shadow_boxes[i].transform.position);
        }

	}
	
	void Update () {
	
	}
}
