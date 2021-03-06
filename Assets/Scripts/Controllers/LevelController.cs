using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
   private string _activeLevelName;
   public UnityAction<string> OnLevelLoaded;
   public UnityAction OnLevelUnLoaded;
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
		//string nsame = SceneManager.GetActiveScene().name;
		Debug.Log(SceneManager.GetActiveScene().name + " is active");
		OnLevelLoaded?.Invoke(SceneManager.GetActiveScene().name);
	}
	public void UnLoadLevel(string name)
	{
		SceneManager.sceneUnloaded += OnSceneUnLoaded;
		SceneManager.UnloadSceneAsync(name);
		
	}
	void OnSceneUnLoaded(Scene scene)
	{
		SceneManager.sceneUnloaded -= OnSceneUnLoaded;
		OnLevelUnLoaded?.Invoke();
	}
}
