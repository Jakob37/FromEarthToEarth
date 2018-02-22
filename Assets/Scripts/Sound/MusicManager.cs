using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Song {
    vivid_skies,
    steady_rain
}

public class MusicManager : MonoBehaviour {

    private static MusicManager instance = null;
    public static MusicManager Instance {
        get { return instance; }
    }

    public AudioSource vivid_skies;
    public AudioSource steady_rain;
    public int time_between_sounds;

    private List<Song> played_songs;

    private AudioSource current_song;

    void Awake() {

        if (instance != null && instance != this) {
            Destroy(this.gameObject);
            return;
        }
        else {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }

    void Start() {

        if (time_between_sounds == 0) {
            time_between_sounds = 10;
            print("No time found, time between sounds assigned 10 to avoid infinite looop");
        }

        current_song = null;
        played_songs = new List<Song>();

        StartCoroutine("SoundLoop");
    }

    private IEnumerator SoundLoop() {

        while (true) { //or for(;;){

            if (!instance.vivid_skies.isPlaying) {
                yield return new WaitForSeconds(time_between_sounds);
                instance.vivid_skies.Play();
            }
            else {
                yield return new WaitForSeconds(1);
            }
        }
    }


    // private IEnumerator Pause() {
    //     yield return new WaitForSeconds(5);
    // }

    public void TrigNewLevel() {

        if (!instance.vivid_skies.isPlaying) {
            // instance.current_song = instance.vivid_skies;
            instance.vivid_skies.Play();
            instance.played_songs.Add(Song.vivid_skies);
        }
    }

    void Update() {

        if (current_song != null && !current_song.isPlaying) {
            current_song = null;
        }

    }
}
