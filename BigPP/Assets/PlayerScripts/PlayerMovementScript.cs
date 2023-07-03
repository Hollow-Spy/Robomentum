using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    public bool isLocked;

    CharacterController CharController;
    public float speed;
    public float jumpHeight = 3f;

    float momentum = 1;
    public float momentumStat = .1f;


    public float gravity = -9.81f;

    public bool isSprinting;
    public float sprintmultiplier = 1.5f;
    public float slidemultiplier = 1.5f;

    Vector3 velocity;

    public Transform groundcheck;
    public float groundDistance = .4f;
    public LayerMask groundMask;
    bool isCoyote;
    public float coyoteTime = .2f;
    public bool isCrounching;

    float ogheight;
    public float crounchHeight;

    public bool isSliding;
    Vector3 sliderVector;

    public bool isGrounded;
    bool stillgrounded;

    public AudioSource sound;
    public AudioClip landsound, jumpsound;

    public JumpBufferScript jumpbuffer;

    public int MaxJumps = 1;
    int jumpquant;

    // Start is called before the first frame update
    void Start()
    {


        CharController = GetComponent<CharacterController>();
        ogheight = CharController.height;
    }

    public void ledgecheck()
    {
        isGrounded = Physics.CheckSphere(groundcheck.position, groundDistance, groundMask);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocked)
        {

            if (velocity.y < 0)
            {
                isGrounded = Physics.CheckSphere(groundcheck.position, groundDistance, groundMask);

            }
            if (!isGrounded && velocity.y < -4 && jumpquant > 0)
            {
                isCoyote = true;
                StartCoroutine("coyoteNumerator");
            }


            if (isGrounded && jumpbuffer.holdingjump)
            {
                Jump();
            }

            if (isGrounded && velocity.y < 0)
            {
                StopCoroutine("coyoteNumerator");

                velocity.y = -4f;
            }

            if (isGrounded && !stillgrounded && velocity.y < 0)
            {
                jumpquant = MaxJumps;

                sound.clip = landsound;
                sound.Play();

                jumpbuffer.buffready = false;
                stillgrounded = true;
                momentum = 1;
            }


            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");



            if (Input.GetKey(KeyCode.LeftShift) && !isCrounching && !isSliding && CharController.height > crounchHeight)
            {


                StopCoroutine("sprinteNumerator");
                isSprinting = true;
            }
            else
            {

                StartCoroutine("sprinteNumerator");

            }

            if (Input.GetKey(KeyCode.LeftControl) && isSprinting && Input.GetKey(KeyCode.W) && !isSliding)
            {
                isSliding = true;

                StartCoroutine("slidingNumerator");
            }





            if (Input.GetKey(KeyCode.LeftControl) && !isSliding)
            {
                isCrounching = true;
            }
            else
            {
                isCrounching = false;
            }



            if (isSliding)
            {
                transform.forward = sliderVector;
                if (z < 0)
                {
                    z = 1;
                }

                x *= .1f;
            }

            Vector3 move = transform.right * x + transform.forward * z;

            if (isSprinting)
            {
                move *= sprintmultiplier;
            }

            if (isGrounded && isCrounching)
            {
                move /= 2;
                CharController.height = crounchHeight;
            }
            else
            {
                if (!isSliding)
                {
                    CharController.height = ogheight;

                }

            }

            CharController.Move(move * speed * momentum * Time.deltaTime);





            if (Input.GetButtonDown("Jump") && (isGrounded || isCoyote))
            {
                jumpquant--;

                isCoyote = false;
                Jump();
            }




            velocity.y += gravity * Time.deltaTime;

            CharController.Move(velocity * Time.deltaTime);

        }
        else
        {
            StopAllCoroutines();
        }

    }

    IEnumerator coyoteNumerator()
    {
        yield return new WaitForSeconds(coyoteTime);
        isCoyote = false;
    }

    IEnumerator sprinteNumerator()
    {
        yield return new WaitForSeconds(0.2f);
        isSprinting = false;
    }

    IEnumerator slidingNumerator()
    {
        CharController.height = crounchHeight * .5f;

        float ogspeed;
        ogspeed = speed;

        speed *= slidemultiplier;
        sliderVector = transform.forward;
        while (isSliding == true)
        {
            speed -= Time.deltaTime;

            yield return null;

            if (speed < ogspeed / 1.6f || Input.GetKeyUp(KeyCode.LeftControl) || Input.GetKeyUp(KeyCode.LeftShift))
            {
                isSliding = false;
            }
        }
        float bonusspeed = (speed - ogspeed) * .25f;

        momentum += bonusspeed;

        speed = ogspeed;





    }



    public void Jump()
    {
        isGrounded = false;

        jumpbuffer.buffready = false;

        momentum += momentumStat;


        stillgrounded = false;

        sound.clip = jumpsound;
        sound.Play();
        velocity.y = Mathf.Sqrt(jumpHeight + -2f * gravity);
    }

}
