using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpecialFlower : MonoBehaviour {

    public int flower_number;

    private bool is_alive;
    private SoundEffectManager sound_manager;
    private FlowerPickupDisplay flower_pickup_display;

    void Awake() {

        sound_manager = FindObjectOfType<SoundEffectManager>();
        flower_pickup_display = FindObjectOfType<FlowerPickupDisplay>();
    }

    void Start() {

        bool is_flower_picked = SaveManager.GetFlowerPicked(SceneManager.GetActiveScene().buildIndex, flower_number);
        if (is_flower_picked) {
            GameObject.Destroy(gameObject);
        }
        else {
            is_alive = true;
        }
    }

    void OnTriggerEnter2D(Collider2D coll) {

        if (coll.gameObject.GetComponent<Player>() != null && is_alive) {
            PickFlower();
        }
    }

    private void PickFlower() {

        int level_index = SceneManager.GetActiveScene().buildIndex;

        sound_manager.PlaySound(SoundEffect.pickup_flower);
        SaveManager.SpecialFlowerPick(level_index, flower_number);
        is_alive = false;
        GameObject.Destroy(gameObject);

        int picked_flowers = SaveManager.GetFlowersPickedOnLevel(level_index);

        flower_pickup_display.ShowFlowerCount(picked_flowers);
    }
}
