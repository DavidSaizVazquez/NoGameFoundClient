using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NoGameFoundClient;

namespace LoginUI
{
    
    //Script that controls the UI of the main menu.
    public class MainMenuLogic : MonoBehaviour
    {

        public Button createGameBtn;
        public Button joinGameBtn;
        public Button userInfoBtn;

        public Canvas mainMenuCanvas;

        private InformationLogic informationLogic;
        private LoginLogic loginLogic;
        private GameCreationLogic gameCreationLogic;
        private JoinAGameLogic joinAGameLogic;

        private ServerConnection serverConnection;


        
        private InvitationLogic invitationLogic;


        // Start is called before the first frame update
        //It gets all the unity objects needed on this script, makes the menu invisible
        //Get an instance of the server connection
        //set the button events
        void Start()
        {
            
            mainMenuCanvas.enabled = false;
            GameObject informationWindow = GameObject.Find("InformationWindow");
            informationLogic = informationWindow.GetComponent<InformationLogic>();
            GameObject loginWindow = GameObject.Find("LoginWindow");
            loginLogic = loginWindow.GetComponent<LoginLogic>();
            GameObject gameCreationWindow = GameObject.Find("GameCreationWindow");
            gameCreationLogic = gameCreationWindow.GetComponent<GameCreationLogic>();

            GameObject invitationWindow = GameObject.Find("InvitationWindow");
            invitationLogic = invitationWindow.GetComponent<InvitationLogic>();
            GameObject joinAGameWindow = GameObject.Find("JoinAGameWindow");
            joinAGameLogic= joinAGameWindow.GetComponent<JoinAGameLogic>();

            serverConnection = ServerConnection.getInstance();
            createGameBtn.onClick.AddListener(CreateGameClick);
            joinGameBtn.onClick.AddListener(JoinGameClick);
            userInfoBtn.onClick.AddListener(UserInfoClick);
            
           gameCreationLogic.playButtonCanvas.enabled = false;
           joinAGameLogic.mainCanvas.enabled = false;


        }
        
        //function executed when 'create game' button is clicked.
        //It hides the main menu and shows the game creation menu.
        //It also sends a message to the server to create a new game
        private void CreateGameClick()
        {
            serverConnection.SendMessage("8/0");
            
            gameCreationLogic.playButtonCanvas.enabled = true; 
            gameCreationLogic.gameCreationCanvas.enabled = true;
            mainMenuCanvas.enabled = false;


        }
        
        //function executed when 'join game' button is clicked.
        //It hides the main menu and shows the join a game menu.
        private void JoinGameClick()
        {
            joinAGameLogic.mainCanvas.enabled = true;
            mainMenuCanvas.enabled = false;
            
        }
        
        //function executed when 'info' button is clicked.
        //It hides the main menu and shows the information menu.
        //It also sends a command to the server to receive the information that must be shown.
        private void UserInfoClick()
        {
            serverConnection.SendMessage("6/" + loginLogic.username.text);
            informationLogic.informationCanvas.enabled = true;
            mainMenuCanvas.enabled = false;
        }

    }
}
