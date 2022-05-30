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

// todo: make enemy if needed
        // instantiateEnemy();
        // mario.GetComponent<PlayerController>().enemyLocation = enemy.transform;

    }

    public void restartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
    }

    private void instantiateEnemy()
    {
        GameObject newEnemy = Instantiate(enemyPrefab);
        newEnemy.transform.position = new Vector2(-1.80f, -0.43f);
        enemy = newEnemy;

    }
  
}
