using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colorchange : MonoBehaviour
{
    public int globesDamage = 10;
    public float damage = 1.0f;
    public Color deadColor;

    private float counter=1000;
    private bool dead = false;
    public SpriteRenderer renderer;
    

    //Reduces health of player by one when collision the bullet
    //Makes player animator boolean dead true if health is gone
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "bullet")
        {
            counter = 0;
        }

        
    }

    private void Update()
    {
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
        counter = 0;
    }


}

