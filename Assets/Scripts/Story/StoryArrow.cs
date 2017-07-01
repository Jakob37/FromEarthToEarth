using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryArrow : MonoBehaviour {

    private bool iterated_through;
    private Image arrow_image;

    void Start() {
        iterated_through = false;
        arrow_image = GetComponent<Image>();
    }

    public void SignalIteratedThrough() {
        iterated_through = true;
    }

    public void Reset() {
        iterated_through = false;
    }

    void Update() {
        if (iterated_through) {
            arrow_image.color = new Color(0.5f, 0.5f, 0.5f);
        }
        else {
            arrow_image.color = new Color(0, 0.5f, 0);
        }
    }

}
