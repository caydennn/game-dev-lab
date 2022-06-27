using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableBrick : MonoBehaviour
{
    public GameObject fireParticlePrefab;
    private GameObject thisFireParticlePrefab;
    private ParticleSystem fireParticle;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player")) {
        Debug.Log("Collision detected");
            thisFireParticlePrefab = (GameObject) Instantiate(fireParticlePrefab, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.identity);
        
           
            // fireParticlePrefab.transform.position = gameObject.transform.position;

            fireParticle = thisFireParticlePrefab.GetComponent<ParticleSystem>();
            fireParticle.Play();
            Destroy(gameObject);
        }
    }
}
