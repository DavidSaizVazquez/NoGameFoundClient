using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using NoGameFoundClient;
using UnityEditor.Experimental;
using UnityEngine;

public class SpecialAbility : MonoBehaviour
{

    public int ability = 0;
    public float rezRecharge=5;
    public ParticleSystem rezParticles;
    public ParticleSystem healParticles;
    public Camera camera;
    public HealthHelp _healthHelp;
    
    
    private float counter=0;
    private ServerConnection server;
    private pointAndShoot _shoot;
    
    
    // Start is called before the first frame update
    void Start()
    {
        server = NoGameFoundClient.ServerConnection.getInstance();
        rezParticles.enableEmission = false;
        healParticles.enableEmission = false;
        _shoot = camera.GetComponent<pointAndShoot>();
        ability = globalData.ability;
    }

    // Update is called once per frame
    // TODO:debug, currently working in another thing
    void Update()
    {
        if (counter > rezRecharge)
        {
            if (Input.GetButtonDown("Special"))
            {
                counter = 0;
                switch (ability)
                {
                    case 0:
                        resurrect();
                        break;
                    case 1:
                        shieldBullet();
                        break;
                    case 2:
                        heal();
                        break;
                    default:
                        Debug.Log("not an ability");
                        break;
                }
               
            }
        }
    }

    private void FixedUpdate()
    {
        counter += Time.fixedDeltaTime;
    }
    private void resurrect()
    {
        rezParticles.enableEmission = true;
        rezParticles.Play();
        server.SendMessage("30/");
    }
    private void heal()
    {
        healParticles.enableEmission = true;
        healParticles.Play();
        _healthHelp.addHealth();
        server.SendMessage("33/");
    }

    private void shieldBullet()
    {
        _shoot.fireShieldBullet();
    }
}
