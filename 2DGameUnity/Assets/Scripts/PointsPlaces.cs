using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PointsPlaces : MonoBehaviour {

	[SerializeField]private int point;
	private int score = 0;
	private Text scoreText;
	private ShipMovement ship;

	void Start(){
		ship = GameObject.Find ("Ship").GetComponent<ShipMovement>();
		score = PlayerPrefs.GetInt ("score");
		scoreText = GameObject.Find ("ScorePoint").GetComponent<Text> ();
		scoreText.text = score.ToString();
	}

	void OnCollisionEnter2D(Collision2D col){
		if (col.gameObject.name == "Ship" && !ship.getDestroyed ()) {
			score += point;

			PlayerPrefs.SetInt ("score", score);
			Scene scene = SceneManager.GetActiveScene ();
			SceneManager.LoadScene (scene.name);
		} else {
			if (PlayerPrefs.HasKey ("Highscore")) {
				if (PlayerPrefs.GetInt ("Highscore") < score) {
					PlayerPrefs.SetInt ("Highscore", score);
				}
			} else {
				PlayerPrefs.SetInt ("Highscore", score);
			}
		}
	}
		
}
