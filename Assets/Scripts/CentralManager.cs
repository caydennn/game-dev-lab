using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CentralManager : MonoBehaviour
{
    public GameObject gameManagerObject;

    public GameManager gameManager;

    public GameObject powerupManagerObject;

    public GameObject spawnManagerObject;

    private SpawnManager spawnManager;

    private PowerupManager powerUpManager;

    

    public static CentralManager centralManagerInstance;

    void Awake()
    {
        centralManagerInstance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        gameManager = gameManagerObject.GetComponent<GameManager>();
        powerUpManager = powerupManagerObject.GetComponent<PowerupManager>();
        spawnManager = spawnManagerObject.GetComponent<SpawnManager>();
    }

    public void increaseScore()
    {
        gameManager.increaseScore();
    }

    public void damagePlayer()
    {
        gameManager.damagePlayer();
    }

    public void consumePowerup(KeyCode k, GameObject g)
    {
        powerUpManager.consumePowerup (k, g);
    }

    public void addPowerup(Texture t, int i, ConsummableInterface c)
    {
        powerUpManager.addPowerup (t, i, c);
    }
    
   
}
