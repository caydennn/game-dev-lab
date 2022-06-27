using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// private _static GameManager _instance;
public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            return _instance;
        }
    }

    private void Awake()
    {
        // check if the _instance is not this, means it's been set before, return
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        // otherwise, this is the first time this instance is created
        _instance = this;

        // add to preserve this object open scene loading
        DontDestroyOnLoad(this.gameObject); // only works on root gameObjects
    }

    public TMP_Text score;

    public int playerScore = 0;

    public static event gameEvent OnPlayerDeath;

    public static event gameEvent SpawnEnemy;

    public void increaseScore()
    {
        playerScore += 1;
        score.text = "SCORE: " + playerScore.ToString();
        SpawnEnemy();
    }

    public void damagePlayer()
    {
        OnPlayerDeath();
    }

    public delegate void gameEvent();
}
