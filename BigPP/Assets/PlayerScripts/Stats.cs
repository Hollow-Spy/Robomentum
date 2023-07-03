using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float Speed;
    public float JumpHeight;
    public float SlideSpeed;
    public float ClimbSpeed;
    public float SprintSpeed;
    public float Momentum;

    public PlayerMovementScript playermov;
    private void Start()
    {
        Speed = playermov.speed;
        JumpHeight = playermov.jumpHeight;
        SlideSpeed = playermov.slidemultiplier;
        //ClimbSpeed = playermov.GetComponentInChildren<wallclimb>().ClimbSpeed;
        SprintSpeed = playermov.sprintmultiplier;

        Momentum = playermov.momentumStat;


    }

    private void Update()
    {
        playermov.speed = Speed;
        playermov.jumpHeight = JumpHeight;
        playermov.slidemultiplier = SlideSpeed;
        //playermov.GetComponentInChildren<wallclimb>().ClimbSpeed = ClimbSpeed;
        playermov.sprintmultiplier = SprintSpeed;
        playermov.momentumStat = Momentum;
    }


}
