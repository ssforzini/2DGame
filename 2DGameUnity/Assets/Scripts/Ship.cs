using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Ship : MonoBehaviour {

	private Rigidbody2D rb;
	private float force = 0f;
	private static int score = 0;
	private GameObject audio;
	private Global globalData;
	private bool landed = false;
	private bool forceCount = true;
	private Text veloc;
	private GameObject lvlMng;

	//PAUSE
	private Image pauseImageBack;
	private Text pauseTitle;
	private Text pauseUpMoveT;
	private Text pauseLeftMoveT;
	private Text pauseRightMoveT;
	private Image pauseUpMoveI;
	private Image pauseLeftMoveI;
	private Image pauseRightMoveI;
	private Image resumePauseImage;
	private Text resumePauseText;
	private Image exitPauseImage;
	private Text exitPauseText;
	//END PAUSE

	private bool destroyed;

	// Use this for initialization
	void Start () {
		destroyed = false;
		rb = GetComponent<Rigidbody2D> ();
		lvlMng = GameObject.Find ("LevelManager");
		veloc = GameObject.Find ("VelocityPoint").GetComponent<Text>();


		pauseImageBack = GameObject.Find ("PauseBack").GetComponent<Image> ();
		resumePauseImage = GameObject.Find ("Resume").GetComponent<Image> ();
		resumePauseText = GameObject.Find ("ResumeText").GetComponent<Text> ();
		exitPauseImage = GameObject.Find ("Exit").GetComponent<Image> ();
		exitPauseText = GameObject.Find ("ExitText").GetComponent<Text> ();
		pauseTitle = GameObject.Find ("Title").GetComponent<Text> ();
		pauseUpMoveT = GameObject.Find ("UpText").GetComponent<Text> ();
		pauseLeftMoveT = GameObject.Find ("LeftText").GetComponent<Text> ();
		pauseRightMoveT = GameObject.Find ("RightText").GetComponent<Text> ();
		pauseUpMoveI = GameObject.Find ("UpImage").GetComponent<Image> ();
		pauseLeftMoveI = GameObject.Find ("LeftImage").GetComponent<Image> ();
		pauseRightMoveI = GameObject.Find ("RightImage").GetComponent<Image> ();

		audio = GameObject.Find ("Music");
		globalData = audio.GetComponent<Global> ();
		pauseOption (false,false);
	}

	void FixedUpdate () {
		//MOVEMENT
		if(globalData.getState() == "play"){
			if(Input.GetKey(KeyCode.LeftArrow) && transform.rotation.z <= 0.7f){
				transform.Rotate (Vector3.forward * 2f);
			}
			if(Input.GetKey(KeyCode.RightArrow) && transform.rotation.z >= -0.69f){
				transform.Rotate (Vector3.back * 2f);
			}
			if (Input.GetKey (KeyCode.UpArrow)) {
				rb.gravityScale = 0;
				rb.AddRelativeForce (Vector2.up * 5f);
				force = rb.velocity.y;

			} else {
				rb.gravityScale = 4f;
			}

			if(Input.GetKey(KeyCode.P)){
				pauseOption(true,true);
			}
			//END MOVEMENT
		} 

	}

	void Update(){

		//CONTEO DE LA VELOCIDAD CUANDO ESTA MOVIENDOSE Y CUANDO ATERRIZA
		if(globalData.getState() == "play"){
			if((!Input.GetKey (KeyCode.UpArrow)) && forceCount){
				if (landed && force <= 0f) {
					if (force >= 0f) {
						force = 0f;
						forceCount = false;
					}
				} 

				force = rb.velocity.y;
			}
			veloc.text = force.ToString ();
		}
	}

	void OnCollisionEnter2D(Collision2D col){
		landed = true;

		if (col.gameObject.tag == "Points") {
			if (force < -25f || (transform.rotation.z >= 0.05f || transform.rotation.z <= -0.05f)) {
				destroyed = true;
				col.gameObject.GetComponent<PointsPlaces>().addPoints();
				destroyAction ();
			} else {
				col.gameObject.GetComponent<PointsPlaces>().addPoints();
			}
		} else if(col.gameObject.name == "CollisionObject") {
			destroyed = true;
			col.gameObject.GetComponent<PointsPlaces>().addPoints();
			destroyAction ();
		}
	}

	public bool getDestroyed(){
		return destroyed;
	}

	//CUANDO PIERDE DESTRUYE EL OBJETO QUE CONTIENE LA MUSICA Y EL MANAGER DE NIVELES
	public void destroyAction(){
		Destroy (gameObject);
		Destroy (audio);
		Destroy (lvlMng);
		SceneManager.LoadScene("Menu");
	}

	//FUNCION PARA ACTIVAR EL MENU DE PAUSA
	public void pauseOption(bool _option, bool pauseOn){
		if (pauseOn) {
			globalData.changeState ("pause");
			rb.constraints = RigidbodyConstraints2D.FreezeAll;			
		} else {
			globalData.changeState ("play");
			rb.constraints = RigidbodyConstraints2D.None;
		}


		pauseImageBack.enabled = _option;
		resumePauseImage.enabled = _option;
		resumePauseText.enabled = _option;
		exitPauseImage.enabled = _option;
		exitPauseText.enabled = _option;
		pauseTitle.enabled = _option; 
		pauseUpMoveT.enabled = _option;
		pauseLeftMoveT.enabled = _option;
		pauseRightMoveT.enabled = _option;
		pauseUpMoveI.enabled = _option; 
		pauseLeftMoveI.enabled = _option;
		pauseRightMoveI.enabled = _option;
	}

	public void scoreCalculation(bool adding, int point){
		if (adding) {
			score += point;
		} else {
			score = point;
		}
	}

	public int getScore(){
		return score;
	}

	public void calculateHighscore(){
		if (PlayerPrefs.HasKey ("Highscore")) {
			if (PlayerPrefs.GetInt ("Highscore") < score) {
				PlayerPrefs.SetInt ("Highscore", score);
			}
		} else {
			PlayerPrefs.SetInt ("Highscore", score);
		}
		scoreCalculation (false,0);
	}
}
