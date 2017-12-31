using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoundEffect {
    basic_click,
    jump,
    pickup_block,
    make_block,
    throw_block,
    iterate_stranger,
    pickup_flower,
}

public class SoundEffectManager : MonoBehaviour {

    public AudioSource basic_click;
    public AudioSource jump;
    public AudioSource pickup_block;
    public AudioSource make_block;
    public AudioSource throw_block;
    public AudioSource iterate_stranger;
    public AudioSource pickup_flower;

    public void PlaySound(SoundEffect sound_effect) {
        switch (sound_effect) {
            case SoundEffect.basic_click:
                basic_click.Play();
                break;
            case SoundEffect.jump:
                jump.Play();
                break;
            case SoundEffect.pickup_block:
                pickup_block.Play();
                break;
            case SoundEffect.throw_block:
                throw_block.Play();
                break;
            case SoundEffect.iterate_stranger:
                iterate_stranger.Play();
                break;
            case SoundEffect.pickup_flower:
                pickup_flower.Play();
                break;
            default:
                break;
        }
    }

}
