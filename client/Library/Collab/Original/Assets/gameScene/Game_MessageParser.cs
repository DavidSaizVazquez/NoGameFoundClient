﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using NoGameFoundClient;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game_MessageParser : MonoBehaviour
{
    public GameObject npcPrefab;
    public Camera mainCamera;
    public GameObject player;
    public HealthHelp hpbar;
    public Text text;
    public Vector2 position;

    private set_npc_boss_position _setNpcBossPosition;

    public teamworkPoints teamwork;
    public Animator npcBossAnimator;
    public float rezRadius;
    
    
    Dictionary<string, GameObject> npcs = new Dictionary<string, GameObject>();
    
    ServerConnection serverConnection = ServerConnection.getInstance();

    private string[] msg;

    private Vector3 p;

    private Vector3 velocity;
    
    void Awake()
    {
        Debug.Log("Awake");
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    // Start is called before the first frame update
    //Starts all npc players
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (!globalData.hostUser)
        {
            _setNpcBossPosition = GameObject.Find("NPC_Boss").GetComponent<set_npc_boss_position>();
        }

        Debug.Log("Game_MessgeParser Loaded!");
        if (globalData.gameUserList != null)
        {
            foreach (var user in globalData.gameUserList)
            {
                if (!user.Equals(globalData.userName) && !user.Equals(""))
                {
                    npcs.Add(user, Instantiate(npcPrefab));
                    npcs[user].transform.position = position;
                    npcs[user].GetComponent<npc_online>().setName(user);
                    teamwork.addNPC(user);
                }
            }
        }

        gameObject.GetComponent<allDead>().chargePlayers();
        listenForServer();
    }

    //on message recieved from server, process it and executes command
    async private void listenForServer()
    {
        while (true)
        {
            try
            {
 string serverResponse = await Task.Run(() => serverConnection.ListenForMessage());
            
            string[] commands = serverResponse.Split('~');
            int i = 0;
            while (commands[i] != "")
            {
                int prefix = int.Parse(commands[i].Split('/')[0]);
                switch (prefix)
                {
                    //Other player's position update
                    case 20:
                        msg = commands[i].Replace("20/", "").Split('/');
                        if (msg.Length < 5)
                        {
                            break;
                        }
                        try
                        {
                            npcs[msg[0]].GetComponent<npc_online>().updateCharacter(
                                float.Parse(msg[1]),
                                float.Parse(msg[2]),
                                int.Parse(msg[3]),
                                float.Parse(msg[4]));

                            teamwork.updateNPCForGlobes(msg[0], float.Parse(msg[1]), float.Parse(msg[2]));
                        }
                        catch (Exception e)
                        {
                            Debug.Log(e);
                        }

                        break;
                   case 21:
                       msg = commands[i].Replace("21/", "").Split('/');
                       if (msg.Length < 5)
                       {
                           break;
                       }
                       p=new Vector3(float.Parse(msg[1]),float.Parse(msg[2]));
                       velocity= new Vector3(float.Parse(msg[3]),float.Parse(msg[4]));
                       mainCamera.GetComponent<pointAndShoot>().fireOnlineBullet(velocity,p,0);
                       break;
                   
                   
                   case 22:
                       _setNpcBossPosition.setPosition(commands[i].Replace("22/", ""));
                       break;
                   
                   
                   case 23:
                    
                       msg = commands[i].Replace("23/", "").Split('/');

                       if (int.Parse(msg[0].Split(',')[0]) == 1) // bullet
                       { 
                           string[] parameters = msg[0].Split(',');
                           _setNpcBossPosition.fireBossBullet(new Vector3(float.Parse(parameters[1]), float.Parse(parameters[2])));
                       }

                       if (int.Parse(msg[1]) == 1) // tongue
                       {
                           if (globalData.level == 1)
                           {
                               npcBossAnimator.SetBool("shootTongue", true);    
                           }
                           else
                           {
                               npcBossAnimator.SetBool("meteorAttack", true);
                           }
                           
                       }
                            
                       break;

                case 24:
                    npcBossAnimator.SetBool("die",true);
                    text.GetComponent<Animator>().Play("over");
                    break;
                case 25:
                    teamwork.globesOnNpc(commands[i].Replace("25/", ""));
                    break;
                    
                   case 30:
                       msg = commands[i].Replace("30/", "").Split('/');
                       float dx = npcs[msg[0]].transform.position.x - player.transform.position.x;
                       float dy = npcs[msg[0]].transform.position.y - player.transform.position.y;
                       if (rezRadius > Math.Sqrt(dx * dx + dy * dy))
                       {
                           player.GetComponent<Animator>().SetBool("Dead",false);
                           hpbar.restartHealth();
                       }

                       npcs[msg[0]].GetComponent<npc_online>().rez();
                       break;
                   case 31:
                       msg = commands[i].Replace("31/", "").Split('/');
                       if (msg.Length < 5)
                       {
                           break;
                       }
                       p=new Vector3(float.Parse(msg[1]),float.Parse(msg[2]));
                       velocity= new Vector3(float.Parse(msg[3]),float.Parse(msg[4]));
                       mainCamera.GetComponent<pointAndShoot>().fireOnlineShieldBullet(velocity,p,0);
                       break;
                   
                }
                i++;
            }
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
           
        }
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    //executed when application is quited
    void OnApplicationQuit()
    {
        Debug.Log("Application ending after " + Time.time + " seconds");
        serverConnection.SendMessage("0/");
    }

    //executed when scene is destroyed
    void OnDestroy()
    {
        Debug.Log("Destroyed...");
    }
    
    
}
