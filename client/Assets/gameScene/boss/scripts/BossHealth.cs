using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using NoGameFoundClient;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    public int health;
    public int globesDamage = 10;
    public Animator animator;
    public Text text;
    public float damage = 1.0f;
    public Color deadColor;

    private float counter=1000;
    private ServerConnection serverConnection = ServerConnection.getInstance();
    private bool dead = false;
    public SpriteRenderer renderer;
    

    //Reduces health of player by one when collision the bullet
    //Makes player animator boolean dead true if health is gone
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "bullet")
        {
            health--;
            counter = 0;
        }

        
    }

    private void Update()
    {
        if (health < 0 && dead==false)
        {
            dead = true;
            animator.SetBool("die",true);
            if(globalData.gameUserList!=null)globalData.Score += 1000/globalData.gameUserList.Length;
            string msg = "24/~";
            serverConnection.SendMessage(msg);
            text.GetComponent<Animator>().Play("victory");
            Task.Delay(500);
            serverConnection.SendMessage(msg);
            Task.Delay(500);
            serverConnection.SendMessage(msg);
            
        }

        counter += Time.deltaTime;
        if (counter < damage)
        {
            renderer.color = deadColor;
        }
        else
        {
            renderer.color= Color.white;
        }
    }

    public void GlobesDamage()
    {
        health -= globesDamage;
        counter = 0;
        string msg = "26/~";
        serverConnection.SendMessage(msg);
    }


}
