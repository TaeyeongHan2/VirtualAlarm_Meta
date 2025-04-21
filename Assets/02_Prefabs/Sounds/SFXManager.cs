using System.Collections;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public AudioSource currentAudioSource;
    public static SFXManager Instance { get; private set; }

    void Awake()
    {
        Instance = this;
    }

    public void StopCurrentSound()
    {
        currentAudioSource.Stop();
    }

    public void PlaySound(AudioSource audio,bool isAutoQuitOn, float maxPlaySEC)
    {
        audio.Play();
        currentAudioSource = audio;
        
        if (isAutoQuitOn)
        {
            var audioCheckCoroutine = StartCoroutine(AutoQuitAlarm(audio,maxPlaySEC));
        }
    }
    private IEnumerator AutoQuitAlarm(AudioSource audio , float maxPlaySEC)
    {
        yield return new WaitForSeconds(maxPlaySEC);
        
        if (audio.isPlaying)
        {
            audio.Stop();
            UINavigator.instance.ChangePage(1);
        }
    }

}
