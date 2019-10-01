using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public bool goodDancing;
    public bool goodPlaying;
    public bool badDancing;
    public bool badPlaying;

    public AudioSource audioSource;
    public AudioClip goodSong;
    public AudioClip badSong;

    public bool recordedGoodTime;
    public float goodSongTime;
    public bool recordedBadTime;
    public float badSongTime;

    // Start is called before the first frame update
    void Start()
    {
        goodSongTime = 0f;
        goodPlaying = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (goodDancing && !goodPlaying)
        {
            audioSource.clip = goodSong;
            audioSource.time = goodSongTime;
            audioSource.Play();
            goodPlaying = true;
        }
        if (goodDancing)
        {
            goodSongTime = audioSource.time;
        }
        if (!goodDancing)
        {
            audioSource.Stop();
            goodPlaying = false;
        }
    }
}
