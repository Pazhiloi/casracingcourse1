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

  private bool grounded;

  public Transform groundRayPoint;
  public LayerMask whatIsGround;
  public float groundRayLength = 0.75f;

  private float dragOnGround;
  public float gravityMod = 10f;
  private void Start()
  {
    theRB.transform.parent = null;

    dragOnGround = theRB.drag;
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

    if (grounded && Input.GetAxis("Vertical") != 0)
    {
      transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0, turnInput * turnStrength * Time.deltaTime * Mathf.Sign(speedInput) * (theRB.velocity.magnitude / maxSpeed), 0));
    }

    transform.position = theRB.position;
  }

  private void FixedUpdate()
  {
    grounded = false;

    RaycastHit hit;

    if (Physics.Raycast(groundRayPoint.position, -transform.up, out hit, groundRayLength, whatIsGround))
    {
      grounded = true;
    }

    if (grounded)
    {
      theRB.drag  = dragOnGround;
      theRB.AddForce(transform.forward * speedInput * 1000f);
    }else
    {
      theRB.drag  = 0.1f;
      theRB.AddForce(-Vector3.up * gravityMod * 100f);
    }

    if (theRB.velocity.magnitude > maxSpeed)
    {
      theRB.velocity = theRB.velocity.normalized * maxSpeed;
    }
  }
}
