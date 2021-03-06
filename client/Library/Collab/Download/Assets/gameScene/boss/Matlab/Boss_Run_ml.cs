﻿using System.Collections;
using System.Collections.Generic;
using NoGameFoundClient;
using UnityEngine;

public class Boss_Run_ml : StateMachineBehaviour
{

    public float speed = 2.5f;
    public float InitialY;
    public float oscillation;
    public float bulletRate;
    public float countMax;
    private float counter=0;
    private ServerConnection serverConnection = ServerConnection.getInstance();


    float t=0;

    Transform player;
    Rigidbody2D rb;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player= GameObject.FindGameObjectWithTag("Player").transform;
        rb = GameObject.FindGameObjectWithTag("boss").GetComponent<Rigidbody2D>();

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        t += Time.deltaTime;
        rb.transform.position = new Vector3(rb.transform.position.x, oscillation * Mathf.Sin(t) + InitialY, rb.transform.position.z);
        if (Random.Range(0, 100) > bulletRate)
        {            //if (counter < countMax && !animator.GetBool("shootTongue"))
            if (counter < countMax)
            {
                animator.SetBool("shootBullet",true);
                counter+=Time.deltaTime;
            }
            else
            {
                animator.SetBool("meteorAttack",true);
                string message = "23/0/1~";
                serverConnection.SendMessage(message);
                counter = 0;
            }
        }
    }


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
