using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Rotation : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Vector3 rotation;
    [SerializeField] private float duration;
    [SerializeField] private GameObject currentSpawner,coinParticle;
    // Update is called once per frame
    
    private void OnTriggerStay(Collider other)
    {
        if (other.transform.tag == "spawner") 
        {
            currentSpawner = other.gameObject;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player") 
        {
            GameManager.Instamce.Score++;
            coinParticle.transform.position = this.transform.position;
            coinParticle.GetComponent<ParticleSystem>().Play(); 
            currentSpawner.GetComponent<CoinScript>().spawned = false;
            this.gameObject.SetActive(false);
        }
    }
    private void Start()
    {
        coinParticle = GameObject.Find("CoinParticle");
    }
    void Update()
    {
        transform.Rotate(Vector3.forward); 
    }
}
