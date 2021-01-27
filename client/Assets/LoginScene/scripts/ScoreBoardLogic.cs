using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using NoGameFoundClient;
using System.Linq;


namespace LoginUI
{
    //Script that controls the game creation window
    public class ScoreBoardLogic : MonoBehaviour
    {

        public Canvas scoreBoardCanvas;
        public Button mainMenuButton;
        public Button personalBoardButton;
        public Button searchButton;
        private ServerConnection serverConnection;
        public GameObject playersTemplate;
        public GameObject scoreTemplate;
        public GameObject entryTemplate;

        private MainMenuLogic mainMenuLogic;
        private PersonalBoardLogic personalBoardLogic;
        public Toggle myGames;
        private List<string> games;
        private List<string> gamesDates;
        public Text date1;
        public Text date2;

      
        // Start is called before the first frame update
        //It gets all the unity objects needed on this script, makes the menu invisible
        //Get an instance of the server connection
        //set the button events
        void Start()
        {
            scoreBoardCanvas.enabled = false;
            serverConnection = ServerConnection.getInstance();
            
            GameObject mainMenuWindow = GameObject.Find("MainMenuWindow");
            mainMenuLogic = mainMenuWindow.GetComponent<MainMenuLogic>();
            mainMenuButton.onClick.AddListener(MainMenuClick);
            
            GameObject personalBoardWindow = GameObject.Find("PersonalBoardWindow");
            personalBoardLogic = personalBoardWindow.GetComponent<PersonalBoardLogic>();
            personalBoardButton.onClick.AddListener(MyStatsClick);
            searchButton.onClick.AddListener(SearchDatesClick);
            myGames.onValueChanged.AddListener(MyGamesClick);
            
        }
        


        //Function executed when Main menu button is clicked.
        //It sends a command to exit the game the user was in
        //Changes the UI to the main menu
        private void MainMenuClick()
        {
            scoreBoardCanvas.enabled = false;
            mainMenuLogic.mainMenuCanvas.enabled = true;
            
        }
 
        private void MyStatsClick()
        {
            serverConnection.SendMessage("17/0");
            scoreBoardCanvas.enabled = false;
            personalBoardLogic.personalBoardCanvas.enabled = true;
            
        }
        
        private void SearchDatesClick()
        {
            if (date1.text.Length == 0 && date2.text.Length == 0)
            {
                showAllGameEntries(games);
                return;
            }
            
            String[] formats = {"dd/MM/yyyy", "dd/M/yyyy", "d/MM/yyyy", "d/M/yyyy"};
            bool correct = true;
            
            DateTime dDate1;
            if (!DateTime.TryParseExact(date1.text,formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out dDate1))
            {
                correct = false;
            }
            DateTime dDate2;
            if (!DateTime.TryParseExact(date2.text,formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out dDate2))
            {
                correct = false;
            }

            if (correct && (dDate1.Date <= dDate2.Date))
            {
                
                Debug.Log("PEEEEERFECT");
                String[] date1Parsed = date1.text.Split('/');
                Array.Reverse(date1Parsed);
                String date1Final = String.Join("-", date1Parsed);
                String[] date2Parsed = date2.text.Split('/');
                Array.Reverse(date2Parsed);
                String date2Final = String.Join("-", date2Parsed);
                String message = "19/" + date1Final +"/"+ date2Final + "~";
                serverConnection.SendMessage(message);
                Debug.Log(message);
                return;
            }
            Debug.Log("EERRRRROOOOOOR BOIII");
                
            


        }
        
        private void MyGamesClick(bool newVal)
        {
            if (games.Count > 0)
            {
                if (newVal)
                {
                    showMyGameEntries();
                }
                else
                {
                    showAllGameEntries(games);
                }
            }

        }

