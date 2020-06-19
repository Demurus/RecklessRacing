using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameController : MonoBehaviour
{
	//[SerializeField] private MenuUI menuUI;
	//[SerializeField] private LevelsController levelsController;
	//private SaveLoadSystem saveLoadSystem;
	//public MenuUI MenuUI => menuUI;
	private static GameController _instance;

	//private int level = 1;
	//private int coins;

	//public SaveLoadSystem SaveLoadSystem => saveLoadSystem;
	public InputController InputController;
	public UnityAction OnStageIsOver;
	//public UnityAction<int> OnCoinsChange;
	
	public static GameController Instance => _instance;

	//public int Level
	//{
	//	get => level;
	//	set
	//	{
	//		level = value;
	//		saveLoadSystem.Data.Level = level;
	//		saveLoadSystem.Save();
	//	}
	//}

	//public int Coins
	//{
	//	get => coins;
	//	set
	//	{
	//		coins = value;
	//		saveLoadSystem.Data.Coins = coins;
	//		saveLoadSystem.Save();
	//		OnCoinsChange?.Invoke(coins);
	//	}
	//}


	private void Awake()
	{
		if (_instance == null)
			_instance = this;
		
		//saveLoadSystem = new SaveLoadSystem();
		//coins = saveLoadSystem.Data.Coins;
		//level = saveLoadSystem.Data.Level;


	}

	private void Start()
	{
		//menuUI.Init();
		InputController.Init();
		StartNewGame();
	}

	public void StartNewGame()
	{
		//TODO: точка входа в игровой цикл
		State.Start<InitializeNewGame>();

	}

	public void LoadLevel()
	{
		Debug.Log("Loading Level");
		//levelsController.LoadLevel();
	}
	public void UnloadLevel()
	{
		Debug.Log("unLoading Level");
		//levelsController.UnloadLevel();
	}

	public void StageIsOver()
	{
		OnStageIsOver?.Invoke();
	}


	public void DelayInvoke(float t, UnityAction p)
	{
		StartCoroutine(DoDelayInvoke(t, p));
	}

	private IEnumerator DoDelayInvoke(float t, UnityAction p)
	{
		yield return new WaitForSeconds(t);
		p?.Invoke();
	}

	//void OnApplicationPause(bool pauseStatus)
	//{
	//	if (pauseStatus)
	//	{
	//		//do nothing
	//	}
	//	else
	//	{
	//		BaseTenjin instance = Tenjin.getInstance("16CM9RAXOZCSZH9YDN749D42CFY3QGVM");
	//		instance.Connect();
	//	}
	//}

#if UNITY_EDITOR
	private void OnGUI()
	{
		State.OnGUIDebugHelper();
	}
#endif
}
