using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sendBossPosition : MonoBehaviour
{
    // Start is called before the first frame update
    private NoGameFoundClient.ServerConnection serverConnection;

    public float refreshPos = 0.1f;
    private float counter = 0;
    
    
    void Start()
    {
        serverConnection = NoGameFoundClient.ServerConnection.getInstance();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        counter += Time.deltaTime;
        if (counter > refreshPos)
        {
            string message = "22/";
            message += gameObject.transform.position.y;
            message += "~";
            serverConnection.SendMessage(message);
            counter = 0;
        }
    }
}
