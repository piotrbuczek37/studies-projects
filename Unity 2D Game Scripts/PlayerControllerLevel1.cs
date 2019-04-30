using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerControllerLevel1 : MonoBehaviour {

    public float moveSpeed = 0.2f;
    public float jumpForce = 6f;
    private Rigidbody2D rigidBody;

    public LayerMask groundLayer;
    public Animator animator;
    private bool isWalking;
    private bool isFacingRight;

    private Vector2 startPosition;
    private float killOffset = 0.2f;

    private int score;
    public int lives = 3;

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        startPosition = this.transform.position;
        
    }

    // Use this for initialization
    void Start ()
    {
        isFacingRight = true;
        
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (GameManager.instance.currentGameState == GameManager.GameState.GS_GAME)
        {
        isWalking = false;

        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        { // move avatar right
                if (transform.parent != null)
                {
                    Unlock();
                }    
                MoveRight(); //transform.Translate(moveSpeed*Time.deltaTime,0.0f,0.0f,Space.World );
            if (!isFacingRight)
                Flip();
            isWalking = true;
        }
        else
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        { // move avatar left
                if (transform.parent != null)
                {
                    Unlock();
                }            
            if (isFacingRight)
                Flip();
            MoveLeft(); // transform.Translate(-moveSpeed*...);
            isWalking = true;
        }
        else
        {
            if (rigidBody.velocity.x > 0.1f)
                rigidBody.velocity = new Vector2(
                0.95f * rigidBody.velocity.x, rigidBody.velocity.y);
            else
            if (rigidBody.velocity.x < 0.1f)
                rigidBody.velocity = new Vector2(
                0.95f * rigidBody.velocity.x, rigidBody.velocity.y);
            else
                rigidBody.velocity = new Vector2(0f, 0f);
        }
        if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)) && isGrounded())
            {
                if (transform.parent != null)
                    Unlock();
                Jump(); // jump
            }
            
        animator.SetBool("isGrounded", isGrounded());
        animator.SetBool("isWalking", isWalking);
            GameManager.instance.addScore(1);
        }
    }

    bool isGrounded()
    {
        return Physics2D.Raycast(this.transform.position, Vector2.down, 1.9f, groundLayer.value);
    }

    void Jump()
    {
        if(isGrounded())
        {
            rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Coin"))
        {
            GameManager.instance.addCoins(1);
            other.gameObject.SetActive (false);
        }
        if (other.CompareTag("Meta"))
        {
            if (GameManager.instance.keys == 3)
            {
                Debug.Log("You have all keys! You WIN!");
                GameManager.instance.LevelCompleted();
            }
            else
                Debug.Log("You don't have all keys!");
        }
        if (other.CompareTag("Key"))
        {
            GameManager.instance.addKeys(1);
            other.gameObject.SetActive(false);
        }
        if (other.CompareTag("Heart"))
        {
            lives += 1;
            if (GameManager.instance.hearts < 2)
            {
                GameManager.instance.heartsTab[GameManager.instance.hearts + 1].enabled = true;
                GameManager.instance.addHearts(1);
                other.gameObject.SetActive(false);
            }
        }
        if (other.CompareTag("FallLevel"))
        {
            GameManager.instance.GameOver();
        }

        else
            if (other.CompareTag("Enemy"))
                {
            if (other.gameObject.transform.position.y + killOffset <
            this.transform.position.y)
            {
                GameManager.instance.addEnemies(1);
                GameManager.instance.addCoins(10);
            }
            else
            {
                lives--;
                GameManager.instance.substractHearts(1);
                if (lives <= 0)
                {
                    Debug.Log("GameOver");
                    GameManager.instance.GameOver();
                }        
                this.transform.position = startPosition;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("MovingPlatform"))
        {
            rigidBody.isKinematic = true;
            transform.parent = other.transform;
        }
    }

    private void Unlock()
    {
        rigidBody.isKinematic = false;
        transform.parent = null;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("MovingPlatform"))
        {
            Unlock();
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
