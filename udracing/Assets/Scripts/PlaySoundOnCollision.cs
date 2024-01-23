using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnCollision : MonoBehaviour
{
    public AudioSource soundToPlay;
    public int groundLayerNo = 8;

    private void OnCollisionEnter(Collision other) {

      if (other.gameObject.layer != groundLayerNo)
      {
      soundToPlay.Stop();
      soundToPlay.pitch = Random.Range(0.8f, 1.2f);
      soundToPlay.Play();
      }
    }
}
