using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PointsPlaces : MonoBehaviour {

	[SerializeField]private int point;
	private static int score = 0;
	private Text scoreText;
	private ShipMovement ship;

	void Start(){
		ship = GameObject.Find ("Ship").GetComponent<ShipMovement>();
		scoreText = GameObject.Find ("ScorePoint").GetComponent<Text> ();
		scoreText.text = score.ToString();
	}

	void OnCollisionEnter2D(Collision2D col){
		
	}

	public void addPoints(){
		if (!ship.getDestroyed ()) {
			score += point;

			SceneManager.LoadScene ("FirstLevel");
		} else {
			if (PlayerPrefs.HasKey ("Highscore")) {
				if (PlayerPrefs.GetInt ("Highscore") < score) {
					PlayerPrefs.SetInt ("Highscore", score);
				}
			} else {
				PlayerPrefs.SetInt ("Highscore", score);
			}
			score = 0;
		}
	}
		
}
