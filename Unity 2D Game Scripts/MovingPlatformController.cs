using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformController : MonoBehaviour {

    public float XMin = 5.5f;
    public float XMax = 5.5f;
    public float moveSpeed = 2f;

    private float startPositionX;
    private bool isMovingRight = true;
    private Rigidbody2D rigidBody;


    // Use this for initialization
    void Awake()
    {
        startPositionX = this.transform.position.x;
        this.transform.position = new Vector2(Random.Range(startPositionX - XMin, startPositionX + XMax), this.transform.position.y);
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isMovingRight)
        {
            if (this.transform.position.x < startPositionX + XMax)
            {
                MoveRight();
            }
            else
            {
                isMovingRight = false;
                MoveLeft();
            }
        }
        else
        {
            if (this.transform.position.x > startPositionX - XMin)
            {
                MoveLeft();
            }
            else
            {
                isMovingRight = true;
                MoveRight();
            }
        }
    }

    void MoveRight()
    {
        if (rigidBody.velocity.x < moveSpeed)
        {
            rigidBody.velocity = new Vector2(moveSpeed, rigidBody.velocity.y);
            rigidBody.AddForce(Vector2.right * 4.6f, ForceMode2D.Impulse);
        }
    }

    void MoveLeft()
    {
        if (rigidBody.velocity.x > -moveSpeed)
        {
            rigidBody.velocity = new Vector2(-moveSpeed, rigidBody.velocity.y);
            rigidBody.AddForce(Vector2.left * 4.6f, ForceMode2D.Impulse);
        }
    }
}
