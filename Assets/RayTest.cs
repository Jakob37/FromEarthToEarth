using UnityEngine;
using System.Collections;

public class RayTest : MonoBehaviour {

    public LayerMask hit_mask;

    private Transform target;

	void Start () {
        target = GameObject.FindObjectOfType<RayTarget>().gameObject.transform;

        print("Firing ray!");

        // Partly based on: http://answers.unity3d.com/questions/840614/linecast-detecting-collision-from-target.html

        RaycastHit hit;
        //var hit : Raycasthit;

        var output = Physics.Linecast(transform.position, target.position, out hit, hit_mask);

        print(output);
        print(hit.point);

    }
	
	void Update () {
	
	}
}
