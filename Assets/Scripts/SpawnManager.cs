using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameConstants gameConstants;

    void Awake()
    {
        spawnFromPooler(ObjectType.gombaEnemy);
        spawnFromPooler(ObjectType.greenEnemy);
    }
        // spawn two gombaEnemy
        // for (int j = 0; j < 2; j++) spawnFromPooler(ObjectType.gombaEnemy);

    // Start is called before the first frame update
    void Start()
    {
        GameManager.SpawnEnemy += SpawnEnemy;
    }

    // Update is called once per frame
    void Update()
    {
    }

    void spawnFromPooler(ObjectType i)
    {
        Debug.Log("Calling spawn from pooler");

        // static method access
        GameObject item = ObjectPooler.SharedInstance.GetPooledObject(i);
        if (item != null)
        {
            //set position, and other necessary states
            item.transform.position =
                new Vector3(Random.Range(-4.5f, 4.5f),
                    (float) gameConstants.groundSurface,
                    0);

            // reset the scale
            item.transform.localScale = new Vector3(1, 1, 1);
            item.SetActive(true);
        }
        else
        {
            Debug.Log("not enough items in the pool.");
        }
    }

    private void SpawnEnemy()
    {
        // spawnFromPooler(ObjectType.gombaEnemy);
        // spawnFromPooler(ObjectType.greenEnemy);
        SpawnRandomEnemy();
    }

    public void SpawnRandomEnemy()
    {
        // randomly spawn an enemy
        int random = Random.Range(0, 2);
        Debug.Log(random);
        if (random <= 0.5)
            spawnFromPooler(ObjectType.gombaEnemy);
        else
            spawnFromPooler(ObjectType.greenEnemy);
    }
}
