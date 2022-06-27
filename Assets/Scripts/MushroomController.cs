using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomController : MonoBehaviour
{
    private Rigidbody2D rigidBody;

    public float speed;

    private Vector2 currentDirection; // {1, 0} or  {-1,0}

    private bool collected = false;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.AddForce(Vector2.up * 2, ForceMode2D.Impulse);
        float rand = Random.Range(0f, 1.0f);

        currentDirection = rand > 0.5 ? new Vector2(1, 0) : new Vector2(-1, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (!collected)
        {
            Vector2 nextPosition =
                rigidBody.position +
                speed * currentDirection.normalized * Time.fixedDeltaTime;
            rigidBody.MovePosition (nextPosition);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Pipe"))
        {
            // change direction
            currentDirection *= -1;
        }

        if (other.gameObject.CompareTag("Player"))
        {
            // Stop Moving
            collected = true;
            StartCoroutine(ScaleUp());
        }
    }

    void OnBecameInvisible()
    {
        // Destroy (gameObject);
        // speed = 0; // stop moving when out of camera view
    }

    // coroutine to scale up mushroom
    IEnumerator ScaleUp()
    {

        float scale = 0.0f;
        while (scale < 0.6f)
        {
            scale += 0.1f;
            this.transform.localScale =
                new Vector3(scale, scale, gameObject.transform.localScale.z);

            yield return null;
        }

        this.transform.localScale  = new Vector3(0,0,0);
    }
}
