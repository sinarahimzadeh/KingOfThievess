using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public static CharacterMovement instance; 
    public enum State {right , left , wall }
   public State state;
    Rigidbody rb;
    [SerializeField] private float speed, jumoForce;
    [SerializeField] bool onGround; 
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        instance = this; 
        state = State.right; 
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.layer == 6) { onGround = true; }
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 6&&collision.transform.tag=="wall") { speed = 0;state = State.wall;rb.velocity = Vector3.zero; }

    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 6) { onGround = false; }
    }
    // Update is called once per frame
    void Update()
    {
        if (onGround)
        {
            if (GameManager.Instamce._gameState == GameManager.GameState.game&&Input.GetMouseButtonDown(0)) 
            {
                rb.AddForce(jumoForce*Time.deltaTime*Vector3.up);
            }
            if (state == State.right && GameManager.Instamce._gameState == GameManager.GameState.game)
            {
               rb.transform.Translate(Vector3.right * Time.deltaTime * speed);
            }
            if (state == State.left && GameManager.Instamce._gameState == GameManager.GameState.game)
            {
               rb.transform.Translate(Vector3.left * Time.deltaTime * speed);
            }
        }
    }
}
