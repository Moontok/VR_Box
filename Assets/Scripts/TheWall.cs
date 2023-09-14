using System.Collections.Generic;
using Unity.Android.Gradle;
using UnityEngine;
using UnityEngine.Events;

public class TheWall : MonoBehaviour
{
    [SerializeField] int explosiveForce = 10000;
    [SerializeField] RemoteExplosionDevice remoteExplosiveDevice = null;

    List<GameObject> wallBlocks = new List<GameObject>();
    bool detonated = false;

    private void Awake()
    {
        foreach (Transform child in transform)
        {
            wallBlocks.Add(child.gameObject);
        }
    }

    public void DynamicWall(bool toggle)
    {
        foreach (GameObject block in wallBlocks)
        {
            Rigidbody rb = block.GetComponent<Rigidbody>();
            rb.isKinematic = toggle;
        }
    }

    public void ExplodeWall()
    {
        if (detonated) return;

        detonated = true;

        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.Play();
        
        foreach (GameObject block in wallBlocks)
        {
            int force = Random.Range(explosiveForce / 2, explosiveForce);

            Rigidbody rb = block.GetComponent<Rigidbody>();
            rb.isKinematic = false;
            rb.AddRelativeForce(Random.onUnitSphere * force);
        }
    }
}
