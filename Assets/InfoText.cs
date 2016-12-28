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

        string mon_text = "";

        mon_text += GetMonText("Jump", player.is_jumping.ToString());
        mon_text += GetMonText("Grounded", player.is_grounded.ToString());
        mon_text += GetMonText("Jumps", player.remaining_jumps.ToString());

        text_object.text = mon_text;
	}

    private string GetMonText(string descr, string text) {
        return string.Format("{0}: {1}\n", descr, text);
    }
}
