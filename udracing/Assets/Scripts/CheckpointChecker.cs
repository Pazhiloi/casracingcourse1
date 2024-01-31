using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointChecker : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
      if (other.tag == "Checkpoint")
      {
        Debug.Log("Hit cp " + other.GetComponent<Checkpoint>().cpNumber);
      }
    }
}
