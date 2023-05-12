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
        InvokeRepeating("Spawn",0.5f,Random.Range(2,spawnTime));
    }

    public void Spawn() 
    {
        if (CurrentCoin==null) 
        {
            CurrentCoin= Instantiate(Coin, this.transform.position,Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
