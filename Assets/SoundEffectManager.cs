using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoundEffect {
    basic_click
}

public class SoundEffectManager : MonoBehaviour {

    public AudioSource basic_click;

    void Start() {

    }

    public void PlaySound(SoundEffect sound_effect) {
        switch (sound_effect) {
            case SoundEffect.basic_click:
                basic_click.Play();
                break;
            default:
                break;
        }
    }

}
