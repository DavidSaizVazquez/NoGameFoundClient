using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using NoGameFoundClient;
using System.Linq;


namespace LoginUI
{
    //Script that controls the game creation window
    public class PersonalBoardLogic : MonoBehaviour
    {

        public Canvas personalBoardCanvas;
        private ServerConnection serverConnection;
        public GameObject textTemplate;
        public Button scoreBoardButton;
        private ScoreBoardLogic scoreBoardLogic;

      
        // Start is called before the first frame update
        //It gets all the unity objects needed on this script, makes the menu invisible
        //Get an instance of the server connection
        //set the button events
        void Start()
        {
            personalBoardCanvas.enabled = false;
            serverConnection = ServerConnection.getInstance();
            
            
            GameObject scoreBoardWindow = GameObject.Find("ScoreboardWindow");
            scoreBoardLogic = scoreBoardWindow.GetComponent<ScoreBoardLogic>();
            scoreBoardButton.onClick.AddListener(ScoreBoardClick);
            
        }
        
        private void ScoreBoardClick()
        {
            personalBoardCanvas.enabled = false;
            scoreBoardLogic.scoreBoardCanvas.enabled = true;
            
        }
        

        //Function that updates the displayed users that joined a game to a list.
        public void usersPlayedWithNotificationListen(string serverResponse)
        {
            List<string> names = serverResponse.Replace("17/", "").Split(',').ToList<string>();
            Transform[] allChildren = textTemplate.transform.parent.GetComponentsInChildren<Transform>();

            foreach (Transform child in allChildren)
            {
                if (!(child.gameObject.name == "Text" || child.gameObject.name == "Content")) Destroy(child.gameObject);
            }

            List<string> uniqueNames = names.Distinct().ToList();

            foreach (string name in uniqueNames)
            {
                if (name != "")
                {
                    GameObject newText = Instantiate(textTemplate) as GameObject;
                    
                    newText.name="user";

                    newText.SetActive(true);
                    newText.GetComponent<Text>().text = name;
                    newText.transform.SetParent(textTemplate.transform.parent, false);
                }
            }
        }


     
        

    }
}
