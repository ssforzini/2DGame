using UnityEngine;
using UnityEngine.SceneManagement;

public class ShipMovement : MonoBehaviour {

	private Rigidbody2D rb;
	private float maxForce = 3f;
	private float force = 0f;
	private GameObject audio;

	private bool destroyed;

	// Use this for initialization
	void Start () {
		destroyed = false;
		rb = GetComponent<Rigidbody2D> ();
		audio = GameObject.Find ("Music");
	}

	// Update is called once per frame
	void FixedUpdate () {
		if(Input.GetKey(KeyCode.LeftArrow) && transform.rotation.z <= 0.7f){
			transform.Rotate (Vector3.forward * 2f);
		}
		if(Input.GetKey(KeyCode.RightArrow) && transform.rotation.z >= -0.69f){
			transform.Rotate (Vector3.back * 2f);
		}
		if (Input.GetKey (KeyCode.UpArrow)) {
			if(force <= maxForce){
				rb.gravityScale = 0;
				rb.AddRelativeForce (Vector2.up * 0.1f);
				force += 0.02f;
			}

		} else {
			rb.gravityScale = 0.05f;
		}

		if(Input.GetKey(KeyCode.Escape)){
			destroyAction ();
			SceneManager.LoadScene("Menu");
		}

		if (!Input.anyKey) {
			force -= 0.1f;
		}
	}

	void Update(){
	}

	void OnCollisionEnter2D(Collision2D col){
		if (col.gameObject.tag == "Points") {
			if(transform.rotation.z >= 0.1f || transform.rotation.z <= -0.1f){
				destroyAction ();
			}
		} else if(col.gameObject.name == "Mapa") {
			destroyAction ();
		}
	}

	public bool getDestroyed(){
		return destroyed;
	}

	private void destroyAction(){
		
		destroyed = true;
		Destroy (gameObject);
		Destroy (audio);
		SceneManager.LoadScene("Menu");		
	}
}
