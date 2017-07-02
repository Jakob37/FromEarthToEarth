using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Song {
    vivid_skies,
    steady_rain
}

public class MusicManager : MonoBehaviour {

    public static MusicManager instance;
    public static MusicManager Instance {
        get { return instance; }
    }

    public AudioSource vivid_skies;
    public AudioSource steady_rain;

    private List<Song> played_songs;

    private AudioSource current_song;

    void Awake() {
        // if (instance != null && instance != this) {
        //     Destroy(this.gameObject);
        // }
        // else {
        //     print("new instance");
        //     instance = this;
        // }
        // DontDestroyOnLoad(transform.root.gameObject);
    }

    void Start() {
        current_song = null;
        played_songs = new List<Song>();
    }

    public void TrigNewLevel() {

        if (current_song == null) {
            current_song = vivid_skies;
            current_song.Play();
            played_songs.Add(Song.vivid_skies);
        }
    }

    void Update() {

        if (current_song != null && !current_song.isPlaying) {
            current_song = null;
        }

    }
}
