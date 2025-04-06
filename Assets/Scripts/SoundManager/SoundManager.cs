using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private Slider globalSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider effectsSlider;
    public static SoundManager I { get; private set; }
    [SerializeField] private GameObject soundPref;
    [SerializeField] private SoundLibrary soundLibrary;
    [SerializeField] private bool playMusic;
    [SerializeField] private SoundName menuBackClip, battleBackClip, publicClip;
    [Range(0, 1)]
    [SerializeField] private float globalVolume, musicVolume, effectsVolume;

    private AudioSource musicAudioSrc;
    private AudioSource ambianceSound;
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

            musicAudioSrc = gameObject.AddComponent<AudioSource>();
            ambianceSound = gameObject.AddComponent<AudioSource>();

            // Recuperar volúmenes guardados o usar los del Inspector
            globalVolume = PlayerPrefs.HasKey("GlobalVolume") ? PlayerPrefs.GetFloat("GlobalVolume") : globalVolume;
            musicVolume = PlayerPrefs.HasKey("MusicVolume") ? PlayerPrefs.GetFloat("MusicVolume") : musicVolume;
            effectsVolume = PlayerPrefs.HasKey("EffectsVolume") ? PlayerPrefs.GetFloat("EffectsVolume") : effectsVolume;
        }
    }
    void Start()
    {
        if (playMusic)
        {
            PlayBackgroundSound(menuBackClip, musicAudioSrc);
        }

        // Establecer valores iniciales en los sliders basados en PlayerPrefs
        if (globalSlider) globalSlider.value = globalVolume * 100f;
        if (musicSlider) musicSlider.value = musicVolume * 100f;
        if (effectsSlider) effectsSlider.value = effectsVolume * 100f;
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

        GameObject soundGO;
        soundGO = PoolManager.SpawnObject(soundPref, transform);

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
            StartCoroutine(ReturnObjectToPool(soundGO, soundClip.audioClip.length));
        }
    }

    public void PlaySound(SoundName soundName, Vector3 position, float pitch = 1)
    {
        SoundClip soundClip = GetAudioClip(soundName);
        if (soundClip == null || !CanPlaySound(soundClip)) return;

        GameObject soundGO = PoolManager.SpawnObject(soundPref, position, Quaternion.identity);

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
            StartCoroutine(ReturnObjectToPool(soundGO, soundClip.audioClip.length));
        }
    }

    public void PlaySound(SoundName soundName, Transform parent, float pitch = 1)
    {
        SoundClip soundClip = GetAudioClip(soundName);
        if (soundClip == null || !CanPlaySound(soundClip)) return;

        GameObject soundGO = PoolManager.SpawnObject(soundPref, parent);

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
            StartCoroutine(ReturnObjectToPool(soundGO, soundClip.audioClip.length));
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
        if (SceneManager.GetActiveScene().buildIndex == 1 || SceneManager.GetActiveScene().buildIndex == 2) return;

        // atura tots els sons
        StopBackgroundSound();

        // si l'escena és una escena de batalla posa la musica de batalla i el public
        if (SceneManager.GetActiveScene().buildIndex > 2)
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

    public void GlobalVolume(float single)
    {
        globalVolume = single / 100f;
        PlayerPrefs.SetFloat("GlobalVolume", globalVolume);

        UpdateAllVolumes();

        Debug.Log($"[VOLUMEN GLOBAL] globalVolume: {globalVolume:F2} → musicVolume real: {(musicVolume * globalVolume):F2}, effectsVolume real: {(effectsVolume * globalVolume):F2}");
    }

    public void MusicVolume(float single)
    {
        musicVolume = single / 100f;
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);

        UpdateAllVolumes();

        Debug.Log($"[VOLUMEN MÚSICA] musicVolume: {musicVolume:F2} → volumen aplicado: {(musicVolume * globalVolume):F2}");
    }

    public void EffectsVolume(float single)
    {
        effectsVolume = single / 100f;
        PlayerPrefs.SetFloat("EffectsVolume", effectsVolume);

        UpdateAllVolumes();

        Debug.Log($"[VOLUMEN EFECTOS] effectsVolume: {effectsVolume:F2} → volumen aplicado: {(effectsVolume * globalVolume):F2}");
    }

    private void UpdateAllVolumes()
    {
        // Actualiza música
        if (musicAudioSrc && musicAudioSrc.clip != null)
        {
            SoundClip musicClip = GetAudioClip(musicAudioSrc.clip.name);
            musicAudioSrc.volume = musicClip.volume * musicVolume * globalVolume;
        }

        // Actualiza sonido de ambiente
        if (ambianceSound && ambianceSound.clip != null)
        {
            SoundClip ambianceClip = GetAudioClip(ambianceSound.clip.name);
            ambianceSound.volume = ambianceClip.volume * musicVolume * globalVolume;
        }

        // Actualiza efectos activos (excepto música y ambiente)
        foreach (AudioSource src in FindObjectsByType<AudioSource>(FindObjectsSortMode.None))
        {
            if (src == musicAudioSrc || src == ambianceSound) continue;
            if (!src.isPlaying || src.clip == null) continue;

            SoundClip effectClip = GetAudioClip(src.clip.name);
            if (effectClip != null)
            {
                src.volume = effectClip.volume * effectsVolume * globalVolume;
            }
        }
    }

    private IEnumerator ReturnObjectToPool(GameObject go, float delay)
    {
        yield return new WaitForSeconds(delay);

        PoolManager.ReturnObjectToPool(go);
    }
}
