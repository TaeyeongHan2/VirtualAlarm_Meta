using UnityEngine;

public class CharacterVoicePlay : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip earlyLine1;
    public AudioClip earlyLine2;
    public AudioClip earlyLine3;
    public AudioClip onTimeLine1;
    public AudioClip onTimeLine2;
    public AudioClip lateLine1;
    public AudioClip lateLine2;

    public AudioClip WaveLine;
    public AudioClip JumpLine;
    
    public AudioClip GuideLine;

    public void PlayEarlyLine()
    {
        audioSource.PlayOneShot(earlyLine1);
    }

    public void PlayEarlyLine2()
    {
        audioSource.PlayOneShot(earlyLine2);
    }

    public void PlayEarlyLine3()
    {
        audioSource.PlayOneShot(earlyLine3);
    }

    public void PlayOnTimeLine1()
    {
        audioSource.PlayOneShot(onTimeLine1);
    }

    public void PlayOnTimeLine2()
    {
        audioSource.PlayOneShot(onTimeLine2);
    }

    public void PlayLateLine1()
    {
        audioSource.PlayOneShot(lateLine1);
    }

    public void PlayLateLine2()
    {
        audioSource.PlayOneShot(lateLine2);
    }

    public void PlayWaveLine()
    {
        audioSource.PlayOneShot(WaveLine);
    }

    public void PlayJumpLine()
    {
        audioSource.PlayOneShot(JumpLine);
    }


    public void PlayGuideLine()
    {
        audioSource.PlayOneShot(GuideLine);
    }
}
