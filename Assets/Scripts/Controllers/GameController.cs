using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class GameController : MonoBehaviour
{
	//[SerializeField] private MenuUI menuUI;
	//[SerializeField] private LevelsController levelsController;
	//private SaveLoadSystem saveLoadSystem;
	//public MenuUI MenuUI => menuUI;
	[SerializeField] private RCC_CarControllerV3[] _botsPrefabs;
	[SerializeField] private List<RCC_CarControllerV3> _bots;
	
	[SerializeField] private RCC_CarControllerV3 _playerPrefab;
	private RCC_CarControllerV3 _player;
	private static GameController _instance;
	//private int level = 1;
	//private int coins;

	//public SaveLoadSystem SaveLoadSystem => saveLoadSystem;
	//public InputController InputController;
	public LevelController LevelController;
	public UIController UIController;
	public LevelCarsSpawnPositions LevelCarsSpawnPositions;

	public UnityAction OnStageIsOver;
	public UnityAction OnRaceIsReady;
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
		//InputController.Init();
		StartNewGame();
	}

	public void StartNewGame()
	{
		//TODO: точка входа в игровой цикл
		State.Start<InitializeNewGame>();

	}
	public void InitLevel()
	{
		//Transform[] spawnPoints = FindObjectOfType<LevelCarsSpawnPositions>().GetSpawnPoints();
		List<Transform> spawnPoints = FindObjectOfType<LevelCarsSpawnPositions>().GetSpawnPoints().ToList();
		int carSpawnPosition= Random.Range(3, spawnPoints.Count);
		RCC_CarControllerV3 player= RCC.SpawnRCC(_playerPrefab, spawnPoints[carSpawnPosition].position, Quaternion.identity,false, false, true);
		RCC.RegisterPlayerVehicle(player);
		spawnPoints.RemoveAt(carSpawnPosition);
		_player = player;
		for (int i = 0; i < spawnPoints.Count; i++)
		{
			//carSpawnPosition = Random.Range(0, spawnPoints.Count);
			RCC_CarControllerV3 AIbot = RCC.SpawnRCC(_botsPrefabs[Random.Range(0, _botsPrefabs.Length)], spawnPoints[i].position, Quaternion.identity, false, false, true);
			_bots.Add(AIbot);
			//spawnPoints.RemoveAt(i);
			Debug.Log(spawnPoints.Count);
		}

		UIController.StartRacePrep();
		//OnRaceIsReady += StartRace;
	}

	//private void StartRace()
	//{
	//	State.Start<Race>();
	//}
	public void SetCarsControllable()
	{
		_player.SetCanControl(true);
		foreach(RCC_CarControllerV3 bot in _bots)
		{
			bot.SetCanControl(true);
		}
	}
	public void LoadLevel()
	{
		Debug.Log("Loading Level");
		LevelController.LoadLevel("Level_IslandHighway");
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
