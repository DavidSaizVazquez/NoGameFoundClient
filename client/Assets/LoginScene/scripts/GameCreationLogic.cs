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
    public class GameCreationLogic : MonoBehaviour
    {

        public Canvas gameCreationCanvas;
        public Animator levelAnimator;
        public Button mainMenuButton;
        public Button changeLevelButton;
        public Button playButton;
        public Canvas playButtonCanvas;
        public Button inviteButton;
        private ServerConnection serverConnection;
        public GameObject textTemplate;
        public GameObject textTemplate2;
        private MainMenuLogic mainMenuLogic;
        private LoginLogic loginLogic;

        private int selectedLevel = 1;
        public Canvas pythonCanvas;
        public Button pythonBtn;
        public Animator pythonAnimator;

        public Canvas matlabCanvas;
        public Button matlabBtn;
        public Animator matlabAnimator;


        public int gameNumber=-1;

        private bool selfInvite = false;

      
        // Start is called before the first frame update
        //It gets all the unity objects needed on this script, makes the menu invisible
        //Get an instance of the server connection
        //set the button events
        void Start()
        {
            inviteButton.enabled = false;

            GameObject mainMenuWindow = GameObject.Find("MainMenuWindow");
            mainMenuLogic = mainMenuWindow.GetComponent<MainMenuLogic>();
            GameObject loginWindow = GameObject.Find("LoginWindow");
            loginLogic = loginWindow.GetComponent<LoginLogic>();
            levelAnimator = gameCreationCanvas.GetComponent<Animator>();
            gameCreationCanvas.enabled = false;
            mainMenuButton.onClick.AddListener(MainMenuClick);
            changeLevelButton.onClick.AddListener(ChangeLevelButtonClick);
            playButton.onClick.AddListener(PlayButtonClick);
            inviteButton.onClick.AddListener(InviteButtonClick);
            serverConnection = ServerConnection.getInstance();


            pythonBtn.onClick.AddListener(PythonClick);
            pythonAnimator = pythonCanvas.GetComponent<Animator>();

            matlabBtn.onClick.AddListener(MatlabClick);
            matlabAnimator = matlabCanvas.GetComponent<Animator>();

            
       


        }
        
        
        //Function executed when Main menu button is clicked.
        //It sends a command to exit the game the user was in
        //Changes the UI to the main menu
        private void MainMenuClick()
        {
            gameCreationCanvas.enabled = false;
            mainMenuLogic.mainMenuCanvas.enabled = true;
            serverConnection.SendMessage("15/0");
            
        }

        
        //Executed when the change level button is clicked
        //Plays the animation to change the view to the selection of levels and and back.
        private void ChangeLevelButtonClick()
        {
            levelAnimator.SetBool("level", !levelAnimator.GetBool("level"));
            if(!levelAnimator.GetBool("level"))
            {
                pythonAnimator.SetBool("select", false);
                matlabAnimator.SetBool("select", false);
            }
            
            
            
        }
        
        //Function executed when the play button is clicked
        //Sends a command to start the game
        private void PlayButtonClick()
        {
			serverConnection.SendMessage("12/"+gameNumber+","+selectedLevel);
            Debug.Log("sending: " + "12/"+gameNumber+","+selectedLevel);
            globalData.hostUser = true;
            globalData.level = this.gameNumber;
        }

        
        //Function executed when the python logo button is clicked
        //Changes selected level
        private void PythonClick()
        {
            pythonAnimator.SetBool("select", true);
            matlabAnimator.SetBool("select", false);
            selectedLevel = 1;
        }
        
        //Function executed when the python logo button is clicked
        //Changes selected level
        private void MatlabClick()
        {
            matlabAnimator.SetBool("select", true);
            pythonAnimator.SetBool("select", false);
            selectedLevel = 2;
        }

        private bool alreadyInGame(string selectedId)
        {
            foreach (string id in globalData.gameUserList)
            {
                if (id == selectedId)
                {
                    return true;
                }
            }

            return false;
        }

        //Function executed when the invite button is clicked
        //It gets the text from the selected option and sends a command to the server to send the invitation
        //The user invites itself it pops an error
        private void InviteButtonClick()

        {
            Debug.Log("invite button");
            Transform selected = textTemplate.transform.parent.Find("selected");

            if (selected != null)
            {
                if(selected.gameObject.GetComponent<Text>().text != loginLogic.username.text && !alreadyInGame(selected.gameObject.GetComponent<Text>().text))
                {
                    if (selfInvite)
                    {
                        loginLogic.errorAnimator.SetBool("open", false);
                    }

                    serverConnection.SendMessage("9/" + selected.gameObject.GetComponent<Text>().text + "," +gameNumber);
                    
                }
                else
                {
                    selfInvite = true;
                    loginLogic.errorAnimator.SetBool("open", true);
                    loginLogic.error.text = "You cannot invite yourself!";
                }
                Debug.Log("YOU JUST INVITED: " + selected.gameObject.GetComponent<Text>().text);
            }
           
        }

        //Function that updates the displayed users that joined a game to a list.
        public void gamePlayersNotificationListen(string serverResponse)
        {
            List<string> names = serverResponse.Replace("11/", "").Split(',').ToList<string>();
            Transform[] allChildren = textTemplate2.transform.parent.GetComponentsInChildren<Transform>();

            foreach (Transform child in allChildren)
            {
                if (!(child.gameObject.name == "Text" || child.gameObject.name == "Content")) Destroy(child.gameObject);
            }

            foreach (string name in names)
            {
                if (name != "")
                {
                    GameObject newText = Instantiate(textTemplate2) as GameObject;
                    
                    newText.name = "connectedUser";

                    newText.SetActive(true);
                    newText.GetComponent<Text>().text = name;
                    newText.transform.SetParent(textTemplate2.transform.parent, false);
                }
            }
        }

        //Function that updates the displayed users connected to the game to a list.
        public void connectedUsersNotificationListen(string serverResponse)
        {
            List<string> names = serverResponse.Replace("7/", "").Split(',').ToList<string>();
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

                inviteButton.enabled = true;
            }
            else
            {
                unSelect();

                inviteButton.enabled = false;
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
}
