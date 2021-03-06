using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MenuController : MonoBehaviour
{
   // [SerializeField] private RCC_Camera _mainCamera;
    [SerializeField] private GameObject _cameraHolder;
    private RCC_CarSelectionExample _carSelection;
    private Transform _carSelectionSpawnPosition;
    //public UnityAction OnMenuClosed;
   
    public void InitMenu()
    {
        Debug.Log("Loading Menu");

        //GameController.Instance.LevelController.OnLevelLoaded -= InitMenu;
        //_mainCamera = FindObjectOfType<RCC_Camera>();
        //_carSelection = FindObjectOfType<RCC_CarSelectionExample>();
      
        GameController.Instance.LevelController.LoadLevel(SceneNames.CarSelectionMenu);
        GameController.Instance.LevelController.OnLevelLoaded += StartMenu;
        

    }
    private void StartMenu(string levelName)
    {
        GameController.Instance.LevelController.OnLevelLoaded -= StartMenu;
        //_mainCamera = FindObjectOfType<RCC_Camera>();
        _carSelection = FindObjectOfType<RCC_CarSelectionExample>();
        _carSelection.Init();
      //  _carSelectionSpawnPosition = GameObject.FindGameObjectWithTag("CarSelectionMenuSpawnPosition").transform;
       // _cameraHolder.GetComponent<RCC_CameraCarSelection>().SetTarget(_carSelectionSpawnPosition);
       // GameController.Instance.SetVehiclesPosition(_carSelectionSpawnPosition);
       // _carSelection.Init(_cameraHolder,GameController.Instance.PlayerPrefabs);
    }

    public void UnInitMenu()
    {
        Debug.Log("UnLoading Menu");
        GameController.Instance.LevelController.UnLoadLevel(SceneNames.CarSelectionMenu);
        GameController.Instance.LevelController.OnLevelUnLoaded += EndMenu;
    }

    private void EndMenu()
    {
        GameController.Instance.LevelController.OnLevelUnLoaded -= EndMenu;
        //OnMenuClosed?.Invoke();
        GameController.Instance.StartNewGame();
    }

}
