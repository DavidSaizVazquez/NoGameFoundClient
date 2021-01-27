using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NoGameFoundClient;
using System.Linq;
using UnityEngine.EventSystems;

namespace LoginUI
{

    //Script that controls the information window
    public class InformationLogic : MonoBehaviour
    {
        public Text EmailText;
        public Text AgeText;
        public Button loadEmail;
        public Button loadAge;
        public Toggle spam;
        public Button modifySpam;
        public Canvas informationCanvas;
        public Button mainMenuBtn;
        public Button deleteAccountBtn;


        public GameObject textTemplate;

        public bool canvasError = false;


        private ServerConnection serverConnection;
        GameObject LoginWindow;
        LoginLogic loginLogic;
        private MainMenuLogic mainMenuLogic;

        // Start is called before the first frame update
        //It gets all the unity objects needed on this script, makes the menu invisible
        //Get an instance of the server connection
        //set the button events
        void Start()
        {
            GameObject mainMenuWindow = GameObject.Find("MainMenuWindow");
            mainMenuLogic = mainMenuWindow.GetComponent<MainMenuLogic>();
            LoginWindow = GameObject.Find("LoginWindow");
            loginLogic = LoginWindow.GetComponent<LoginLogic>();
            serverConnection = ServerConnection.getInstance();
        
            informationCanvas.enabled = false;

            loadEmail.onClick.AddListener(EmailLoadOnClick);
            loadAge.onClick.AddListener(AgeLoadOnClick);
            modifySpam.onClick.AddListener(SpamModifyOnClick);
            mainMenuBtn.onClick.AddListener(MainMenuClick);
            deleteAccountBtn.onClick.AddListener(DeleteAccountClick);
            
        }

        //Function executed when the main menu button is clicked
        //Changes the UI back to the main menu
        private void MainMenuClick()
        {
            informationCanvas.enabled = false;
            mainMenuLogic.mainMenuCanvas.enabled = true;

        }

        private void DeleteAccountClick()
        {
            Debug.Log("Sending delete account petition...");
            serverConnection.SendMessage("18/0");
        }
        
        //Function executed when the email button is clicked
        //Sends a request to the server to give the email
        public void EmailLoadOnClick()
        {
            serverConnection.SendMessage("4/" + loginLogic.username.text);
        }
        
        //Function executed when the age button is clicked
        //Sends a request to the server to give the age
        public void AgeLoadOnClick()
        {
            serverConnection.SendMessage("3/" + loginLogic.username.text);
        }

        
        //Function executed when the email button is clicked
        //Sends a message to the server to modify the spam setting
        public void SpamModifyOnClick()
        {
            serverConnection.SendMessage("5/" + loginLogic.username.text + "," + (spam.isOn?1:0));
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

                        newText.name="connectedUser";

                        newText.SetActive(true);
                        newText.GetComponent<Text>().text = name;
                        newText.transform.SetParent(textTemplate.transform.parent, false);
                    }
                }
        }

        //Function to add the events to change text font and color on hover and on click
        public void addEventTriggers(GameObject newText){

            newText.AddComponent(typeof(EventTrigger));
            EventTrigger trigger = newText.GetComponent<EventTrigger>();
            EventTrigger.Entry entry;

            entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerClick;
            entry.callback.AddListener( (eventData) => { onClick(newText);});
            trigger.triggers.Add(entry);

            entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerEnter;
            entry.callback.AddListener( (eventData) => { makeGrey(newText);});
            trigger.triggers.Add(entry);

            entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerExit;
            entry.callback.AddListener( (eventData) => { makeBlack(newText);});
            trigger.triggers.Add(entry);

        }
        
        //Function that detects the selected item
        public void onClick(GameObject gameObject){

            if(gameObject.name!="selected"){
            unSelect();
            gameObject.GetComponent<Text>().fontStyle = FontStyle.Bold;
            gameObject.GetComponent<Text>().fontSize = 17;
            gameObject.name="selected";
            } else {
                unSelect();
            }

        }
        
        //Function that deselects an item
        public void unSelect(){
            Transform selected = textTemplate.transform.parent.Find("selected");

            if (selected!=null) 
            {
                selected.gameObject.GetComponent<Text>().fontSize = 14;
                selected.gameObject.GetComponent<Text>().fontStyle = FontStyle.Normal;
                selected.gameObject.name="connectedUser";
            };
            

        }

        //Function that changes the color of the text to gray
        public void makeGrey(GameObject go){
            go.GetComponent<Text>().color = Color.grey;
        }
        
        //Function that changes the color of the text to black
        public void makeBlack(GameObject go){
            go.GetComponent<Text>().color = Color.black;
        }
     
     
}
}
