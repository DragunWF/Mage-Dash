using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private int health = 3;
    private const float jumpForce = 12.5f;
    private Rigidbody2D rigidBody;

    public void DamageHealth()
    {
        health -= 1;
    }

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {

    }

    private void OnJump()
    {
        rigidBody.velocity += new Vector2(0, jumpForce);
        // Add sound effect in the future
    }

    private void OnFire()
    {

    }
}
