using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour {

	[SerializeField]private Button[] buttons;
	private Button playButton;
	private Button exitButton;

	// Use this for initialization
	void Start () {
		playButton = buttons [0].GetComponent<Button> ();
		exitButton = buttons [1].GetComponent<Button> ();

		playButton.onClick.AddListener(PlayClick);
		exitButton.onClick.AddListener(ExitClick);
	}

	void ExitClick(){
		Application.Quit();
	}

	void PlayClick(){
		PlayerPrefs.SetInt("HiScore",0);
		SceneManager.LoadScene("FirstLevel");
	}
}
