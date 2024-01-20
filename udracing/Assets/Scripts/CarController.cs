using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
  public Rigidbody theRB;
  public float maxSpeed = 30f;

  public float forwardAccel = 8f, reverseAccel = 4f;

  private float speedInput;

  public float turnStrength = 180f;
  private float turnInput;

  private void Start()
  {
    theRB.transform.parent = null;
  }

  private void Update()
  {
    speedInput = 0f;

    if (Input.GetAxis("Vertical") > 0)
    {
      speedInput = Input.GetAxis("Vertical") * forwardAccel;
    }
    else if (Input.GetAxis("Vertical") < 0)
    {
      speedInput = Input.GetAxis("Vertical") * reverseAccel;
    }

    turnInput = Input.GetAxis("Horizontal");

    if (Input.GetAxis("Vertical") != 0)
    {
      transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0, turnInput * turnStrength * Time.deltaTime * Mathf.Sign(speedInput) * (theRB.velocity.magnitude / maxSpeed), 0));
    }

    transform.position = theRB.position;
  }

  private void FixedUpdate()
  {
    theRB.AddForce(transform.forward * speedInput * 1000f);
  }
}
