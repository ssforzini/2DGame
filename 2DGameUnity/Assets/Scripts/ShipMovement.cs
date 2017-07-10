using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShipMovement : MonoBehaviour {

	private Rigidbody2D rb;
	private float force = 0f;
	private GameObject audio;
	private bool landed = false;
	private bool forceCount = true;
	private Text veloc;

	private bool destroyed;

	// Use this for initialization
	void Start () {
		destroyed = false;
		rb = GetComponent<Rigidbody2D> ();
		audio = GameObject.Find ("Music");
		veloc = GameObject.Find ("VelocityPoint").GetComponent<Text>();
	}

	void FixedUpdate () {

		//MOVEMENT
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
		//END MOVEMENT


		if(Input.GetKey(KeyCode.Escape)){
			destroyAction ();
			SceneManager.LoadScene("Menu");
		}

	}

	void Update(){
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

	void OnCollisionEnter2D(Collision2D col){
		landed = true;
		Debug.Log (force);
		/*if (col.gameObject.tag == "Points") {
			if(transform.rotation.z >= 0.1f || transform.rotation.z <= -0.1f){
				//destroyAction ();
			}
		} else if(col.gameObject.name == "Mapa") {
			//destroyAction ();
		}*/
	}

	public bool getDestroyed(){
		return destroyed;
	}

	private void destroyAction(){
		
		destroyed = true;
		Destroy (gameObject);
		Destroy (audio);
		//SceneManager.LoadScene("Menu");		
	}
}
