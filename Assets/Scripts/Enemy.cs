using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] bool isBat = false;
    [SerializeField] float speed = 5f;

    private Rigidbody2D rigidBody;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (isBat)
        {
            float deltaSpeed = -speed * Time.deltaTime;
            transform.Translate(deltaSpeed, 0, 0);
        }
    }
}
