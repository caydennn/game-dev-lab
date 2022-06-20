using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public AudioSource themeMusic;
    public AudioSource marioDieAudio;
    

    public void playThemeMusic() {
        themeMusic.Play();
    }

    public  void playMarioDieAudio() {
        marioDieAudio.PlayOneShot(marioDieAudio.clip);
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
}
