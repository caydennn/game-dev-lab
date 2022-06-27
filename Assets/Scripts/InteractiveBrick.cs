using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveBrick : MonoBehaviour
{
    public GameObject coinPrefab; // the  coin prefab

    // Start is called before the first frame update
    void Start()
    {
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            float yoffset =
                other.gameObject.transform.position.y - transform.position.y;

            if (yoffset < 0.75f)
            {
                Instantiate(coinPrefab,
                new Vector3(this.transform.position.x,
                    this.transform.position.y + 1.0f,
                    this.transform.position.z),
                Quaternion.identity);
            }
        }
    }
}
