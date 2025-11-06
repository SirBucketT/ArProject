using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource AudioSourceSFX, AudioSourceBGM;
    [SerializeField] private AudioClip OnCorrectClick, OnIncorrectClick;

    public static SoundManager Instance;

    private void Awake ()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayScoreGain () => AudioSourceSFX.PlayOneShot(OnCorrectClick);
    public void PlayBadPick () => AudioSourceSFX.PlayOneShot(OnIncorrectClick);

    public void PlaySfx(AudioClip audioClip) => AudioSourceSFX.PlayOneShot(audioClip);

    public void PlayBGM(AudioClip audioClip)
    {
        AudioSourceBGM.Stop();
        AudioSourceBGM.clip = audioClip;
        AudioSourceBGM.Play();
    }
}
