using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private GameClip[] _gameClips;
    public AudioSource GameAudioSource;
    public AudioSource GameMusicSource;

    public AudioManager(AudioSource gameMusicSource)
    {
        GameMusicSource = gameMusicSource;
    }

    public static AudioManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }


    public void PlaySound(string soundName)
    {
        var sound = Array.Find(_gameClips, 
            gameClip => gameClip.name == soundName);
        GameAudioSource.PlayOneShot(sound.audioClip);
    }
    //Serialize this in the inspector, 
    //Idea here was an audio dictionary initially
    //but could not serialize a dictionary in the inspector
    [Serializable]
    struct GameClip
    {
        public string name;
        public AudioClip audioClip;
    }
}
