using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
     [SerializeField] private AudioSource _audioSource;
     [SerializeField] private AudioClip _audioClip;

     private void OnEnable()
     {
          ScoreManager.OnScore += HitSound;
     }

     private void OnDisable()
     {
          ScoreManager.OnScore -= HitSound;  
     }

     public void HitSound()
     {
          _audioSource.PlayOneShot(_audioClip);
     }
}
