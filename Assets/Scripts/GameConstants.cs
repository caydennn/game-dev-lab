using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[
    CreateAssetMenu(
        fileName = "GameConstants",
        menuName = "ScriptableObjects/GameConstants",
        order = 1)
]
public class GameConstants : ScriptableObject
{
    // for Scoring system
    int currentScore;

    int currentPlayerHealth;

    // for Reset values
    Vector3 gombaSpawnPointStart = new Vector3(2.5f, -0.45f, 0); // hard coded location

    // for Consume.cs

    public int consumeTimeStep = 10;

    public int consumelargestScale = 4;

    // for Break.cs
    public int breakTimeStep = 30;

    public int breakDebrisTorque = 10;

    public int breakDebrisForce = 10;

    // for SpawnDerbis.cs
    public int spawnNumberOfDebris = 10;

    // for Rotator.cs

    public int rotatorRotationSpeed = 6;

    // for ground
    public double groundSurface = -0.29;

    // for Enemy
    public float enemyPatroltime = 2.0f;
    public float maxOffset = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
}
