using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ledgegrabscript : MonoBehaviour
{
    public PlayerMovementScript playermov;
    float oldgravity;
    bool isClimbing;
    Collider ledge;

    private void Start()
    {
        oldgravity = playermov.gravity;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Ledge") && Input.GetButton("Jump") && !isClimbing)
        {
            isClimbing = true;
            ledge = other;
            playermov.gravity = 0;
            playermov.GetComponent<CharacterController>().Move(Vector3.zero);

            playermov.isLocked = true;
            StopCoroutine("climbing");
            StartCoroutine("climbing");
          
            if(playermov.GetComponentInChildren<wallclimb>().isclimbing )
            {
                playermov.GetComponentInChildren<wallclimb>().isclimbing = false;
                playermov.GetComponentInChildren<wallclimb>().locked = true;

            }
        }

    }

    IEnumerator climbing()
    {

        yield return new WaitForSeconds(.2f);
       

        while(Input.GetButton("Jump") && !playermov.isGrounded && playermov.gameObject.transform.position.y < ledge.transform.position.y+4 && isClimbing)
        {
            playermov.GetComponent<CharacterController>().Move(new Vector3(0,5 * Time.deltaTime,0));

          
            playermov.ledgecheck();
            yield return null;
        }
      

        playermov.gravity = oldgravity;
        playermov.isLocked = false;
        isClimbing = false;
        playermov.GetComponentInChildren<wallclimb>().locked = false;

    }
}
