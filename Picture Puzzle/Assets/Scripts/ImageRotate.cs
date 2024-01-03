using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageRotate : MonoBehaviour
{
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip audioClip;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioClip;
    }
    private void OnMouseDown()
    {
        if (!GameController.youWin)
        {
            PlaySound();
            transform.Rotate(0, 0, 90f);

        }
    }
    void PlaySound()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
}
