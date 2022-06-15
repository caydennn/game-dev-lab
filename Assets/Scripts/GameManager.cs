using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class GameManager : MonoBehaviour
{

    public TMP_Text score;
    public int playerScore = 0;
    public static event gameEvent OnPlayerDeath;
    public static event gameEvent SpawnEnemy;

    public void increaseScore() {
        playerScore += 1;
        score.text = "SCORE: " + playerScore.ToString();
        SpawnEnemy();
        // SpawnEnemy();
    }

    public void damagePlayer() {
        OnPlayerDeath();
    }
    
    public delegate void gameEvent();

}
