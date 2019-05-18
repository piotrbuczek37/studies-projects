using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector2Int gridMoveDirection; //Definition of an Integer vector with the direction in which the snake is to move
    private Vector2Int gridPosition; //Definition of an Integer vector with the position of the snake
    private float gridMoveTimer; //Definition of the time with which snake can move in the direction until it is longer than the maximum time
    private float gridMoveTimerMax; //Definition of the maximum time with which snake can move in the direction
    [Range(0.01f, 1.00f)]
    public float MovementSpeedValue = 0.5f; //Definition of public snake movement speed value which can be changed in the editor
    private int snakeBodySize; //Definition of variable number of snake tail segments
    private List<Vector2Int> snakeBodyPositionList; //Definition of the list with the snake positions
    private float specialFoodTime = 0; //Initialization of the time with which special food can be seen until it is longer than the maximum time
    private float specialFoodTimerMax = 0; //Initialization of the maximum time with which special food can be seen
    private bool isSpecialFoodActive = false; //Initialization of the status of the activity of special food
    private bool leftClicked = false; //Initialization of the left turn button bool variable
    private bool rightClicked = false; //Initialization of the right turn button bool variable

    GameObject tailPart = null; //Initialization of the global snake tail object
    GameObject specialFoodObject = null; //Initialization of the global special food object

    public Sprite tailSprite; //Definition of public sprite of the snake's tail which can be changed in the editor

    private bool canHeMove = false; //Initialization of the status of the snake movement

    /// <summary>Awake() is a method which is performed when the game starts. The start position of the snake is set, the speed of movement (refresh time for movement), the direction in which the snake is to move. 
    /// The random position of the usual and special food is also set, the length of the snake, and the list of snake positions is created.
    /// </summary>
    void Awake()
    {
        gridPosition = new Vector2Int(5, 7);
        gridMoveTimerMax = 1.0f - MovementSpeedValue;
        gridMoveTimer = gridMoveTimerMax;
        gridMoveDirection = new Vector2Int(0, 1);

        GameObject.Find("Food").transform.position = new Vector2(Random.Range(1, 9), Random.Range(1, 14));
        GameObject.Find("SpecialFood").transform.position = new Vector2(Random.Range(1, 9), Random.Range(1, 14));
        specialFoodObject = GameObject.Find("SpecialFood");
        specialFoodObject.SetActive(false);

        tailPart = GameObject.Find("TailPart");
        

        snakeBodyPositionList = new List<Vector2Int>();
        snakeBodySize = 4;

    }

    /// <summary>Update() is a method, in which each frame calls functions associated with moving the snake and spawning special food.
    /// </summary>
    void Update()
    {
        

        if(GameManager.instance.currentGameState == GameManager.GameState.GS_GAME) {

            SnakeControllerInput();

            GridMovement();

            SpawningSpecialFood();
        }
    }

    /// <summary>The SnakeControllerInput() method checks whether the key or button is pressed and then sets the direction of the snake movement.
    /// </summary>
    void SnakeControllerInput()
    {
        if (canHeMove&& (Input.GetKeyDown(KeyCode.RightArrow) || rightClicked))
        {
            if (gridMoveDirection.x == 1)
            {
                gridMoveDirection.x = 0;
                gridMoveDirection.y = -1;
            }
            else if (gridMoveDirection.y == -1)
            {
                gridMoveDirection.x = -1;
                gridMoveDirection.y = 0;
            }
            else if (gridMoveDirection.x == -1)
            {
                gridMoveDirection.x = 0;
                gridMoveDirection.y = 1;
            }
            else
            {
                gridMoveDirection.x = 1;
                gridMoveDirection.y = 0;
            }
            rightClicked = false;
            canHeMove = false;
        }
        if (canHeMove&&(Input.GetKeyDown(KeyCode.LeftArrow) || leftClicked))
        {
            if (gridMoveDirection.x == 1)
            {
                gridMoveDirection.x = 0;
                gridMoveDirection.y = 1;
            }
            else if (gridMoveDirection.y == 1)
            {
                gridMoveDirection.x = -1;
                gridMoveDirection.y = 0;
            }
            else if (gridMoveDirection.x == -1)
            {
                gridMoveDirection.x = 0;
                gridMoveDirection.y = -1;
            }
            else
            {
                gridMoveDirection.x = 1;
                gridMoveDirection.y = 0;
            }
            leftClicked = false;
            canHeMove = false;
        }
    }

    /// <summary>The GridMovement() method moves the snake position at the level for a specified time. It also adds and removes snake positions to the list, 
    /// thanks to which the snake tail can grow by a certain number of segments and the tail can move with the snake. In each segment a new object is created 
    /// consisting of SpriteRenderer and CircleCollider2D and after the movement the last segment in the list is removed.
    /// </summary>
    void GridMovement()
    {
        gridMoveTimer += Time.deltaTime;

        if (gridMoveTimer >= gridMoveTimerMax)
        {
            gridMoveTimer -= gridMoveTimerMax;

            snakeBodyPositionList.Insert(0, gridPosition);

            gridPosition += gridMoveDirection;
            canHeMove = true;

            if (snakeBodyPositionList.Count >= snakeBodySize + 1)
            {
                snakeBodyPositionList.RemoveAt(snakeBodyPositionList.Count - 1);
            }

            for (int i = 0; i < snakeBodyPositionList.Count; i++)
            {
                Vector2Int snakeBodyPosition = snakeBodyPositionList[i];

                tailPart = new GameObject();
                tailPart.name = "TailPart";
                tailPart.transform.position = new Vector2(snakeBodyPosition.x, snakeBodyPosition.y);
                SpriteRenderer sr = tailPart.AddComponent<SpriteRenderer>();
                sr.color = new Color(0.9f, 0.9f, 0.9f, 1.0f);
                sr.sprite = tailSprite;
                CircleCollider2D cc = tailPart.AddComponent<CircleCollider2D>();
                cc.isTrigger = true;
                tailPart.tag = "Tail";
                Destroy(tailPart, gridMoveTimerMax);
            }

        }

        transform.position = new Vector3(gridPosition.x, gridPosition.y);
    }

    /// <summary>The OnTriggerEnter2D() method checks if the snake's head "touches" other collider. If so, an appropriate action is performed (e.g., adding points).
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Food"))
        {
            other.gameObject.transform.position = new Vector2(Random.Range(1, 9),Random.Range(1, 14));
            GameManager.instance.addScore(1);
            snakeBodySize++;
        }
        if (other.CompareTag("SpecialFood"))
        {
            other.gameObject.SetActive(false);
            isSpecialFoodActive = false;
            specialFoodTime = 0;
            snakeBodySize++;
            GameManager.instance.addScore(10);
        }
        if (other.CompareTag("Tail"))
        {
            GameManager.instance.GameOver();
        }
        if (other.CompareTag("Wall"))
        {
            GameManager.instance.GameOver();
        }
    }

    /// <summary>The setLeftClicked() method checks whether the left turn button has been pressed. If so, then the variable becomes true
    /// </summary>
    /// <param name="clicked"></param>
    public void setLeftClicked(bool clicked)
    {
        leftClicked = clicked;
    }

    /// <summary>The setRightClicked() method checks whether the right turn button has been pressed. If so, then the variable becomes true
    /// </summary>
    /// <param name="clicked"></param>
    public void setRightClicked(bool clicked)
    {
        rightClicked = clicked;
    }

    /// <summary>SpawningSpecialFood() is a method which is spawning special food at random time and stays for a certain time on the level.
    /// The object flashes in the last four seconds. After this time has elapsed, the object disappears and then appears in a random time and in a random place. 
    /// </summary>
    void SpawningSpecialFood()
    {
        specialFoodTimerMax = Random.Range(10.0f, 15.0f);
        specialFoodTime += Time.deltaTime;
        if (specialFoodTime > specialFoodTimerMax && specialFoodObject.activeSelf == false && !isSpecialFoodActive)
        {
            specialFoodObject.SetActive(true);
            specialFoodObject.transform.position = new Vector2(Random.Range(1, 9), Random.Range(1, 14));
            specialFoodTime = 0;
            isSpecialFoodActive = true;
        }
        else if (specialFoodTime > specialFoodTimerMax && isSpecialFoodActive)
        {
            specialFoodTime = 0;
            isSpecialFoodActive = false;
            specialFoodObject.SetActive(false);
        }

        if (specialFoodTime > 6.0f && specialFoodTime <= 7.0f && specialFoodObject.activeSelf == false && isSpecialFoodActive)
        {
            specialFoodObject.SetActive(true);
        }
        else if (specialFoodTime > 7.0f && specialFoodTime <= 8.0f && specialFoodObject.activeSelf == true && isSpecialFoodActive)
        {
            specialFoodObject.SetActive(false);
        }
        else if (specialFoodTime > 8.0f && specialFoodTime <= 9.0f && specialFoodObject.activeSelf == false && isSpecialFoodActive)
        {
            specialFoodObject.SetActive(true);
        }
        else if (specialFoodTime > 9.0f && specialFoodTime <= 9.5f && specialFoodObject.activeSelf == true && isSpecialFoodActive)
        {
            specialFoodObject.SetActive(false);
        }
        else if (specialFoodTime > 9.5f && specialFoodTime <= 10.0f && specialFoodObject.activeSelf == false && isSpecialFoodActive)
        {
            specialFoodObject.SetActive(true);
        }
    }
}
