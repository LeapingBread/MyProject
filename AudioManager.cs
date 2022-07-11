using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class AudioManager : Singleton<AudioManager>
{
    [Header("SoundData")]
    [SerializeField] SoundDataDetial_SO soundDataDetial;
    [SerializeField] Sound_SO sound;
    public SoundDataDetial_SO SoundDataDetial_SO { get { return soundDataDetial; } }
    public Sound_SO Sound { get { return sound; } }
    [Header("Audio Source")]
    [SerializeField] AudioSource ambientSource;
    [SerializeField] AudioSource gameMusicSource;
    [SerializeField] float musicStartTime;
    Coroutine soundRoutine;
    [Header("Audio Mixer")]
    public AudioMixer audioMixer;
    [Header("Snapshots")]
    public AudioMixerSnapshot basicSnapShot;
    public AudioMixerSnapshot ambientSnapShot;
    public AudioMixerSnapshot musicSnapShot;
    [SerializeField] float musicTransitionSceond;

    private void OnEnable()
    {
        EventSystem.AfterSceneLoaded += OnAfterSceneLoaded;
        EventSystem.PlaySoundEvent += OnPlaySoundEvent;
    }
    private void OnDisable()
    {
        EventSystem.AfterSceneLoaded -= OnAfterSceneLoaded;
        EventSystem.PlaySoundEvent -= OnPlaySoundEvent;
    }
    private void Start()
    {
        string currentScene = "Menu";
        Sound sceneSound = sound.getSound(currentScene);
        if (sceneSound == null) return;
        SoundDtials ambient = soundDataDetial.GetSoundDtials(sceneSound.ambient);
        SoundDtials music = soundDataDetial.GetSoundDtials(sceneSound.music);

        if (soundRoutine != null)
            StopCoroutine(soundRoutine);
        soundRoutine = StartCoroutine(SoundRoutine(ambient, music));
    }
    void OnPlaySoundEvent(SoundName soundName)
    {
        var soundDetials = AudioManager.Instance.SoundDataDetial_SO.GetSoundDtials(soundName);
        if(soundName != null)
        EventSystem.CallInitSoundEffect(soundDetials);
    }
    void OnAfterSceneLoaded()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        Sound sceneSound = sound.getSound(currentScene);
        if (sceneSound == null) return;
        SoundDtials ambient = soundDataDetial.GetSoundDtials(sceneSound.ambient);
        SoundDtials music = soundDataDetial.GetSoundDtials(sceneSound.music);

        if (soundRoutine != null)
            StopCoroutine(soundRoutine);
        soundRoutine = StartCoroutine(SoundRoutine(ambient, music));
    }
    IEnumerator SoundRoutine(SoundDtials ambient, SoundDtials music)
    {
        PlayAmbientClip(ambient);
        yield return new WaitForSeconds(musicStartTime);
        PlayMusicClip(music);
    }
    void PlayMusicClip(SoundDtials soundDtials)
    {
        audioMixer.SetFloat("MusicVolume", ConvertSoundVolume(soundDtials.soundVolume));
        gameMusicSource.clip = soundDtials.audioClip;
        if (gameMusicSource.isActiveAndEnabled)
            gameMusicSource.Play();
        basicSnapShot.TransitionTo(musicTransitionSceond);
    }
    void PlayAmbientClip(SoundDtials soundDtials)
    {
        audioMixer.SetFloat("AmbientVolume", ConvertSoundVolume(soundDtials.soundVolume));
        ambientSource.clip = soundDtials.audioClip;
        if (ambientSource.isActiveAndEnabled)
            ambientSource.Play();
        ambientSnapShot.TransitionTo(1f);
    }
    float ConvertSoundVolume ( float amount)
    {
        return (amount * 100 - 80);
    }
}
