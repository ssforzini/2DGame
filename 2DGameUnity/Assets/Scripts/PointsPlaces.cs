using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PointsPlaces : MonoBehaviour {

	[SerializeField]private int point;
	private int score = 0;
	private Text scoreText;

	void Start(){
		score = PlayerPrefs.GetInt ("score");
		scoreText = GameObject.Find ("ScorePoint").GetComponent<Text> ();
		scoreText.text = score.ToString();
	}

	void OnCollisionEnter2D(Collision2D col){
		if(col.gameObject.name == "Ship"){
			score += point;

			PlayerPrefs.SetInt("score",score);
			/*Scene scene = SceneManager.GetActiveScene();
			SceneManager.LoadScene(scene.name);*/
		}
	}
		
}
