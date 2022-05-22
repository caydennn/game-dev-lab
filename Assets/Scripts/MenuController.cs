using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class MenuController : MonoBehaviour
{
    public GameObject mario;
    public GameObject enemyPrefab;
    public TMP_Text scoreText;

    private GameObject enemy;
    void Awake()
    {
        Time.timeScale = 0.0f;
    }

    public void StartButtonClicked()
    {
        foreach (Transform eachChild in transform)
        {
            if (eachChild.name != "Score")
            {

                // disable them
                eachChild.gameObject.SetActive(false);
                Time.timeScale = 1.0f;
            }
        }

        instantiateEnemy();
        mario.GetComponent<PlayerController>().enemyLocation = enemy.transform;




    }

    // todo: probably not the ideal way to restart the game
    // explore better options 
    public void restartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        // mario.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        // mario.GetComponent<Rigidbody2D>().angularVelocity= 0f;
        // mario.GetComponent<Rigidbody2D>().Sleep();

        // Destroy(enemy);
        // instantiateEnemy();
        // // Reset mario and gomba
        // mario.transform.position = Vector3.zero;
        // mario.GetComponent<PlayerController>().enemyLocation = enemy.transform;

        // // Show the UI 
        // foreach (Transform eachChild in transform)
        // {
        //     if (eachChild.name != "Score")
        //     {
        //         // disable them
        //         eachChild.gameObject.SetActive(false);
        //     }


        // }
        // scoreText.text = "Score: 0";
        // mario.GetComponent<PlayerController>().score = 0;
        // Time.timeScale = 1.0f;



    }

    private void instantiateEnemy()
    {
        GameObject newEnemy = Instantiate(enemyPrefab);
        newEnemy.transform.position = new Vector2(-1.80f, -0.43f);
        enemy = newEnemy;

    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
