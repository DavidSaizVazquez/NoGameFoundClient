using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using LoginUI;
using UnityEngine;
using UnityEngine.UI;
using NoGameFoundClient;
using UnityEngine.EventSystems;

//Script that controls the 'join a game' UI menu
public class JoinAGameLogic : MonoBehaviour
{
    

    public Canvas mainCanvas;
    public GameObject textTemplate;
    public Button joinButton;
    public Button mainMenuButton;

    private MainMenuLogic mainMenuLogic;
    
    private ServerConnection serverConnection;
   
    //Start is called before the first frame update
    //It gets all the unity objects needed on this script
    //Get an instance of the server connection
    //set the button events
    void Start()
    {
        GameObject mainMenuWindow = GameObject.Find("MainMenuWindow");
        mainMenuLogic = mainMenuWindow.GetComponent<MainMenuLogic>();
        serverConnection = ServerConnection.getInstance();
        joinButton.onClick.AddListener(JoinButtonClick);
        mainMenuButton.onClick.AddListener(MainMenuButtonClick);
    }

    //Function executed when the main menu button is clicked
    //Hides the current menu and shows the main menu
    private void MainMenuButtonClick()
    {
        mainCanvas.enabled = false;
        mainMenuLogic.mainMenuCanvas.enabled = true;
    }
    
    //Function executed when the join button is clicked
    //It gets the text of the selected item and sends a command to the server to join such game.
    private void JoinButtonClick()

    {
        Debug.Log("join button");
        Transform selected = textTemplate.transform.parent.Find("selected");

        if (selected != null)
        {
            string game = selected.gameObject.GetComponent<Text>().text;
            serverConnection.SendMessage("14/"+game);
        }
           
    }
    
    //Function called when a 13/command is received.
    //It updates the list of items displayed with all the parameters received in the command.
    //It sets the events to the new objecs
    public void gamesNotificationUpdate(string serverResponse)
        {
            List<string> names = serverResponse.Replace("13/", "").Split(',').ToList<string>();
            Transform[] allChildren = textTemplate.transform.parent.GetComponentsInChildren<Transform>();

            foreach (Transform child in allChildren)
            {
                if (!(child.gameObject.name == "Text" || child.gameObject.name == "Content")) Destroy(child.gameObject);
            }

            foreach (string name in names)
            {
                if (name != "")
                {
                    GameObject newText = Instantiate(textTemplate) as GameObject;

                    addEventTriggers(newText);

                    newText.name = "connectedUser";

                    newText.SetActive(true);
                    newText.GetComponent<Text>().text = name;
                    newText.transform.SetParent(textTemplate.transform.parent, false);
                }
            }
        }


        //Function to add the events to change text font and color on hover and on click
        public void addEventTriggers(GameObject newText)
        {

            newText.AddComponent(typeof(EventTrigger));
            EventTrigger trigger = newText.GetComponent<EventTrigger>();
            EventTrigger.Entry entry;

            entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerClick;
            entry.callback.AddListener((eventData) => { onClick(newText); });
            trigger.triggers.Add(entry);

            entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerEnter;
            entry.callback.AddListener((eventData) => { makeGrey(newText); });
            trigger.triggers.Add(entry);

            entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerExit;
            entry.callback.AddListener((eventData) => { makeBlack(newText); });
            trigger.triggers.Add(entry);

        }

        //Function that detects the selected item
        public void onClick(GameObject gameObject)
        {

            if (gameObject.name != "selected")
            {
                unSelect();
                gameObject.GetComponent<Text>().fontStyle = FontStyle.Bold;
                gameObject.GetComponent<Text>().fontSize = 17;
                gameObject.name = "selected";

                joinButton.enabled = true;
            }
            else
            {
                unSelect();

                joinButton.enabled = false;
            }

        }
        
        //Function that deselects an item
        public void unSelect()
        {
            Transform selected = textTemplate.transform.parent.Find("selected");

            if (selected != null)
            {
                selected.gameObject.GetComponent<Text>().fontSize = 14;
                selected.gameObject.GetComponent<Text>().fontStyle = FontStyle.Normal;
                selected.gameObject.name = "connectedUser";
            };


        }
    
        
        //Function that changes the color of the text to gray
        public void makeGrey(GameObject go)
        {
            go.GetComponent<Text>().color = Color.grey;
        }
        
        //Function that changes the color of the text to black
        public void makeBlack(GameObject go)
        {
            go.GetComponent<Text>().color = Color.black;
        }


}
