using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameConstants gameConstants;

    private ObjectPooler objectPooler;

    private float originalX;

    private int moveRight = -1;

    private Vector2 velocity;

    private Rigidbody2D enemyBody;

    void Start()
    {
        enemyBody = GetComponent<Rigidbody2D>();

        // objectPooler = objectPoolerObject.GetComponent<ObjectPooler>();
        // find tag with SpawnPool
        objectPooler =
            GameObject
                .FindGameObjectWithTag("SpawnPool")
                .GetComponent<ObjectPooler>();

        // get the starting position
        originalX = transform.position.x;
        ComputeVelocity();

        GameManager.OnPlayerDeath += EnemyRejoice;
    }

    void ComputeVelocity()
    {
        velocity =
            new Vector2((moveRight) *
                gameConstants.maxOffset /
                gameConstants.enemyPatroltime,
                0);
    }

    void MoveEnemy()
    {
        enemyBody
            .MovePosition(enemyBody.position + velocity * Time.fixedDeltaTime);
    }

    void Update()
    {
        if (
            Math.Abs(enemyBody.position.x - originalX) <
            gameConstants.maxOffset
        )
        {
            MoveEnemy();
        }
        else
        {
            // change direction
            moveRight *= -1;
            ComputeVelocity();
            MoveEnemy();
        }

        if (moveRight < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            float yoffset =
                other.gameObject.transform.position.y - transform.position.y;

            // check if the yoffset is more than 0.75f
            if (yoffset > 0.75f)
            {
                KillSelf();
            }
            else
            {
                // hurt player
                CentralManager.centralManagerInstance.damagePlayer();
            }
        }

        if (other.gameObject.tag == "Pipe")
        {
            moveRight *= -1;
            GetComponent<SpriteRenderer>().flipX = false;

            ComputeVelocity();
        }
    }

    void KillSelf()
    {
        // enemy dies
        CentralManager.centralManagerInstance.gameManager.increaseScore();

        StartCoroutine(flatten());
        Debug.Log("Kill sequence ends");
    }

    IEnumerator flatten()
    {
        // gradually animate eneby being flattened on to the ground
        int steps = 5;
        float stepper = 1.0f / (float) steps;
        for (int i = 0; i < steps; i++)
        {
            this.transform.localScale =
                new Vector3(this.transform.localScale.x,
                    this.transform.localScale.y - stepper,
                    this.transform.localScale.z);

            // make sure enemy is still above ground
            this.transform.position =
                new Vector3(this.transform.position.x,
                    (float) gameConstants.groundSurface +
                    GetComponent<SpriteRenderer>().bounds.extents.y,
                    this.transform.position.z);
            yield return null;
        }

        this.gameObject.SetActive(false);
        Debug.Log("Enemy returned to pool");
        yield break;
    }

    // animation when player is dead
    void EnemyRejoice()
    {
        Debug.Log("Enemy killed Mario");

        foreach (ObjectPooler.ExistingPoolItem
            item
            in
            objectPooler.pooledObjects
        )
        {
            GameObject gameObject = item.gameObject;
            if (gameObject.activeInHierarchy)
            {
                gameObject.GetComponent<EnemyController>().velocity =
                    new Vector2(0, 0);
                gameObject.GetComponent<Animator>().SetTrigger("Rejoice");
            }
        }
    }

    void BeginDancing()
    {
        StartCoroutine(flipEnemy());
    }

    IEnumerator flipEnemy()
    {
        while (true)
        {
            Debug.Log("Flippin");
            GetComponent<SpriteRenderer>().flipX = true;
            yield return new WaitForSeconds(0.5f);
            GetComponent<SpriteRenderer>().flipX = false;
            yield return new WaitForSeconds(0.5f);
        }

        // yield return new WaitForSeconds(0.5f);
    }
}
