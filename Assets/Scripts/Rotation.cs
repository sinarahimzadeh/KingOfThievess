using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Rotation : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Vector3 rotation;
    [SerializeField] private float duration;
    void Start()
    {
       

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward); 
    }
}
