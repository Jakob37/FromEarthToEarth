using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelLogic : MonoBehaviour {


    public int current_level;

    public void WinCondition() {

        print("Loading scene");
        SceneManager.LoadScene(current_level + 1);
    }
}
