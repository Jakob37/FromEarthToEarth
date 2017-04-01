using UnityEngine;
using System.Collections;

public class MusicController : MonoBehaviour {

//    public AudioClip[] audio_clips;
    public AudioSource first_rain_song;

    private static MusicController instance = null;
    public static MusicController Instance {
        get { return instance; }
    }

    private float transition_in_time;
    private float transition_out_time;

    void Awake() {
        if (instance != null && instance != this) {
            Destroy(this.gameObject);
            return;
        }
        else {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }

	void Start () {

        if (first_rain_song != null) {
            first_rain_song.Play();
        }
        else {
            Debug.Log("Warning: No song assigned to first_rain_song");
        }
    }

    public void Mute() {
        first_rain_song.Stop();
    }
	
}
