using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreMonitor : MonoBehaviour
{
    public IntVariable marioScore;

    public TMP_Text text;

    public void Start()
    {
        UpdateScore();

    }

    public void UpdateScore()
    {
      
        text.text = "Score: " + marioScore.Value.ToString();
        Debug.Log("Shud be updated score");
    }
}
