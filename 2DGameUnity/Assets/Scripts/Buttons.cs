using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour {

	[SerializeField]private Button[] buttons;
	private Button playButton;
	private Button exitButton;
	private Text highScore;

	// Use this for initialization
	void Start () {
		highScore = GameObject.Find ("highscore").GetComponent<Text>();
		if (PlayerPrefs.HasKey ("Highscore")) {
			highScore.text = PlayerPrefs.GetInt ("Highscore").ToString();
		} else {
			highScore.text = "0";
		}
		playButton = buttons [0].GetComponent<Button> ();
		exitButton = buttons [1].GetComponent<Button> ();

		playButton.onClick.AddListener(PlayClick);
		exitButton.onClick.AddListener(ExitClick);
	}

	void ExitClick(){
		Application.Quit();
	}

	void PlayClick(){
		PlayerPrefs.SetInt("score",0);
		SceneManager.LoadScene("MusicScene");
	}
}
