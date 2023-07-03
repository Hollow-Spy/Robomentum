using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpScript : MonoBehaviour
{
    public int rarity;

  

    PlayerStats stat;

    private void Start()
    {
    stat = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();

    }

    public void setRarity(int rare)
    {
        rarity = rare;
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player") )
        {
            int random = Random.Range(0, 6);

           switch(random)
            {
                case 0:
                    stat.Speed += .2f * rarity;
                    Debug.Log("speed");

                    break;
                case 1:
                    Debug.Log("jump");

                    stat.JumpHeight += .2f * rarity;
                    break;
                case 2:
                    Debug.Log("slie");

                    stat.SlideSpeed += .3f * rarity;

                    break;
                case 3:
                    Debug.Log("climb");

                    stat.ClimbSpeed += .2f * rarity;

                    break;
                case 4:
                    Debug.Log("sprint");

                    stat.SprintSpeed += .1f * rarity;

                    break;
                case 5:
                    Debug.Log("momentum");
                    stat.Momentum += .1f * rarity;

                    break;
                    
            }
            Destroy(gameObject);
        }
    }
}
