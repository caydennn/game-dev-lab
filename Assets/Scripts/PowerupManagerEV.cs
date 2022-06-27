using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PowerupIndex
{
    ORANGEMUSHROOM = 0,
    REDMUSHROOM = 1
}

public class PowerupManagerEV : MonoBehaviour
{
    // reference of all player stats affected
    public IntVariable marioJumpSpeed;

    public IntVariable marioMaxSpeed;

    public PowerupInventory powerupInventory;

    public List<GameObject> powerupIcons;

    void Start()
    {
        if (!powerupInventory.gameStarted)
        {
            powerupInventory.gameStarted = true;
            powerupInventory.Setup(powerupIcons.Count);
            resetPowerup();
        }
        else
        {
            // re-render the contents of the powerup from the previous time
            for (int i = 0; i < powerupInventory.Items.Count; i++)
            {
                Powerup p = powerupInventory.Get(i);
                if (p != null)
                {
                    AddPowerupUI(i, p.powerupTexture);
                }
            }
        }
    }

    public void resetPowerup()
    {
        for (int i = 0; i < powerupIcons.Count; i++)
        {
            powerupIcons[i].SetActive(false);
        }
    }

    void AddPowerupUI(int index, Texture t)
    {
        powerupIcons[index].GetComponent<RawImage>().texture = t;
        powerupIcons[index].SetActive(true);
    }

    void RemovePowerupUI(int index, Texture t)
    {
        // powerupIcons[index].GetComponent<RawImage>().texture = t;
        powerupIcons[index].SetActive(false);
    }

    public void AddPowerup(Powerup p)
    {
        powerupInventory.Add(p, (int) p.index);
        AddPowerupUI((int) p.index, p.powerupTexture);
    }

    public void RemovePowerup(Powerup p)
    {
        powerupInventory.Remove((int) p.index);
        RemovePowerupUI((int) p.index, p.powerupTexture);
    }

    public void OnApplicationQuit()
    {
        ResetValues();
    }

    public void ResetValues()
    {
        powerupInventory.Clear();
        resetPowerup();
    }

    public void AttemptConsumePowerup(KeyCode key)
    {
        if (key == KeyCode.X)
        {
            ConsumePowerup((int) PowerupIndex.REDMUSHROOM);
        }
        else if (key == KeyCode.Z)
        {
            ConsumePowerup((int) PowerupIndex.ORANGEMUSHROOM);
        }
    }

    public void ConsumePowerup(int index)
    {
        Debug.Log("Consume powerup called");
        Debug.Log (index);
        Powerup p = powerupInventory.Get((int) index);
        Debug.Log (p);
        if (p != null)
        {
            // marioJumpSpeed += p.absoluteJumpBooster;
            // marioMaxSpeed += p.absoluteSpeedBoost;
            marioJumpSpeed.ApplyChange(p.absoluteJumpBooster);
            marioMaxSpeed.ApplyChange(p.aboluteSpeedBooster);
            RemovePowerup (p);
            StartCoroutine(removeEffect(p));
            Debug.Log("Fade Effect");
        }
    }

    IEnumerator removeEffect(Powerup p)
    {
        yield return new WaitForSeconds(p.duration);
        marioJumpSpeed.ApplyChange(-p.absoluteJumpBooster);
        marioMaxSpeed.ApplyChange(-p.aboluteSpeedBooster);
    }
}
