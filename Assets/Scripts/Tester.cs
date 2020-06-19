using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tester : MonoBehaviour
{
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W)) State.Start<WinState>();
        if (Input.GetKeyDown(KeyCode.L)) State.Start<LoseState>();

    }


}
