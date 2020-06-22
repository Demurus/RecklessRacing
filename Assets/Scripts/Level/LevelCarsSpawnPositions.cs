using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCarsSpawnPositions : MonoBehaviour
{
    [SerializeField] private Transform[] _carsSpawnPoints;

    public Transform[] GetSpawnPoints()
    {
        return _carsSpawnPoints;
    }
}
