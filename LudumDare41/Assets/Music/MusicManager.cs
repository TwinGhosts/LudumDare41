using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    private bool isMuted = false;

    [Header("Source")]
    public AudioSource musicSource;

    [Header("Sound Files")]
    [Header("Misc")]
    public AudioClip buttonClick;

    [Header("Player")]
    public AudioClip playerDamage;
    public AudioClip playerDead;

    [Header("Classes")]
    public AudioClip archerAbility1;
    public AudioClip archerAbility2;
    public AudioClip archerAbility3;
    public AudioClip archerAbility4;
    public AudioClip warriorAbility1;
    public AudioClip warriorAbility2;
    public AudioClip warriorAbility3;
    public AudioClip warriorAbility4;

    public AudioClip enemyDead;
    public AudioClip win;
    public AudioClip explode1;
    public AudioClip shoot1;
    public AudioClip shoot2;
    public AudioClip support;

    [Header("Music Files")]
    public AudioClip musicTitle;
    public AudioClip musicMain;

    void Awake()
    {
        // Keep the object when there is no music manager yet
        if (Util.MusicManager == null)
        {
            DontDestroyOnLoad(gameObject);
            Util.MusicManager = this;
        }
        else
        {
            Destroy(gameObject);
        }

        CheckForMusic();
    }

    public void MuteMusic(bool mute)
    {
        if (mute)
        {
            Util.MusicManager.musicSource.Pause();
        }
        else
        {
            Util.MusicManager.musicSource.UnPause();
        }
    }

    public void ToggleMute()
    {
        Util.MusicManager.isMuted = !Util.MusicManager.isMuted;
        Util.MusicManager.MuteMusic(Util.MusicManager.isMuted);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            Util.MusicManager.ToggleMute();
        }
    }

    private void CheckForMusic()
    {
        if (!Util.MusicManager.musicSource.isPlaying && Util.MusicManager.musicSource.clip != Util.MusicManager.musicTitle && Util.MenuSceneActive)
        {
            Util.MusicManager.PlayMusic(musicTitle, 0.3f, true);
        }
        else if (Util.MusicManager.musicSource.isPlaying && Util.MusicManager.musicSource.clip != Util.MusicManager.musicTitle && Util.MenuSceneActive)
        {
            Util.MusicManager.musicSource.Stop();
            Util.MusicManager.PlayMusic(musicTitle, 0.3f, true);
        }

        if (!Util.MusicManager.musicSource.isPlaying && Util.MusicManager.musicSource.clip != Util.MusicManager.musicMain && !Util.MenuSceneActive)
        {
            Util.MusicManager.PlayMusic(musicMain, 0.3f, true);
        }
        else if (Util.MusicManager.musicSource.isPlaying && Util.MusicManager.musicSource.clip != Util.MusicManager.musicMain && !Util.MenuSceneActive)
        {
            Util.MusicManager.musicSource.Stop();
            Util.MusicManager.PlayMusic(musicMain, 0.3f, true);
        }
    }

    public void PlaySound(AudioSource source, AudioClip clipToPlay, float volume)
    {
        source.PlayOneShot(clipToPlay, volume);
    }

    public void PlayMusic(AudioClip musicClip, float volume, bool repeat)
    {
        musicSource.clip = musicClip;
        musicSource.volume = volume;
        musicSource.Play();
        musicSource.loop = repeat;
    }

    public void PlaySound(AudioClip clip)
    {
        var source = musicSource;
        source.PlayOneShot(clip, 0.3f);
    }
}
