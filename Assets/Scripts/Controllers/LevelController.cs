using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
   private string _activeLevelName;
   public void LoadLevel(string name)
    {
		SceneManager.sceneLoaded += OnSceneLoaded;
		_activeLevelName = name;
		//SceneManager.LoadSceneAsync("Level-0" + (GameC.Instance.SaveLoadSystem.Data.Level % 5 + 1), LoadSceneMode.Additive);
		SceneManager.LoadSceneAsync(name, LoadSceneMode.Additive);
		
	}
	void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		SceneManager.sceneLoaded -= OnSceneLoaded;
		SceneManager.SetActiveScene(SceneManager.GetSceneByName(_activeLevelName));
		// do some init
		//_gameManager.Init();
		
	}
}
