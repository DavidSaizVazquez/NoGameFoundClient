
using LoginUI;
using UnityEngine;
using UnityEngine.UI;
using NoGameFoundClient;


//Script that controls that popup window that appears when we receive an invitation.
public class InvitationLogic : MonoBehaviour
{
    public Canvas invitationCanvas;
    public Button joinBtn;
    public Button declineBtn;

    public Text message;
    public Animator animator;

    public int gameNumber;

    private ServerConnection serverConnection;

    private GameCreationLogic gameCreationLogic;

    // Start is called before the first frame update
    //It gets all the unity objects needed on this script, makes the menu invisible
    //Get an instance of the server connection
    //set the button events
    void Start()
    {
        GameObject gameCreationWindow = GameObject.Find("GameCreationWindow");
        gameCreationLogic = gameCreationWindow.GetComponent<GameCreationLogic>();
        serverConnection = ServerConnection.getInstance();
        invitationCanvas.enabled = false;
        joinBtn.onClick.AddListener(joinClick);
        declineBtn.onClick.AddListener(declineClick);

        animator = invitationCanvas.GetComponent<Animator>();
    }

    //Function executed when the join button is clicked
    //It sends a message to the server to tell him that we join the game
    //Starts the animation to close the window
    private void joinClick()
    {
        serverConnection.SendMessage("10/" + gameNumber);
        gameCreationLogic.gameCreationCanvas.enabled = true;
        animator.SetBool("open", false);
    }

    //Function executed when the decline button is clicked
    //It sends a message to the server to tell him that we do not join the game
    //Starts the animation to close the window
    private void declineClick()
    {
        serverConnection.SendMessage("10/-1");
        animator.SetBool("open", false);
    }
  
}
