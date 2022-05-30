using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomController : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    public float speed;

    private Vector2 currentDirection; // {1, 0} or  {-1,0}


    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.AddForce(Vector2.up  *  2, ForceMode2D.Impulse);
        currentDirection = new Vector2(1, 0);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 nextPosition = rigidBody.position + speed * currentDirection.normalized * Time.fixedDeltaTime;
        rigidBody.MovePosition(nextPosition);
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
            speed = 0;
        }
    }

     void OnBecameInvisible() {
        Destroy(gameObject);
        // speed = 0; // stop moving when out of camera view
    }
}
