using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {

    private Player player;

    private float LEFT_EDGE = -1.644665f;
    private float RIGHT_EDGE = 3f;

    private float screen_width = 8f;

    public Transform left_edge;
    public Transform right_edge;

    void Start () {
        player = FindObjectOfType<Player>();
	}
	
    private Vector3 GetViewportPoint(Vector3 position) {
        return Camera.main.WorldToViewportPoint(position);
    }

	void Update () {

        if (player != null) {
            TrackPlayer();
        }

	}

    private void TrackPlayer() {

        transform.position = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);

        var left_pos = left_edge.position.x;
        var right_pos = right_edge.position.x;

        var newX = Mathf.Clamp(transform.position.x, left_pos + screen_width / 2, right_pos - screen_width / 2);
        // var newX = Mathf.Clamp(transform.position.x, LEFT_EDGE, RIGHT_EDGE);
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }
}
