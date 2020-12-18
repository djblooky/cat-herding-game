using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndAudio : MonoBehaviour
{
    [SerializeField] private AudioClip GameWin;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(GameWin);
    }





}
