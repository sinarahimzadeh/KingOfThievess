using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public static CharacterMovement instance; 
    public enum HorizontalState {right , left  }
   public HorizontalState state;

    public enum VerticalState { ground, wall , air }
    public VerticalState state2;
    Rigidbody rb;
    [SerializeField] private float speed, jumoForce;
    [SerializeField] bool slide;
  
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        instance = this; 
        state = HorizontalState.right; 
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.layer == 6 && collision.transform.tag == "wall")
        {
            if (collision.transform.name == "roof") state2 = VerticalState.ground;
            else
            {
                state2 = VerticalState.wall;
            }
            }

        else if (collision.gameObject.layer == 6 && collision.transform.tag == "ground") { state2 = VerticalState.ground; }
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 6&&collision.transform.tag=="wall") { state2=VerticalState.wall;rb.velocity = Vector3.zero; }

    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 6) { state2 = VerticalState.air; }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if(Input.GetMouseButtonDown(0) && GameManager.Instamce._gameState == GameManager.GameState.game&& state2!= VerticalState.air)
        {
            if (state2 == VerticalState.wall) 
            {
                if (state == HorizontalState.left) state = HorizontalState.right;
                else { state = HorizontalState.left; }
            }
            switch (state) {
                case HorizontalState.left:
                    rb.AddForce(jumoForce * Time.deltaTime * new Vector3(-0.1f,1,0));
                    break;
                case HorizontalState.right:
                    rb.AddForce(jumoForce * Time.deltaTime * new Vector3(0.1f, 1, 0));

                    break;
            
                   }
        }
        if (state2 == VerticalState.ground)
        {
           
            if (state == HorizontalState.right && GameManager.Instamce._gameState == GameManager.GameState.game)
            {
               rb.transform.Translate(Vector3.right * Time.deltaTime * speed);
            }
            if (state == HorizontalState.left && GameManager.Instamce._gameState == GameManager.GameState.game)
            {
               rb.transform.Translate(Vector3.left * Time.deltaTime * speed);
            }
        }
    }
}
