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
        goodSongTime = 10f;
        goodPlaying = false;
        badSongTime = 15f;
    }

    // Update is called once per frame
    void Update()
    {
        if (goodDancing && !goodPlaying && !badDancing)
        {
            audioSource.clip = goodSong;
            audioSource.time = goodSongTime;
            audioSource.Play();
            goodPlaying = true;
        }
        if (badDancing && !badPlaying && !goodDancing)
        {
            audioSource.clip = badSong;
            audioSource.time = badSongTime;
            audioSource.Play();
            badPlaying = true;
        }
        if (goodDancing && !badDancing)
        {
            goodSongTime = audioSource.time;
        }
        if (badPlaying && !goodDancing)
        {
            badSongTime = audioSource.time;
        }
        if (!goodDancing && !badDancing)
        {
            audioSource.Stop();
            goodPlaying = false;
            badPlaying = false;
        }
    }
}
