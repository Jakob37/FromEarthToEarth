using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickSound : MonoBehaviour {

    private Button button { get { return GetComponent<Button>(); } }

    void Start() {

        button.onClick.AddListener(() => PlaySound());
    }

    void PlaySound() {

        var sound_manager = FindObjectOfType<SoundEffectManager>();
        sound_manager.PlaySound(SoundEffect.basic_click);
    }
}
