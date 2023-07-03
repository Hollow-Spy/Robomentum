using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBufferScript : MonoBehaviour
{
    public LayerMask groundMask;
    public bool buffready;
    public PlayerMovementScript playermov;
    BoxCollider thecollider;

    public bool holdingjump;


    void Start()
    {
        thecollider = GetComponent<BoxCollider>();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {

            buffready = true;
        }


    }


    // Update is called once per frame
    void Update()
    {
        if (!playermov.isGrounded && Input.GetButtonDown("Jump") && buffready)
        {
            holdingjump = true;

        }
        if (Input.GetButtonUp("Jump"))
        {
            holdingjump = false;
        }

    }
}
