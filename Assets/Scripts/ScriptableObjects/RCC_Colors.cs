using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "Colors", menuName = "Color")]
public class RCC_Colors : ScriptableObject
{
	public Color[] VehicleColors;

	#region singleton
	private static RCC_Colors instance;
	public static RCC_Colors Instance { get { if (instance == null) instance = Resources.Load("RCC Assets/RCC_Colors") as RCC_Colors; return instance; } }
	#endregion
}
