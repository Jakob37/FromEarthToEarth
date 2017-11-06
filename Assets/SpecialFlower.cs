using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpecialFlower : MonoBehaviour {

    private bool is_alive;

    void Start() {

        bool is_flower_picked = SaveManager.GetFlowerPicked(SceneManager.GetActiveScene().buildIndex);
        if (is_flower_picked) {
            GameObject.Destroy(gameObject);
        }
        else {
            is_alive = true;
        }
    }

    void OnTriggerEnter2D(Collider2D coll) {

        if (coll.gameObject.GetComponent<Player>() != null && is_alive) {
            SaveManager.SpecialFlowerPick(SceneManager.GetActiveScene().buildIndex);
            is_alive = false;
            GameObject.Destroy(gameObject);
        }
    }
}
