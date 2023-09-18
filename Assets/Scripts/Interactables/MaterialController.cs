using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialController : MonoBehaviour
{
    [SerializeField] Material baseMaterial = null;
    [SerializeField] Material emissionMaterial = null;

    public void TurnEmissionOn()
    {
        GetComponent<Renderer>().material = emissionMaterial;
    }

    public void TurnEmissionOff()
    {
        GetComponent<Renderer>().material = baseMaterial;
    }
}
