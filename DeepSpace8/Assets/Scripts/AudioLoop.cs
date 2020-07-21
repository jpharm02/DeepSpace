using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Audio Loop script to play CyberPunk music while playing game.
public class AudioLoop : MonoBehaviour
{
    public AudioSource adSource;
    public AudioClip[] adClips;
    private bool playMusic = true;

    void Start()
    {
        StartCoroutine(playAudioSequentially());
    }

    IEnumerator playAudioSequentially()
    {
        yield return null;

        while (playMusic)
        {
            for (int i = Random.Range(0, adClips.Length) ; i < adClips.Length; i++)
            {
                adSource.clip = adClips[i];

                adSource.Play();

                while (adSource.isPlaying)
                {
                    yield return null;
                }
            }
        }
    }
}
