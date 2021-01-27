using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NoGameFoundClient;
using UnityEditor;
using UnityEditor.Experimental;
using UnityEngine;
using UnityEngine.SceneManagement;

public class teamworkPoints : MonoBehaviour
{


    private bool isHost = false;  
    private bool dead = false;
    private float clock;
    public GameObject hostPlayer;
    public float waitTime;
    public float timeToGetThere;
    private float activeClock;
    private int players=0;
    private List<GameObject> globes;
    public float minDistanceFromGlobe;
    Dictionary<string, List<float>> npcs = new Dictionary<string, List<float>>();
    private GameObject[] orderedGlobeList;

    private bool on=false;
    private List<Boolean> detectors;
    private BossHealth boss;

    void Start()
    {
        isHost = globalData.hostUser;
        if(isHost)boss=GameObject.Find("Boss").GetComponent<BossHealth>();
        GameObject[] buff = {GameObject.Find("Globe1"), GameObject.Find("Globe2"), GameObject.Find("Globe3"), GameObject.Find("Globe4")};
        orderedGlobeList = buff;
        
        globes = buff.OrderBy(a => Guid.NewGuid()).ToList();
        
        foreach (GameObject globe in globes)
        {
            globe.SetActive(false);
        }
        detectors = new List<Boolean>();
        players = globalData.gameUserList.Length;
        for (int i = 0; i < players; i++)
        {
            detectors.Add(false);
        }
    }

    void Update()
    {
        activeClock+= Time.deltaTime;
        clock += Time.deltaTime;
        if (isHost)
        {
            if (clock > waitTime && !on)
            {
                Debug.Log("STAAAAAART************");
                startTeamWork();
                clock = 0;
                activeClock = 0;
                on = true;
            }
            if (activeClock > timeToGetThere && on)
            {
                on = false;
                clock = 0;
                activeClock = 0;
                turnOffGlobes();


                if (checkIfPlayersAreOnGlobes())
                {
                    generateDamage();
                }
                Debug.Log("END");
                

                for (int i = 0; i < detectors.Count; i++)
                {
                    detectors[i] = false;
                }
                
                globes = globes.OrderBy(a => Guid.NewGuid()).ToList();
                
            }
        }
        else
        {
            if (activeClock > timeToGetThere && on)
            {
                turnOffGlobes();
                on = false;
            }

        }
    }

    void startTeamWork()
    {
        string ids = "25/";
        for (int i = 0; i < players; i++)
        {
            int c = 0;
            globes[i].SetActive(true);
            ids += (int.Parse(globes[i].name.Replace("Globe",""))-1).ToString();
            ids += ",";

        }

        ids = ids.Substring(0, ids.Length - 1);
        ids += "~";
        
        ServerConnection.getInstance().SendMessage(ids);
    }

    bool checkIfPlayersAreOnGlobes()
    {
        playerOnAGlobe(hostPlayer.GetComponent<Transform>().position.x,
            hostPlayer.GetComponent<Transform>().position.y);
        if (players > 1)
        {
            foreach (var item in npcs.Values)
            {
                playerOnAGlobe(item[0], item[1]);
            }
        }
        
        foreach (var res in detectors)
        {
            if (!res)
            {
                return false;
            }
        }

        return true;
    }

    void playerOnAGlobe(float x,float y)
    {
        for (int i = 0; i < players; i++)
        {
            GameObject globe = globes[i];
            if (Math.Abs(globe.GetComponent<Transform>().position.x - x) < minDistanceFromGlobe && Math.Abs(globe.GetComponent<Transform>().position.y - y) < minDistanceFromGlobe)
            {
                detectors[i] = true;
            }
        }
    }

    public void addNPC(String user)
    {
        List<float> buffer = new List<float>(){0,0};
        npcs.Add(user, buffer);

    }
    public void updateNPCForGlobes(String user, float x, float y)
    {
        List<float> buffer = new List<float>();
        buffer.Add(x);
        buffer.Add(y);
        npcs[user]= buffer;
    }

    void generateDamage()
    {
        boss.GlobesDamage();
        Debug.Log("OMG HIT BOOS WOHOO YEAH ");
    }

    public void globesOnNpc(String globeNames)
    {
        activeClock = 0;
        on = true;
        string[] globeIds = globeNames.Split(',');
        foreach (var globeid in globeIds)
        {
            orderedGlobeList[int.Parse(globeid)].SetActive(true);
        }

    }

    void turnOffGlobes()
    {
        foreach (GameObject globe in globes)
        {
            globe.SetActive(false); 

        }
    }





}
