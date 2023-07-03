using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallclimb : MonoBehaviour
{
   public bool isclimbing;
    public PlayerMovementScript playermov;
    float OldGravity;
    CharacterController charcontroller;
    public float ClimbSpeed = 2;
    public bool locked;

    // Start is called before the first frame update
    void Start()
    {
        OldGravity = playermov.gravity;
    }

    private void OnTriggerEnter(Collider other)
    {
       
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("WallClimb") && Input.GetButton("Jump") && !isclimbing && !locked)
        {
            isclimbing = true;
            playermov.gravity = 0;
            playermov.isLocked = true;
            playermov.isGrounded = false;
            charcontroller = playermov.GetComponent<CharacterController>();
            charcontroller.Move(Vector3.zero);
            


        }
    }

    private void OnTriggerExit(Collider other)
    {
       if (other.gameObject.layer == LayerMask.NameToLayer("WallClimb"))
        {
            isclimbing = false;

        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!isclimbing && Input.GetButtonDown("Jump"))
        {
            playermov.gravity = OldGravity;
            playermov.isLocked = false;
            isclimbing = false;
        }
        if(isclimbing)
        {
           

            if (Input.GetKey(KeyCode.W))
            {
                
                charcontroller.Move(new Vector3(0, ClimbSpeed * Time.deltaTime, 0));
            }
            if (Input.GetKey(KeyCode.S))
            {
                charcontroller.Move(new Vector3(0, -ClimbSpeed * Time.deltaTime, 0));

            }
           
        }
    


    }
}
