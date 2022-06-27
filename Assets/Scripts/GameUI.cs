using UnityEngine;

public class GameUI : Singleton<GameUI>
{
    public override void Awake()
    {
        base.Awake();
        Debug.Log("awake called");
        // other instructions...
    }
}
