using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speedPlayer = 15f;

    [SerializeField] private float smoothSpeed = 0.2f;


    private Vector2 inputMovement;
    private Vector2 currentInputMovement;
    private Vector2 velocity = Vector2.zero;

    private Rigidbody2D rb2D;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        currentInputMovement = Vector2.SmoothDamp(currentInputMovement, inputMovement, ref velocity, smoothSpeed);
    }

    void FixedUpdate()
    {
        rb2D.MovePosition(rb2D.position + currentInputMovement * speedPlayer * Time.deltaTime);
    }

    public void InputVector(InputAction.CallbackContext callback)
    {
        inputMovement = callback.ReadValue<Vector2>();
    }
}
