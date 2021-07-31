using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BayatGames.SaveGameFree;

public class Sounds : MonoBehaviour
{
    public List<AudioClip> sfxList;
    public AudioSource bgAudioSource;
    public List<AudioSource> sfxSources;
    public Settings settings;

    public void Start()
    {
        if (SaveGame.Exists("bgSound"))
        {
            settings.Load();
        }
    }

    public void StartBG()
    {
        bgAudioSource.Stop();
        bgAudioSource.Play();
    }
}
