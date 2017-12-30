using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpecialFlower : MonoBehaviour {

    public int flower_number;

    private bool is_alive;
    private SoundEffectManager sound_manager;

    void Start() {

        bool is_flower_picked = SaveManager.GetFlowerPicked(SceneManager.GetActiveScene().buildIndex, flower_number);
        if (is_flower_picked) {
            GameObject.Destroy(gameObject);
        }
        else {
            is_alive = true;
            sound_manager = FindObjectOfType<SoundEffectManager>();
        }
    }

    void OnTriggerEnter2D(Collider2D coll) {

        if (coll.gameObject.GetComponent<Player>() != null && is_alive) {
            PickFlower();
        }
    }

    private void PickFlower() {
        sound_manager.PlaySound(SoundEffect.pickup_flower);
        SaveManager.SpecialFlowerPick(SceneManager.GetActiveScene().buildIndex, flower_number);
        is_alive = false;
        GameObject.Destroy(gameObject);
    }
}
