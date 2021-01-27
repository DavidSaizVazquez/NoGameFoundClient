using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class loadBoss_or_NPCboss : MonoBehaviour
{
    
    private GameObject boss;
    private GameObject npc_boss;
    
    
    // Start is called before the first frame update
    void OnLevelWasLoaded()
    {
        boss = GameObject.Find("Boss");
        npc_boss = GameObject.Find("NPC_Boss");
        if (globalData.hostUser)
        {
            npc_boss.SetActive(false);
        }
        else
        {
            boss.SetActive(false);
        }
    }
    

}
