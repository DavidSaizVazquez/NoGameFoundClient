using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using NoGameFoundClient;
using UnityEngine;
using UnityEngine.SceneManagement;

public class quit : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (globalData.hostUser)
        {
            ServerConnection.getInstance().SendMessage("32/"+(int)globalData.Score+"~");
            
        }

        globalData.GamePaused = true;
        Cursor.visible = true;
        SceneManager.LoadScene("LoginScene/LoginGAME", LoadSceneMode.Single);
    }
    
}

