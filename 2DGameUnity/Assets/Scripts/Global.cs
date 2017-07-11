using UnityEngine;
using UnityEngine.SceneManagement;

public class Global : MonoBehaviour {

	private string actualState;

	void Awake(){
		DontDestroyOnLoad (gameObject);
		actualState = "play";
	}

	public void changeState(string _state){
		actualState = _state;
	}

	public string getState(){
		return actualState;
	}
}
