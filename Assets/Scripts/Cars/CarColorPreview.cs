using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarColorPreview : MonoBehaviour
{
    [SerializeField] private MeshRenderer _carPaintableBody;
    private Material _carMaterial;

    public void SetColor(Color incomingColor)
    {
        _carMaterial = _carPaintableBody.material;
        _carMaterial.color = incomingColor;
    }
}
