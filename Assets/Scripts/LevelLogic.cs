using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelLogic : MonoBehaviour {

    // public int current_level;

    public void WinCondition() {

        var current_level = SceneManager.GetActiveScene().buildIndex;
        var new_level = current_level + 1;
        print("Loading scene: " + new_level);
        SceneManager.LoadScene(new_level);
    }
}
