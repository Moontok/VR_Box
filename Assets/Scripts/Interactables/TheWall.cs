using System.Collections.Generic;
using Unity.Android.Gradle;
using UnityEngine;
using UnityEngine.Events;

public class TheWall : MonoBehaviour
{
    public UnityEvent OnDestroy;

    [SerializeField] int explosiveForce = 10000;
    [SerializeField] RemoteExplosionDevice remoteExplosiveDevice = null;
    [SerializeField] AudioClip destroyWallClip = null;

    List<GameObject> wallBlocks = new List<GameObject>();
    bool detonated = false;

    public AudioClip GetDestroyClip => destroyWallClip;

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

        OnDestroy?.Invoke();

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
