﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
    public float XMin = 5.5f;
    public float XMax = 5.5f;
    public float moveSpeed = 2f;
    public Animator animator;

    public float killOffset = 0.2f;
    private float startPositionX;
    private bool isMovingRight = true;
    private Rigidbody2D rigidBody;
    private bool isFacingRight = true;


    // Use this for initialization
    void Awake () {
        startPositionX = this.transform.position.x;
        this.transform.position = new Vector2 (Random.Range(startPositionX - XMin, startPositionX + XMax), this.transform.position.y);
        rigidBody = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
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
                Flip();
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
                Flip();
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

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.gameObject.transform.position.y >
            this.transform.position.y + killOffset)
            {
                Debug.Log("Enemy is dead");
                animator.SetBool("isDead", true);
                StartCoroutine(KillOnAnimationEnd());
            }
            else
            {
                Debug.Log("Killed player");
            }
        }
    }

    private IEnumerator KillOnAnimationEnd()
    {
        yield return new WaitForSeconds(0.7f);
        this.gameObject.SetActive(false);
    }
}
