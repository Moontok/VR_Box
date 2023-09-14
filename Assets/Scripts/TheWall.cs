using System.Collections.Generic;
using Unity.Android.Gradle;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TheWall : MonoBehaviour
{
    [SerializeField] bool isDynamic = false;
    [SerializeField] int explosionForce = 2000;

    List<GameObject> wallBlocks = new List<GameObject>();

    private void Awake()
    {
        foreach (Transform child in transform)
        {
            wallBlocks.Add(child.gameObject);
        }
    }

    public void DynamicWall(bool toggle)
    {
        if (!isDynamic) return;

        foreach (GameObject block in wallBlocks)
        {
            Rigidbody rb = block.GetComponent<Rigidbody>();
            rb.isKinematic = toggle;
        }
    }

    public void ExplodeWall()
    {
        foreach (GameObject block in wallBlocks)
        {
            Rigidbody rb = block.GetComponent<Rigidbody>();
            rb.isKinematic = false;
            rb.AddRelativeForce(Random.onUnitSphere * explosionForce);
        }
    }
}
