using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
  public Rigidbody theRB;
  public float maxSpeed = 30f;

  public float forwardAccel = 8f, reverseAccel = 4f;

  private float speedInput;

  private void Start() {
    theRB.transform.parent = null;
  }

  private void Update() {
    speedInput = 0f;

    if (Input.GetAxis("Vertical") > 0)
    {
      speedInput = Input.GetAxis("Vertical") * forwardAccel;
    }else if (Input.GetAxis("Vertical") < 0)
    {
      speedInput = Input.GetAxis("Vertical") * reverseAccel;
    }
    transform.position = theRB.position;
  }

  private void FixedUpdate() {
    theRB.AddForce(new Vector3(0, 0, speedInput));
  }
}
 