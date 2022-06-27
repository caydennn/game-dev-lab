using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;

    public float upSpeed;

    public float maxSpeed;

    public Transform enemyLocation;

    public TMP_Text scoreText;

    public AudioSource marioJumpAudio;

    public AudioSource marioDieAudio;

    public AudioSource themeMusic;

    public GameObject restartButton;

    public int score = 0;

    public ParticleSystem dustCloud;

    private Animator marioAnimator;

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
        marioAnimator = GetComponent<Animator>();

        // find game object with tag Music
        // themeMusic = GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>();
        GameManager.OnPlayerDeath += PlayerDiesSequence;
    }

    private void Awake()
    {
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

        if (
            Input.GetKeyDown("space") && onGroundState // prevent double jumping
        )
        {
            marioBody.AddForce(Vector2.up * upSpeed, ForceMode2D.Impulse);
            onGroundState = false;
            countScoreState = true; //check if Gomba is underneath
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (
            other.gameObject.CompareTag("Ground") ||
            other.gameObject.CompareTag("Obstacles") ||
            other.gameObject.CompareTag("Pipe")
        )
        {
            onGroundState = true; // back on ground
            countScoreState = false; // reset score state
            dustCloud.Play();
            // scoreText.text = "Score: " + score.ToString();
        }
    }

    private void Update()
    {
        // toggle state
        if (Input.GetKeyDown("a") && faceRightState)
        {
            faceRightState = false;
            marioSprite.flipX = true;

            // Lab 2:
            if (Mathf.Abs(marioBody.velocity.x) > 1.0)
                marioAnimator.SetTrigger("onSkid");
        }

        if (Input.GetKeyDown("d") && !faceRightState)
        {
            faceRightState = true;
            marioSprite.flipX = false;

            // Lab 2:
            if (Mathf.Abs(marioBody.velocity.x) > 1.0)
                marioAnimator.SetTrigger("onSkid");
        }

        // Lab 2
        marioAnimator.SetFloat("xSpeed", Mathf.Abs(marioBody.velocity.x));
        marioAnimator.SetBool("onGround", onGroundState);

        if (Input.GetKeyDown("z"))
        {
            CentralManager
                .centralManagerInstance
                .consumePowerup(KeyCode.Z, this.gameObject);
        }
        if (Input.GetKeyDown("x"))
        {
            CentralManager
                .centralManagerInstance
                .consumePowerup(KeyCode.X, this.gameObject);
        }
    }

    void PlayJumpSound()
    {
        marioJumpAudio.PlayOneShot(marioJumpAudio.clip);
    }

    public void PlayerDiesSequence()
    {
        // Mario dies
        Debug.Log("Mario Dies");

        themeMusic.Stop();
        Debug.Log("Audio Enabled?");

        // Debug.Log(marioDieAudio.enabled);
        marioDieAudio.PlayOneShot(marioDieAudio.clip);

        // marioDieAudio.Play();
        // Time.timeScale = 0.0f;
        // Animate mario dying
        marioAnimator = GetComponent<Animator>();

        marioAnimator.SetTrigger("onDeath");
        marioBody
            .AddForce(new Vector2(0, marioBody.mass * 50), ForceMode2D.Impulse);
        GetComponent<BoxCollider2D>().enabled = false;
        restartButton.gameObject.SetActive(true);
    }

    private void OnDestroy()
    {
        GameManager.OnPlayerDeath -= PlayerDiesSequence;
    }
}
