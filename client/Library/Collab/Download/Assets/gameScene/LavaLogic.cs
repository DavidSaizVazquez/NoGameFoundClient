using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaLogic : MonoBehaviour
{
    public GameObject player;
    private playerMovement playerScript;
    private bool firstTime = true;

    private void Start()
    {
        playerScript = player.GetComponent<playerMovement>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && firstTime)
        {
            playerScript.remoteKill();
            firstTime = false;
        }
        
    }

}
