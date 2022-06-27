using System.Collections;
using UnityEngine;

public class PlayerControllerEV : MonoBehaviour
{
    private float force;

    public IntVariable marioUpSpeed;

    public IntVariable marioMaxSpeed;

    public GameConstants gameConstants;

    private Animator marioAnimator;

    private Rigidbody2D marioBody;

    public AudioSource marioJumpAudio;

    public AudioSource marioDeathAudio;

    public CustomCastEvent onCast;

    private SpriteRenderer marioSprite;

    private bool onGroundState = true;

    private bool faceRightState = true;

    private bool countScoreState = false;

    private bool isADKeyUp = true;

    private bool isSpacebarUp = true;

    private bool isDead = false;

    // other components and interal state
    private void Start()
    {
        marioUpSpeed.SetValue(gameConstants.playerMaxJumpSpeed);
        marioMaxSpeed.SetValue(gameConstants.playerStartingMaxSpeed);
        force = gameConstants.playerDefaultForce;

        marioBody = GetComponent<Rigidbody2D>();
        marioSprite = GetComponent<SpriteRenderer>();
        marioAnimator = GetComponent<Animator>();
    }

    void PlayJumpSound()
    {
        marioJumpAudio.PlayOneShot(marioJumpAudio.clip);
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
            // dustCloud.Play();
            // scoreText.text = "Score: " + score.ToString();
        }
    }

    void Update()
    {
        if (!isDead)
        {
            if (Input.GetKeyDown("a"))
            {
                isADKeyUp = false;
                if (faceRightState)
                {
                    faceRightState = false;
                    marioSprite.flipX = true;
                    if (Mathf.Abs(marioBody.velocity.x) > 4.0)
                    {
                        marioAnimator.SetTrigger("onSkid");
                    }
                }
            }

            if (Input.GetKeyDown("d"))
            {
                isADKeyUp = false;
                if (!faceRightState)
                {
                    faceRightState = true;
                    marioSprite.flipX = false;
                    if (Mathf.Abs(marioBody.velocity.x) > 4.0)
                    {
                        marioAnimator.SetTrigger("onSkid");
                    }
                }
            }

            if (onGroundState)
            {
                // resetmarioground
                marioAnimator.SetBool("onGround", onGroundState);
            }

            marioAnimator.SetFloat("xSpeed", Mathf.Abs(marioBody.velocity.x));

            if (Input.GetKeyDown("z"))
            {
                Cast(KeyCode.Z);
            }
            if (Input.GetKeyDown("x"))
            {
                // cast power up
                Cast(KeyCode.X);
            }

            if (Input.GetKeyUp("a") || Input.GetKeyUp("d"))
            {
                isADKeyUp = true;
            }

            if (Input.GetKeyDown("space"))
            {
                isSpacebarUp = false;
            }

            if (Input.GetKeyUp("space"))
            {
                isSpacebarUp = true;
            }
        }
    }

    void Cast(KeyCode key)
    {
        onCast.Invoke (key);
    }

    public void PlayerDiesSequence()
    {
        isDead = true;
        marioAnimator.SetTrigger("onDeath");

        // marioAnimator.SetBool("onDeath", true);
        marioDeathAudio.PlayOneShot(marioDeathAudio.clip);
        GetComponent<Collider2D>().enabled = false;
        marioBody.AddForce(Vector3.up * 30, ForceMode2D.Impulse);
        marioBody.gravityScale = 30;
        StartCoroutine(dead());
    }

    IEnumerator dead()
    {
        yield return new WaitForSeconds(1.0f);
        marioBody.bodyType = RigidbodyType2D.Static;
    }

    void FixedUpdate()
    {
        if (!isDead)
        {
            //check if a or d is pressed currently
            if (!isADKeyUp)
            {
                float direction = faceRightState ? 1.0f : -1.0f;
                Vector2 movement = new Vector2(force * direction, 0);
                if (marioBody.velocity.magnitude < marioMaxSpeed.Value)
                    marioBody.AddForce(movement);
            }

            if (!isSpacebarUp && onGroundState)
            {
                marioBody
                    .AddForce(Vector2.up * marioUpSpeed.Value,
                    ForceMode2D.Impulse);
                onGroundState = false;

                // part 2
                marioAnimator.SetBool("onGround", onGroundState);
                countScoreState = true; //check if goomba is underneath
            }
        }
    }
}
