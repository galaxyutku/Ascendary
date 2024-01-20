using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] public AudioSource CemetarySource;
    [SerializeField] public AudioSource ChurchSource;
    [SerializeField] public AudioSource SwampSource;
    [SerializeField] public AudioSource FinalSource;
    [SerializeField] public string scene;

    public AudioClip CemetaryMusic;
    public AudioClip ChurchMusic;
    public AudioClip SwampMusic;
    public AudioClip FinalMusic;

    public static bool isMusicOn;


    private void Start()
    {
        if(scene == "menu")
        {
            FinalSource.clip = FinalMusic;
            FinalSource.Play();
        }
        if(scene == "play")
        {
            CemetarySource.clip = CemetaryMusic;
            CemetarySource.Play();
        }
    }

    public void PlayCemetary()
    {
        CemetarySource.clip = CemetaryMusic;
        CemetarySource.Play();
    }

    public void PlayChurch()
    {
        ChurchSource.clip = ChurchMusic;
        ChurchSource.Play();
    }

    public void PlaySwamp()
    {
        SwampSource.clip = SwampMusic;
        SwampSource.Play();
    }
    public void PlayFinal()
    {
        FinalSource.clip = FinalMusic;
        FinalSource.Play();
    }

    public void PlayMain()
    {
        FinalSource.clip = FinalMusic;
        FinalSource.Play();
    }

    public void StopAll()
    {
        CemetarySource.Stop();
        SwampSource.Stop();
        FinalSource.Stop();
        ChurchSource.Stop();
    }
}
