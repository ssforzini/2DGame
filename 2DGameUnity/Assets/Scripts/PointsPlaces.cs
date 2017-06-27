using UnityEngine;
using UnityEngine.SceneManagement;

public class PointsPlaces : MonoBehaviour {

	[SerializeField]private int point;
	private int score = 0;

	void Start(){
		score = PlayerPrefs.GetInt ("HiScore");
	}

	// Update is called once per frame
	void Update () {
	}

	void OnCollisionEnter2D(Collision2D col){
		if(col.gameObject.name == "Ship"){
			score += point;
			Debug.Log (score);

			PlayerPrefs.SetInt("HiScore",score);
			/*Scene scene = SceneManager.GetActiveScene();
			SceneManager.LoadScene(scene.name);*/
		}
	}
		
}
