using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShipMovement : MonoBehaviour {

	private Rigidbody2D rb;
	private float force = 0f;
	private GameObject audio;
	private Global globalData;
	private bool landed = false;
	private bool forceCount = true;
	private Text veloc;

	//PAUSE
	private Image pauseImageBack;
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
		audio = GameObject.Find ("Music");
		globalData = audio.GetComponent<Global> ();
		veloc = GameObject.Find ("VelocityPoint").GetComponent<Text>();
		pauseImageBack = GameObject.Find ("PauseBack").GetComponent<Image> ();
		resumePauseImage = GameObject.Find ("Resume").GetComponent<Image> ();
		resumePauseText = GameObject.Find ("ResumeText").GetComponent<Text> ();
		exitPauseImage = GameObject.Find ("Exit").GetComponent<Image> ();
		exitPauseText = GameObject.Find ("ExitText").GetComponent<Text> ();
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
		} else if(globalData.getState() == "pause") {
		}

	}

	void Update(){
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
		Debug.Log (force);
		Debug.Log (transform.rotation.z);

		if (col.gameObject.tag == "Points") {
			//if(transform.rotation.z >= 0.1f || transform.rotation.z <= -0.1f){
			col.gameObject.GetComponent<PointsPlaces>().addPoints();
				//destroyAction ();
			//}
		} else if(col.gameObject.name == "CollisionObject") {
			destroyed = true;
			col.gameObject.GetComponent<PointsPlaces>().addPoints();
			destroyAction ();
		}
	}

	public bool getDestroyed(){
		return destroyed;
	}

	public void destroyAction(){
		
		Destroy (gameObject);
		Destroy (audio);
		SceneManager.LoadScene("Menu");
	}

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
	}
}
