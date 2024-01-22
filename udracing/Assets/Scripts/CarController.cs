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

  public Transform groundRayPoint, groundRayPoint2;
  public LayerMask whatIsGround;
  public float groundRayLength = 0.75f;

  private float dragOnGround;
  public float gravityMod = 10f;

  public Transform leftFrontWheel, rightFrontWheel;
  public float maxWheelTurn = 25f;

  public ParticleSystem[] dustTrail;
  public float maxEmission = 25f, emissionFadeSpeed = 20f;
  private float emissionRate;

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


    leftFrontWheel.localRotation = Quaternion.Euler(leftFrontWheel.localRotation.eulerAngles.x, (turnInput * maxWheelTurn) - 180, leftFrontWheel.localRotation.eulerAngles.z);

    rightFrontWheel.localRotation = Quaternion.Euler(rightFrontWheel.localRotation.eulerAngles.x, (turnInput * maxWheelTurn), rightFrontWheel.localRotation.eulerAngles.z);

    transform.position = theRB.position;


    // control particle emissions

    emissionRate = Mathf.MoveTowards(emissionRate, 0f, emissionFadeSpeed * Time.deltaTime);

    if (grounded && (Mathf.Abs(turnInput) > 0.5f || (theRB.velocity.magnitude < maxSpeed * 0.5f && theRB.velocity.magnitude != 0)))
    {
      emissionRate = maxEmission;
    }

    if (theRB.velocity.magnitude <= 0.5f)
    {
      emissionRate = 0f;
    }

    for (int i = 0; i < dustTrail.Length; i++)
    {
      var emissionModule = dustTrail[i].emission;

      emissionModule.rateOverTime = emissionRate;
    }
  }

  private void FixedUpdate()
  {
    grounded = false;

    RaycastHit hit;
    Vector3 normalTarget = Vector3.zero;

    if (Physics.Raycast(groundRayPoint.position, -transform.up, out hit, groundRayLength, whatIsGround))
    {
      grounded = true;

      normalTarget = hit.normal;
    }

    if (Physics.Raycast(groundRayPoint2.position, -transform.up, out hit, groundRayLength, whatIsGround))
    {
      grounded = true;

      normalTarget = (normalTarget + hit.normal) / 2f;
    }

    if (grounded)
    {
      transform.rotation = Quaternion.FromToRotation(transform.up, normalTarget) * transform.rotation;
    }

    if (grounded)
    {
      theRB.drag = dragOnGround;
      theRB.AddForce(transform.forward * speedInput * 1000f);
    }
    else
    {
      theRB.drag = 0.1f;
      theRB.AddForce(-Vector3.up * gravityMod * 100f);
    }

    if (theRB.velocity.magnitude > maxSpeed)
    {
      theRB.velocity = theRB.velocity.normalized * maxSpeed;
    }
  }
}
