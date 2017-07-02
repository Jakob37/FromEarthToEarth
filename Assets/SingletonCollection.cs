using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonCollection : MonoBehaviour {

    public static SingletonCollection instance;
    public static SingletonCollection Instance {
        get { return instance; }
    }

    void Awake() {
        if (instance != null && instance != this) {
            Destroy(this.gameObject);
        }
        else {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }
}
