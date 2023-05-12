using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Rotation : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Vector3 rotation;
    [SerializeField] private float duration;
    [SerializeField] private CoinScript coinScript;
    // Update is called once per frame

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.tag == "Player") 
        {
            GameManager.Instamce.Score++;
            CoinScript.CurrentCoin = null;
            this.gameObject.SetActive(false);
        }
    }
    void Update()
    {
        transform.Rotate(Vector3.forward); 
    }
}
