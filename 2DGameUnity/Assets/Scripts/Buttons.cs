using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour {

	[SerializeField]private Button[] buttons;
	private Text highScore;
	private Ship ship;
	private LevelManager lvl;

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
			buttons [3].GetComponent<Button> ().onClick.AddListener(delegate{RedirectClick("Controls");});
		} else if(SceneManager.GetActiveScene().name == "Credits" || SceneManager.GetActiveScene().name == "Controls") {
			buttons [0].GetComponent<Button> ().onClick.AddListener(delegate{RedirectClick("Menu");});
		} else {
			ship = GameObject.Find ("Ship").GetComponent<Ship>();
			lvl = GameObject.Find ("LevelManager").GetComponent<LevelManager> ();
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
		lvl.resetLevel ();
		ship.scoreCalculation (false,0);
		ship.destroyAction ();
	}

	void RedirectClick(string scene){
		SceneManager.LoadScene(scene);		
	}
}
