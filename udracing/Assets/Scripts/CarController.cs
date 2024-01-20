using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
  public Rigidbody theRB;
  public float maxSpeed = 30f;

  private void Start() {
    theRB.transform.parent = null;
  }

  private void Update() {
    theRB.AddForce(new Vector3(0, 0, 100f));

    transform.position = theRB.position;
  }
}
 