using UnityEngine;
using System.Collections;

public class ShadowOverlay : MonoBehaviour {

    private GameObject[] shadow_boxes;

    public GameObject shadow_box_prefab;

    private LeftEdge left_edge;
    private RightEdge right_edge;

    private float box_size;


	void Start () {

        box_size = shadow_box_prefab.GetComponent<SpriteRenderer>().sprite.texture.width / shadow_box_prefab.GetComponent<SpriteRenderer>().sprite.pixelsPerUnit;

        left_edge = GameObject.FindObjectOfType<LeftEdge>();
        right_edge = GameObject.FindObjectOfType<RightEdge>();

        shadow_boxes = new GameObject[10];

        for (int i = 0; i < shadow_boxes.Length; i++) {
            shadow_boxes[i] = (GameObject)Instantiate(shadow_box_prefab);
            shadow_boxes[i].transform.position = new Vector2(left_edge.transform.position.x + i * box_size, 0);
            print("Created at: " + shadow_boxes[i].transform.position);
        }

	}
	
	void Update () {
	
	}
}
