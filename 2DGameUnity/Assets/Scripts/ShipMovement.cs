using UnityEngine;
using UnityEngine.SceneManagement;

public class ShipMovement : MonoBehaviour {

	private Rigidbody2D rb;
	private float maxForce = 3f;
	private float force = 0f;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
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
			SceneManager.LoadScene("Menu");
		}

		if (!Input.anyKey) {
			force -= 0.1f;
		}
	}

	void Update(){
		//Debug.Log (transform.rotation.z);
	}

	void OnCollisionEnter2D(Collision2D col){
		if (col.gameObject.tag == "Points") {
			Debug.Log (transform.rotation.z);

			if(transform.rotation.z >= 0.1f || transform.rotation.z <= -0.1f){
				Destroy (gameObject);
				SceneManager.LoadScene("Menu");
			}
		} else if(col.gameObject.name == "Mapa") {
			Destroy (gameObject);
			SceneManager.LoadScene("Menu");
		}
	}
}
