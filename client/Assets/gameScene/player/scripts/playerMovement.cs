﻿﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class playerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;
    public float runSpeed = 40f;
    public GameObject ghost;
    public Camera camera;
    public HealthHelp health;
    public GameObject dashObject;
    public Animator windAnimator;
    public float dashDelay;
    private bool wasDashing = false;
    
    float horizontalMove = 0f;
    bool jump = false;
    bool crouch = false;
    bool dash = false;
    private float clock;
    private float timeDashing = 0f;

    private bool remoteKilling = false;

    void Start(){
        ghost.GetComponent<Renderer>().enabled=false;
        clock = dashDelay;
    }

    // Detects keys clicked by user (PLAYER 1) up and down keys and updates animator states which updates visible animation onscreen
    // Update is called once per frame
    void Update()
    {
        if (!globalData.GamePaused)
        {
            
        
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        animator.SetFloat("Horizontal",Input.GetAxisRaw("Horizontal"));
        clock += Time.deltaTime;

        if (Input.GetButtonDown("Jump"))
        {
            animator.SetBool("Jumping",true);
            jump = true;
        } else if (Input.GetButtonUp("Jump"))
        {
            animator.SetBool("Jumping",false);
        }

        if (Input.GetButtonDown("Crouch"))
        {
            animator.SetBool("Crouch",true);
            crouch = true;
        }else if (Input.GetButtonUp("Crouch"))
        {
            crouch = false;
            animator.SetBool("Crouch",false);
        }
        
        if (Input.GetButtonDown("Dash"))
        {
            if (clock > dashDelay)
            {
                wasDashing = true;
                dash = true;
                animator.SetBool("Dash",true);
                clock = 0;
            }
            else
            {
                dash = false;
                //animator.SetBool("Dash",false);
            }

        }

        if (dash){timeDashing += Time.deltaTime;}
        if (timeDashing > 0.25)
        {
            dash = false;
            clock = 0;
            timeDashing = 0;
           animator.SetBool("Dash",false);
        }
        
        //Kills character and displays flying ghost from corpse
        if((Input.GetButtonDown("Kill") || health.getHP() < 1 || remoteKilling) && !animator.GetBool("Dead")){
            animator.SetBool("Dead",true);
            ghost.GetComponent<Renderer>().enabled=true;            
            ghost.GetComponent<Transform>().position = new Vector3(transform.position.x,transform.position.y,transform.position.z);
            Vector3 v = ghost.GetComponent<Rigidbody2D>().velocity;
            v.y=3f;
            ghost.GetComponent<Rigidbody2D>().velocity= v;
        }
        }
    }

    //Moves character with controller
    //Removes ghost if it leavescamera bounds
    void FixedUpdate()
    {
        if(!animator.GetBool("Dead")){
            controller.Move(horizontalMove * Time.fixedDeltaTime,crouch,jump,dash ,animator);
        } else {
            Vector3 screenPosition = camera.WorldToScreenPoint(ghost.GetComponent<Transform>().position);
            if (screenPosition.y > Screen.height || screenPosition.y < 0)
                {
                    ghost.GetComponent<Renderer>().enabled=false;
                }
        }
        jump = false;
        
        
    }

    public void remoteKill()
    {
        remoteKilling = true;
    }
}
