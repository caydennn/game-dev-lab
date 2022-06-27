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

    // Mario basic starting values
    public int playerStartingMaxSpeed = 5;

    public int playerMaxJumpSpeed = 30;

    public int playerDefaultForce = 150;

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
    public float groundSurface = -0.29f;

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
