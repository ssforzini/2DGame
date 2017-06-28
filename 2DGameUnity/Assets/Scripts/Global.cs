using UnityEngine;
using UnityEngine.SceneManagement;

public class Global : MonoBehaviour {

	void Awake(){
		DontDestroyOnLoad (gameObject);
	}

	void Start(){
		SceneManager.LoadScene("FirstLevel");
	}
}
