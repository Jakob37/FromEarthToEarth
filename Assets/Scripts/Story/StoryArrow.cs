using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryArrow : MonoBehaviour {

    private bool iterated_through;
    private Image arrow_image;
    private bool hidden;

    void Start() {
        iterated_through = false;
        arrow_image = GetComponent<Image>();
    }

    public void SignalIteratedThrough() {
        iterated_through = true;
    }

    public void Reset(bool hidden=false) {
        iterated_through = false;
        this.hidden = hidden;
    }

    void Update() {

        if (hidden) {
            arrow_image.color = new Color(0, 0, 0, 0);
        }
        else if (iterated_through) {
            arrow_image.color = new Color(0.5f, 0.5f, 0.5f);
        }
        else {
            arrow_image.color = new Color(0, 0.5f, 0);
        }
    }
}
