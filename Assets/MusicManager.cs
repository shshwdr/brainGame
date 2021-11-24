using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    AudioSource audioSource;
    private AudioClip[] clips;
    int currentIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        clips = Resources.LoadAll<AudioClip>("music");
        audioSource.clip = clips[currentIndex];
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (!audioSource.isPlaying)
        {
            currentIndex++;
            if (currentIndex >= clips.Length)
            {
                currentIndex = 0;
            }
            audioSource.clip = clips[currentIndex];
            audioSource.Play();
        }
    }
}
