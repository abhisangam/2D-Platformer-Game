using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AudioType
{
    SFX,
    BG
}

[Serializable]
public class GameSound
{
    public string Name;
    public AudioType Type;
    public AudioClip Clip;
};

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    public static AudioManager Instance { get { return instance; } private set { } }

    [SerializeField]
    AudioSource SFXAudioSource;
    [SerializeField]
    AudioSource BGAudioSource;

    //Audio clips
    [SerializeField]
    GameSound[] gameSounds;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            GameObject.Destroy(gameObject);
        }
    }

    public void Play(string soundName)
    {
        GameSound sound = new GameSound();
        for(int i = 0; i < gameSounds.Length; i++)
        {
            if (gameSounds[i].Name == soundName)
            {
                sound = gameSounds[i];
                break;
            }
        }

        if(sound.Clip != null)
        {
            switch(sound.Type)
            {
                case AudioType.SFX:
                    SFXAudioSource.PlayOneShot(sound.Clip);
                    break;
                case AudioType.BG:
                    BGAudioSource.clip = sound.Clip;
                    BGAudioSource.Play();
                    break;
            }
        }
    }
}