        //Function that updates the displayed users that joined a game to a list.
        public void scoreBoardNotificationListen(string serverResponse)
        {
            games = serverResponse.Replace("16/", "").Split(',').ToList<string>();
            showAllGameEntries(games);
        }
        public void scoreBoardNotificationDATESListen(string serverResponse)
        {
            gamesDates = serverResponse.Replace("19/", "").Split(',').ToList<string>();
            showAllGameEntries(gamesDates);
        }

        private void showMyGameEntries()
        {
            String myUser = globalData.userName;
            Transform[] allChildren = entryTemplate.transform.parent.GetComponentsInChildren<Transform>();

            foreach (Transform child in allChildren)
            {
                if (!(child.gameObject.name == "EntryTemplate" || child.gameObject.name == "Content")) Destroy(child.gameObject);
            }

            foreach (string game in games)
            {
                if (game != "")
                {
                    GameObject entry = Instantiate(entryTemplate) as GameObject;
                    GameObject players = Instantiate(playersTemplate) as GameObject;
                    GameObject scoreT = Instantiate(scoreTemplate) as GameObject;

                    string gameId = game.Split('-')[0];
                    List<string> names = game.Split('-')[1].Split('*').ToList<string>();
                    string allnames = "";
                    bool iPlayed = false;
                    foreach (string name1 in names)
                    {
                        if (name1 == myUser)
                        {
                            iPlayed = true;
                        }

                        if (name1 != "")
                        {
                            allnames += name1 + ", ";
                        }
                    }
                    allnames = allnames.Substring(0, allnames.Length - 2);

                    if (iPlayed)
                    {

                        string score = game.Split('-')[2].Replace("-", "");
                        if (allnames.Length > 36)
                        {
                            allnames = allnames.Substring(0, 36);
                            allnames += "...";

                        }

                        if (score.Length > 4)
                        {
                            score = score.Substring(0, 4);

                        }

                        players.SetActive(true);
                        players.GetComponent<Text>().text = allnames;
                        players.transform.SetParent(entry.transform, false);

                        scoreT.SetActive(true);
                        scoreT.GetComponent<Text>().text = score;
                        scoreT.transform.SetParent(entry.transform, false);

                        entry.name = "gameEntry";
                        entry.SetActive(true);
                        entry.transform.SetParent(entryTemplate.transform.parent, false);
                    }
                }
            }
        }
        
        private void showAllGameEntries(List<string> gamesList)
        {
            Transform[] allChildren = entryTemplate.transform.parent.GetComponentsInChildren<Transform>();

            foreach (Transform child in allChildren)
            {
                if (!(child.gameObject.name == "EntryTemplate" || child.gameObject.name == "Content")) Destroy(child.gameObject);
            }

            foreach (string game in gamesList)
            {
                if (game != "")
                {
                    GameObject entry = Instantiate(entryTemplate) as GameObject;
                    GameObject players = Instantiate(playersTemplate) as GameObject;
                    GameObject scoreT = Instantiate(scoreTemplate) as GameObject;

                    string gameId = game.Split('-')[0];
                    List<string> names = game.Split('-')[1].Split('*').ToList<string>();
                    string allnames = "";
                    
                    foreach (string name1 in names)
                    {
                        if (name1 != "")
                        {
                            allnames += name1 + ", ";
                        }
                    }

                    allnames = allnames.Substring(0, allnames.Length - 2);
                    string score = game.Split('-')[2].Replace("-", "");
                    if (allnames.Length > 36)
                    {
                        allnames=allnames.Substring(0, 36);
                        allnames += "...";

                    }
                    if (score.Length > 4)
                    {
                        score=score.Substring(0, 4);

                    }
                    players.SetActive(true);
                    players.GetComponent<Text>().text = allnames;
                    players.transform.SetParent(entry.transform, false);
                    
                    scoreT.SetActive(true);
                    scoreT.GetComponent<Text>().text = score;
                    scoreT.transform.SetParent(entry.transform, false);
                    
                    entry.name = "gameEntry";
                    entry.SetActive(true);
                    entry.transform.SetParent(entryTemplate.transform.parent, false);
                }
            }
        }
        


     
        

    }
}
