using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {

    private Player player;

    private float screen_width = 8f;
    private float screen_height = 6f;

    public Transform left_edge;
    public Transform right_edge;
    public Transform top_edge;
    public Transform bottom_edge;

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

        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);

        var left_pos = left_edge.position.x;
        var right_pos = right_edge.position.x;

        var top_pos = top_edge.position.y;
        var bottom_pos = bottom_edge.position.y;

        var newX = Mathf.Clamp(transform.position.x, left_pos + screen_width / 2, right_pos - screen_width / 2);
        var newY = Mathf.Clamp(transform.position.y, bottom_pos + screen_height / 2, top_pos - screen_height / 2);
        transform.position = new Vector3(newX, newY, transform.position.z);
    }
}
