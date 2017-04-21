using UnityEngine;
using System.Collections;

public class RayTest : MonoBehaviour {

    public LayerMask hit_mask;

    private Transform target;

	void Start () {
        target = GameObject.FindObjectOfType<Player>().gameObject.transform;
        print("Firing ray!");
        // Partly based on: http://answers.unity3d.com/questions/840614/linecast-detecting-collision-from-target.html
    }
	
	void Update () {

        Debug.DrawLine(new Vector3(-5, 0, 0), new Vector3(1, 0, 0), Color.red, 2, false);

        if (Input.GetKeyDown(KeyCode.L)) {
            DoLinecast();
        }
	}

    private void DoLinecast(bool debug_line=true) {

        RaycastHit2D hit;

        if (debug_line) {
            Debug.DrawLine(target.position, transform.position, Color.yellow, 2, false);
        }

        hit = Physics2D.Linecast(transform.position, target.position, hit_mask);

        print(hit.transform.gameObject);
        print(hit.point);
    }
}
