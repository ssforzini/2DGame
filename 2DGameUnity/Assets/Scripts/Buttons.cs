using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour {

	[SerializeField]private Button[] buttons;
	private Text highScore;
	private ShipMovement ship;

	// Use this for initialization
	void Start () {

		if(SceneManager.GetActiveScene().name == "Menu"){
			highScore = GameObject.Find ("highscore").GetComponent<Text>();
			if (PlayerPrefs.HasKey ("Highscore")) {
				highScore.text = PlayerPrefs.GetInt ("Highscore").ToString();
			} else {
				highScore.text = "0";
			}

			buttons [0].GetComponent<Button> ().onClick.AddListener(PlayClick);
			buttons [1].GetComponent<Button> ().onClick.AddListener(ExitClick);
			buttons [2].GetComponent<Button> ().onClick.AddListener(delegate{RedirectClick("Credits");});
		} else if(SceneManager.GetActiveScene().name == "Credits") {
			buttons [0].GetComponent<Button> ().onClick.AddListener(delegate{RedirectClick("Menu");});
		} else {
			ship = GameObject.Find ("Ship").GetComponent<ShipMovement>();
			buttons [0].GetComponent<Button> ().onClick.AddListener(ResumeClick);
			buttons [1].GetComponent<Button> ().onClick.AddListener(EndGameClick);
		}
			
	}

	void ExitClick(){
		Application.Quit();
	}

	void PlayClick(){
		PlayerPrefs.SetInt("score",0);
		SceneManager.LoadScene("MusicScene");
	}

	void ResumeClick(){
		ship.pauseOption (false,false);
	}

	void EndGameClick(){
		ship.destroyAction ();
	}

	void RedirectClick(string scene){
		SceneManager.LoadScene(scene);		
	}
}
