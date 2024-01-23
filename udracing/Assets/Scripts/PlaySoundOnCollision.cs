using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnCollision : MonoBehaviour
{
    public AudioSource soundToPlay;

    private void OnCollisionEnter(Collision other) {
      soundToPlay.Stop();
      soundToPlay.Play();
    }
}
