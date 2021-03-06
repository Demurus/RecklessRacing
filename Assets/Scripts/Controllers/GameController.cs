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
	private List<RCC_CarControllerV3> _bots = new List<RCC_CarControllerV3>();
	[SerializeField] private RCC_Camera _mainCamera;
	[SerializeField] private GameObject _racingUI;
	private RCC_CarControllerV3 _player;
	private static GameController _instance;
	//private int level = 1;
	//private int coins;

	//public SaveLoadSystem SaveLoadSystem => saveLoadSystem;
	//public InputController InputController;
	public List<RCC_CarControllerV3> PlayerPrefabs = new List<RCC_CarControllerV3>();
	public LevelController LevelController;
	public UIController UIController;
	public MenuController MenuController;
	//public LapController LapController;
	public LevelCarsSpawnPositions LevelCarsSpawnPositions;

	public UnityAction OnStageIsOver;
	public UnityAction OnRaceIsReady;
	//public UnityAction<int> OnCoinsChange;
	public int AmountOfLaps = 3;
	
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
		
	}

	private void Start()
	{
		LoadMenu();
	}

	public void StartNewGame()
	{
		//TODO: точка входа в игровой цикл
		//MenuController.OnMenuClosed -= StartNewGame;
		State.Start<InitializeNewGame>();
	}
	
	public void InitLevel()
	{
		_mainCamera.gameObject.SetActive(true);
		_racingUI.SetActive(true);
		//Transform[] spawnPoints = FindObjectOfType<LevelCarsSpawnPositions>().GetSpawnPoints();
		List<Transform> spawnPoints = FindObjectOfType<LevelCarsSpawnPositions>().GetSpawnPoints().ToList();
		int carSpawnPosition = Random.Range(3, spawnPoints.Count);
		//RCC_CarControllerV3 spawnedVehicle = RCC.SpawnRCC(RCC_Vehicles.Instance.vehicles[i], spawnPosition.position, spawnPosition.rotation, false, false, false);
		RCC_CarControllerV3 player= RCC.SpawnRCC(RCC_Vehicles.Instance.vehicles[PlayerPrefs.GetInt(PlayerPrefsKeys.SelectedRCCVehicle)], spawnPoints[carSpawnPosition].position, Quaternion.identity,false, false, true);
		_player = player;
		//_player.GetComponent<CarColorPreview>().SetColor(RCC_Colors.Instance.VehicleColors[PlayerPrefs.GetInt(PlayerPrefsKeys.SelectedVehicleColor)]);
		RCC.RegisterPlayerVehicle(_player);
		spawnPoints.RemoveAt(carSpawnPosition);
		//_player = player;
		for (int i = 0; i < spawnPoints.Count; i++)
		{
			//carSpawnPosition = Random.Range(0, spawnPoints.Count);
			RCC_CarControllerV3 AIbot = RCC.SpawnRCC(_botsPrefabs[Random.Range(0, _botsPrefabs.Length)], spawnPoints[i].position, Quaternion.identity, false, false, true);
			_bots.Add(AIbot);
			
		}
		UIController.LapController.Init(AmountOfLaps);

		UIController.StartRacePrep();
		//OnRaceIsReady += StartRace;
	}
	
	public void SetVehiclesPosition(Transform targerPosition)
	{
		foreach (RCC_CarControllerV3 prefab in PlayerPrefabs)
		{
			prefab.transform.position = targerPosition.position;
			prefab.transform.rotation = targerPosition.rotation;
		}
	}
	
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
		//LevelController.LoadLevel("RCC Car Selection Load Next Scene");
	}
	public void LoadMenu()
	{
		MenuController.InitMenu();
		//MenuController.OnMenuClosed += StartNewGame;
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
