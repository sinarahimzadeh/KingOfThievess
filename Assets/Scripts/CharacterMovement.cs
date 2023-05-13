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
    [SerializeField] private Vector3 rbVelLimit;
    public  float speed, jumoForce;
    [SerializeField] bool slide;
    public PhysicMaterial pm;
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;

    }
    void Start()
    {
        Camera.main.aspect = 18 /9;
        rb = GetComponent<Rigidbody>();
        instance = this; 
        state = HorizontalState.right; 
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.transform.tag == "wall"&&collision.gameObject.layer == 6)
        {
            foreach (ContactPoint contact in collision.contacts)
            {
                GameManager.Instamce.Score = contact.otherCollider.bounds.size.y;

                if (contact.normal == Vector3.up)

                { state2 = VerticalState.ground; slide = false; speed = GameManager.Instamce.originalSpeed; }
                else if (contact.normal == Vector3.down) 
                {
                    break;
                }
                else if (contact.point.y<=(collision.transform.position.y-(contact.otherCollider.bounds.size.y/2))+0.6f && collision.transform.name != "airwall")
                {
                    state2 = VerticalState.wallben;

                   // slide = false;
                    speed = 0;
                }
                else
                {
                    state2 = VerticalState.wall;
                    slide = true;
                }
                print(contact.otherCollider + contact.point.ToString() + contact.normal.ToString());
                // Visualize the contact point
                //  Debug.DrawRay(contact.point, contact.normal, Color.white);
            }
        }
       

        // when it is on the ground
        else if (collision.gameObject.layer == 6 && collision.transform.tag == "ground")
        { state2 = VerticalState.ground;slide = false;
        }
        
    }
   
    private void OnCollisionEnter(Collision collision)
    {
  
        if (collision.gameObject.layer == 6&&collision.transform.tag=="wall")
        {

            foreach (ContactPoint contact in collision.contacts)
            {
                GameManager.Instamce.Score = contact.otherCollider.bounds.size.y;

                if (contact.normal == Vector3.up)

                { state2 = VerticalState.ground; slide = false; speed = GameManager.Instamce.originalSpeed; }
                else if (contact.normal == Vector3.down)
                {
                    break;
                }
                else if (contact.point.y <= (collision.transform.position.y - (contact.otherCollider.bounds.size.y / 2)) + 0.6f&&collision.transform.name!="airwall")
                {
                    state2 = VerticalState.wallben;
                    rb.velocity = Vector3.zero;
                    slide = false;
                    speed = 0;
                }
                else
                {
                    state2 = VerticalState.wall;
                    slide = true;
                }
              //  print(contact.otherCollider + contact.point.ToString() + contact.normal.ToString());
                // Visualize the contact point
                //  Debug.DrawRay(contact.point, contact.normal, Color.white);
            }

        }

    }
    private void OnCollisionExit(Collision collision)
    {
        // how the jump is detected so no second jump is possible in the air
        if (collision.gameObject.layer == 6)
        { state2 = VerticalState.air; }
        if (state2 == VerticalState.wall && collision.transform.tag == "wall") 
        {
            slide = false;
        }

    }
    // Update is called once per frame
    void Update()
    {
        if (slide == true) { pm.dynamicFriction = 5; }
        if (slide == false) { pm.dynamicFriction = 0.6f; }
        //  if (rb.velocity.y > rbVelLimit.y) { rb.velocity = new Vector3(rb.velocity.x,rbVelLimit.y,rb.velocity.z); }
        if (state2 == VerticalState.ground) { speed = GameManager.Instamce.originalSpeed; }
        if (Input.GetMouseButtonDown(0) && GameManager.Instamce._gameState == GameManager.GameState.game&& state2!= VerticalState.air)
        {
            if (state2 == VerticalState.wallben) 
            {
                rb.AddForce(jumoForce * new Vector3(0, .5f, 0));
                state2 = VerticalState.wall;
                
            }
            else
            {
                if (slide) { slide = false; }
                if (state2 == VerticalState.wall)

                {

                    if (state == HorizontalState.left) state = HorizontalState.right;
                    else { state = HorizontalState.left; }

                    speed = GameManager.Instamce.originalSpeed;
                }

                switch (state)
                {
                    case HorizontalState.left:
                        rb.AddForce(jumoForce  * new Vector3(-0.2f, .5f, 0));
                        break;
                    case HorizontalState.right:
                        rb.AddForce(jumoForce  * new Vector3(0.2f, .5f, 0));

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
