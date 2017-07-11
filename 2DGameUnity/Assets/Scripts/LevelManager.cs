using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

	private static int actualLevel = 1;
	private static int maxLevel = 3;

	void Awake(){
		DontDestroyOnLoad (gameObject);
	}

	void Start(){
		SceneManager.LoadScene ("Level_"+actualLevel);
	}

	public void goNextLevel(){
		actualLevel++;
		if(actualLevel > maxLevel){
			resetLevel ();
		}
		SceneManager.LoadScene ("Level_"+actualLevel);
	}

	public void resetLevel(){
		actualLevel = 1;
	}
}