using UnityEngine;
using UnityEngine.UI;
using NoGameFoundClient;


namespace LoginUI
{
//Script that controls the login window
	public class LoginLogic : MonoBehaviour
{


	public Canvas loginCanvas;
	public Text username;
	public InputField password;
	
	public Text error;
	public Canvas errorCanvas;
	public Animator errorAnimator;


	public Button loginButton;
	public Button registerButton;
	private ServerConnection serverConnection;	


	public bool credentialsError = false;


	private RegisterLogin registerLogin;

	// Start is called before the first frame update
	//It gets all the unity objects needed on this script, makes the menu invisible
	//Get an instance of the server connection
	//set the button events
	void Start()
	{

		serverConnection = ServerConnection.getInstance();


		errorAnimator = errorCanvas.GetComponent<Animator>();
	
		loginButton.enabled = false;
		registerButton.enabled = false;
		errorCanvas.enabled = false;

		GameObject registerWindow = GameObject.Find("RegisterWindow");
		registerLogin = registerWindow.GetComponent<RegisterLogin>();

		loginButton.onClick.AddListener(LoginButtonClick);
		registerButton.onClick.AddListener(RegisterButtonClick);

		}

	//Function executed when Login button is clicked.
	//It sends a command to the server with the credentials of the user
	public void LoginButtonClick() {
		//send login petition
		serverConnection.SendMessage("1/" + username.text + "," + password.text);

	}
	
	//Function executed when Login button is clicked.
	//Changes the UI to the register window.
	public void RegisterButtonClick()
	{
		registerLogin.registerWindow.enabled = true;
		loginCanvas.enabled = false;
	}
}

}
