using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{

    public bool thrusting;
    public float turnSpeed = 1.0f;
    private Rigidbody2D _rigidBody;
    public float thrustSpeed = 1.0f;
    private float _turnDirection;
    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        thrusting = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            _turnDirection = 1f;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            _turnDirection = -1f;
        }
        else
        {
            _turnDirection = 0f;
        }
    }

    private void FixedUpdate()
    {
        if (thrusting)
        {
            _rigidBody.AddForce(this.transform.up * this.thrustSpeed);
        }
        if (_turnDirection != 0.0f)
        {
            _rigidBody.AddTorque(this.turnSpeed * _turnDirection);
        }
    }
}
