using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrangeMushroom : MonoBehaviour, ConsummableInterface
{
    public Texture t;

    public void consumedBy(GameObject player)
    {
        Debug.Log("Consuming ORANGE mushroom");
        player.GetComponent<PlayerController>().speed *= 2;
        StartCoroutine(removeEffect(player));
    }

    IEnumerator removeEffect(GameObject player)
    {
        yield return new WaitForSeconds(5.0f);
        player.GetComponent<PlayerController>().speed /= 2;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {

            CentralManager.centralManagerInstance.addPowerup(t, 1, this);
            GetComponent<Collider2D>().enabled = false;
        }
    }
}
