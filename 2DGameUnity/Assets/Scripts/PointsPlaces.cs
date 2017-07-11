using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PointsPlaces : MonoBehaviour {

	[SerializeField]private int point;
	private Text scoreText;
	private Ship ship;
	private LevelManager lvl;

	void Start(){
		ship = GameObject.Find ("Ship").GetComponent<Ship>();
		scoreText = GameObject.Find ("ScorePoint").GetComponent<Text> ();
		scoreText.text = ship.getScore().ToString();
		lvl = GameObject.Find ("LevelManager").GetComponent<LevelManager> ();
	}

	public void addPoints(){
		if (!ship.getDestroyed ()) {
			ship.scoreCalculation (true,point);
			lvl.goNextLevel ();
		} else {
			ship.calculateHighscore();
			lvl.resetLevel ();
		}
	}



}
