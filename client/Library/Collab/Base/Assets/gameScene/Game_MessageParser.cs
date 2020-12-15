using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using NoGameFoundClient;
using UnityEditor;

public class Game_MessageParser : MonoBehaviour
{
    public GameObject npcPrefab;
    public Vector2 position;
    
    Dictionary<string, GameObject> npcs = new Dictionary<string, GameObject>();
    
    ServerConnection serverConnection = ServerConnection.getInstance();
    // Start is called before the first frame update
    void OnLevelWasLoaded()
    {
        
        foreach (var user in globalData.gameUserList)
        {
            npcs.Add(user,Instantiate(npcPrefab));
            npcs[user].transform.position = position;
            npcs[user].GetComponent<npc_online>().setName(user);
        }
        listenForServer();
    }

    async private void listenForServer()
    {
        while (true)
        {
            string serverResponse = await Task.Run(() => serverConnection.ListenForMessage());


            string[] commands = serverResponse.Split('~');

            int i = 0;
            while (commands[i] != "")
            {
                int prefix = int.Parse(commands[i].Split('/')[0]);
                Debug.Log(serverResponse);
                switch (prefix)
                {
                    //Other player's position update
                    case 20:
                        npcs[commands[0]].GetComponent<npc_online>().updateCharacter(float.Parse(commands[1]),
                            float.Parse(commands[2]),
                            int.Parse(commands[3]),
                            int.Parse(commands[4]));
                        break;
                   
                    case 21:
                      
                        break;
                    case 22:
                        break;
                    case 23:
                        break;
                       
                    case 24:
                       
                        break;
                  
                }



                i++;
            }
        }

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
