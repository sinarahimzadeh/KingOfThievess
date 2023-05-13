using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform Coin;
   [SerializeField] public static Transform  CurrentCoin;
    [SerializeField] float spawnTime;
    public bool spawned;
    void Start()
    {

        InvokeRepeating("Spawn", Random.Range(0, spawnTime), Random.Range(2,spawnTime));
    }

    public void Spawn() 
    {
        if (!spawned) 
        {
            Instantiate(Coin, this.transform.position,Coin.transform.rotation);

            spawned = true;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
