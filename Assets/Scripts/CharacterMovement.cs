using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

public class CharacterMovement : MonoBehaviour
{
    public static CharacterMovement instance; 
    public enum HorizontalState {right , left  }
   public HorizontalState state;

    public enum VerticalState { ground, wall , air , wallben }
    public VerticalState state2;
    Rigidbody rb;
    [SerializeField] private float speed, jumoForce;
    [SerializeField] bool slide;
    [SerializeField] PhysicMaterial pm;
  
    // Start is called before the first frame update
    void Start()
    {
        Camera.main.aspect = 18 /9;
        rb = GetComponent<Rigidbody>();
        instance = this; 
        state = HorizontalState.right; 
    }

    private void OnCollisionStay(Collision collision)
    {
        //when lands on top of the wall.
        if (collision.gameObject.layer == 6 && collision.transform.tag == "wall")
        {
            if (collision.transform.name == "roof")
            { state2 = VerticalState.ground; }
            else if (collision.transform.name == "wallben")
            {
                state2 = VerticalState.wallben;
                
            }
            else 
            {
                state2 = VerticalState.wall;
                slide = true;
            }

        }

        // when it is on the ground
        else if (collision.gameObject.layer == 6 && collision.transform.tag == "ground")
        { state2 = VerticalState.ground;
            rb.velocity = Vector3.zero; 
        }
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 6&&collision.transform.tag=="wall")
        {
            if (collision.transform.name == "roof")
            { 
                state2 = VerticalState.ground; 
            }
            else if (collision.transform.name == "wallben")
            {
                state2 = VerticalState.wallben; rb.velocity = Vector3.zero;
                speed = 0;

            }
            else
            {
                state2 = VerticalState.wall;
                slide = true;
            }
            
           
        }

    }
    private void OnCollisionExit(Collision collision)
    {
        // how the jump is detected so no second jump is possible in the air
        if (collision.gameObject.layer == 6)
        { state2 = VerticalState.air; }

    }
    // Update is called once per frame
    void Update()
    {
        if (slide == true) { pm.dynamicFriction = 50; }
        if (slide == false) { pm.dynamicFriction = 0.6f; }


        if(Input.GetMouseButtonDown(0) && GameManager.Instamce._gameState == GameManager.GameState.game&& state2!= VerticalState.air)
        {
            if (state2 == VerticalState.wall) 
            {
                rb.AddForce(jumoForce * Time.deltaTime * new Vector3(0, 1, 0));
                
            }
            else
            {
                if (slide) { slide = false; }
                if (state2 == VerticalState.wall)
                {

                    if (state == HorizontalState.left) state = HorizontalState.right;
                    else { state = HorizontalState.left; }
                }

                switch (state)
                {
                    case HorizontalState.left:
                        rb.AddForce(jumoForce * Time.deltaTime * new Vector3(-0.1f, 1, 0));
                        break;
                    case HorizontalState.right:
                        rb.AddForce(jumoForce * Time.deltaTime * new Vector3(0.1f, 1, 0));

                        break;

                }
            }
        }


        // move on the ground

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
