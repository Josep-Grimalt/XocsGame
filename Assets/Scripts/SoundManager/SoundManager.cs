using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public static SoundManager I { get; private set; }
    [SerializeField] private SoundLibrary soundLibrary;
    [SerializeField] private bool playMusic;
    [SerializeField] private SoundName menuBackClip, battleBackClip, publicClip;
    [Range(0, 1)]
    [SerializeField] private float globalVolume, musicVolume, effectsVolume;

    private readonly AudioSource musicAudioSrc, ambianceSound;
    private Dictionary<SoundName, float> soundTimers;

    void Awake()
    {
        if (I)
        {
            Destroy(gameObject);
        }
        else
        {
            I = this;
            InitializeSoundTimers();
            DontDestroyOnLoad(I);
        }
    }

    void Start()
    {
        if (playMusic)
        {
            PlayBackgroundSound(menuBackClip, musicAudioSrc);
        }

    }

    void OnEnable()
    {
        SceneManager.activeSceneChanged += ChangeBackgroundSound;
    }

    void OnDisable()
    {
        SceneManager.activeSceneChanged -= ChangeBackgroundSound;
    }

    public void PlaySound(SoundName soundName, float pitch = 1)
    {
        SoundClip soundClip = GetAudioClip(soundName);
        if (soundClip == null || !CanPlaySound(soundClip)) return;

        GameObject soundGO = new("2D Sound");
        soundGO.transform.SetParent(transform);

        AudioSource audioSrc2D = soundGO.AddComponent<AudioSource>();
        audioSrc2D.loop = soundClip.loop;
        audioSrc2D.volume = soundClip.volume * effectsVolume * globalVolume;
        audioSrc2D.pitch = pitch;

        if (audioSrc2D.loop)
        {
            audioSrc2D.clip = soundClip.audioClip;
            audioSrc2D.Play();
        }
        else
        {
            audioSrc2D.PlayOneShot(soundClip.audioClip);
            Destroy(soundGO, soundClip.audioClip.length);
        }
    }

    public void PlaySound(SoundName soundName, Vector3 position, float pitch = 1)
    {
        SoundClip soundClip = GetAudioClip(soundName);
        if (soundClip == null || !CanPlaySound(soundClip)) return;

        GameObject soundGO = new("3D Sound");
        soundGO.transform.position = position;

        AudioSource audioSrc3D = soundGO.AddComponent<AudioSource>();
        audioSrc3D.loop = soundClip.loop;
        audioSrc3D.volume = soundClip.volume * effectsVolume * globalVolume;
        audioSrc3D.pitch = pitch;

        if (audioSrc3D.loop)
        {
            audioSrc3D.clip = soundClip.audioClip;
            audioSrc3D.Play();
        }
        else
        {
            audioSrc3D.PlayOneShot(soundClip.audioClip);
            Destroy(soundGO, soundClip.audioClip.length);
        }
    }

    public void PlaySound(SoundName soundName, Transform parent, float pitch = 1)
    {
        SoundClip soundClip = GetAudioClip(soundName);
        if (soundClip == null || !CanPlaySound(soundClip)) return;

        GameObject soundGO = new("2D Sound");
        soundGO.transform.SetParent(parent);

        AudioSource audioSrc = soundGO.AddComponent<AudioSource>();
        audioSrc.loop = soundClip.loop;
        audioSrc.volume = soundClip.volume * effectsVolume * globalVolume;
        audioSrc.pitch = pitch;

        if (audioSrc.loop)
        {
            audioSrc.clip = soundClip.audioClip;
            audioSrc.Play();
        }
        else
        {
            audioSrc.PlayOneShot(soundClip.audioClip);
            Destroy(soundGO, soundClip.audioClip.length);
        }
    }

    public void PlayBackgroundSound(SoundName soundName, AudioSource audioSource)
    {
        SoundClip soundClip = GetAudioClip(soundName);
        if (soundClip == null || !CanPlaySound(soundClip)) return;

        if (!audioSource)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.loop = true;
        audioSource.volume = soundClip.volume * musicVolume * globalVolume;
        audioSource.clip = soundClip.audioClip;

        audioSource.Play();
    }

    public void StopBackgroundSound()
    {
        if (musicAudioSrc) musicAudioSrc.Stop();
        if (ambianceSound) ambianceSound.Stop();
    }

    public void StopAllSounds()
    {
        foreach (AudioSource audioSource in FindObjectsByType<AudioSource>(FindObjectsSortMode.None))
        {
            audioSource.Stop();
        }
    }

    private void InitializeSoundTimers()
    {
        soundTimers = new Dictionary<SoundName, float>();

        foreach (SoundClip soundClip in soundLibrary.soundClips)
        {
            if (soundClip.hasPlayTimer)
            {
                soundTimers[soundClip.soundName] = soundClip.playTimer;
            }
        }
    }

    private bool CanPlaySound(SoundClip soundClip)
    {
        if (!soundClip.hasPlayTimer || !soundTimers.ContainsKey(soundClip.soundName))
        {
            if (soundClip.hasPlayTimer) soundTimers[soundClip.soundName] = Time.time;

            return true;
        }

        float lastTimePlayed = soundTimers[soundClip.soundName];
        if (Time.time > lastTimePlayed + soundClip.playTimer)
        {
            soundTimers[soundClip.soundName] = Time.time;

            return true;
        }
        else
        {
            return false;
        }
    }

    private SoundClip GetAudioClip(SoundName soundName)
    {
        foreach (SoundClip soundClip in soundLibrary.soundClips)
        {
            if (soundClip.soundName.Equals(soundName))
            {
                return soundClip;
            }
        }
        return null;
    }

    private SoundClip GetAudioClip(string soundName)
    {
        foreach (SoundClip soundClip in soundLibrary.soundClips)
        {
            if (soundClip.audioClip.name.Equals(soundName))
            {
                return soundClip;
            }
        }
        return null;
    }

    private void ChangeBackgroundSound(Scene arg0, Scene arg1)
    {
        // si l'escena es la de player selection no fa cap canvi
        if (SceneManager.GetActiveScene().buildIndex == 1) return;

        // atura tots els sons
        StopBackgroundSound();

        // si l'escena és una escena de batalla posa la musica de batalla i el public
        if (SceneManager.GetActiveScene().buildIndex > 1)
        {
            PlayBackgroundSound(battleBackClip, musicAudioSrc);
            PlayBackgroundSound(publicClip, ambianceSound);
        }

        // si l'escena NO es cap escena de batalla torna a posar la musica dels menus
        if (SceneManager.GetActiveScene().buildIndex < 2)
        {
            PlayBackgroundSound(menuBackClip, musicAudioSrc);
        }
    }

    public void MusicVolume(float single)
    {
        musicVolume = single / 100;

        // actualitza music volume
        if (musicAudioSrc)
        {
            musicAudioSrc.volume = GetAudioClip(musicAudioSrc.clip.name).volume * musicVolume * globalVolume;
        }
    }

    public void GlobalVolume(float single)
    {
        globalVolume = single / 100;

        // actualitza music volume
        if (musicAudioSrc)
        {
            musicAudioSrc.volume = GetAudioClip(musicAudioSrc.clip.name).volume * globalVolume * musicVolume;
        }
    }

    public void EffectsVolume(float single)
    {
        effectsVolume = single / 100;
    }
}
