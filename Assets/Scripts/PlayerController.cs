using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class PlayerController : MonoBehaviour
{
    public float speed;
    public float upSpeed;
    public float maxSpeed;

    public Transform enemyLocation;
    public TMP_Text scoreText;

    public GameObject restartButton;
    public int score = 0;
    private bool countScoreState = false;
    private SpriteRenderer marioSprite;
    private bool faceRightState = true;
    private Rigidbody2D marioBody;
    private bool onGroundState = true;

    // Start is called before the first frame update
    void Start()
    {
        // Set to be 30 FPS
        Application.targetFrameRate = 30;
        marioBody = GetComponent<Rigidbody2D>();
        marioSprite = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {

        float moveHorizontal = Input.GetAxis("Horizontal");

        if (Mathf.Abs(moveHorizontal) > 0)
        {
            Vector2 movement = new Vector2(moveHorizontal, 0);
            if (marioBody.velocity.magnitude < maxSpeed)
                marioBody.AddForce(movement * speed);
        }
        if (Input.GetKeyUp("a") || Input.GetKeyUp("d"))
        {
            // stop
            marioBody.velocity = Vector2.zero;

        }

        if (Input.GetKeyDown("space") && onGroundState)
        {
            marioBody.AddForce(Vector2.up * upSpeed, ForceMode2D.Impulse);
            onGroundState = false;
            countScoreState = true; //check if Gomba is underneath
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            onGroundState = true; // back on ground
            countScoreState = false; // reset score state
            scoreText.text = "Score: " + score.ToString();
        }
    }

    private void Update()
    {
        // toggle state
        if (Input.GetKeyDown("a") && faceRightState)
        {
            faceRightState = false;
            marioSprite.flipX = true;
        }

        if (Input.GetKeyDown("d") && !faceRightState)
        {
            faceRightState = true;
            marioSprite.flipX = false;
        }

        // when jumping, and Gomba is near Mario and we haven't registered our score
        if (!onGroundState && countScoreState)
        {
            if (Mathf.Abs(transform.position.x - enemyLocation.position.x) < 0.5f)
            {
                countScoreState = false;
                score++;
                Debug.Log(score);
            }
        }
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
        float moveHorizontal = Input.GetAxis("Horizontal");
            Vector2 movement = new Vector2(moveHorizontal, 0);

        // marioBody.velocity = Vector2.zero;
        marioBody.AddForce(movement * -speed);
        Time.timeScale = 0.0f;
        restartButton.SetActive(true);
        }
    }
}

