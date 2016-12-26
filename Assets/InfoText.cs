using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InfoText : MonoBehaviour {

    public Player player;

    private Text text_object;

	void Start () {
        text_object = GetComponent<Text>();
	}
	
	void Update () {
        text_object.text = string.Format("Jump: {0}\nGrounded: {1}", player.is_jumping, player.is_grounded);
	}
}
