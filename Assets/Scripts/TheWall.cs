using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[ExecuteAlways]
public class TheWall : MonoBehaviour
{
    [SerializeField] GameObject blockPrefab = null;
    [SerializeField] GameObject socketBlockPrefab = null;
    [SerializeField] int blocksWide = 1;
    [SerializeField] int blocksHigh = 2;
    [SerializeField] bool hasSocketBlock = true;
    [SerializeField] Vector2 socketBlockPosition = Vector2.zero;
    [SerializeField] bool buildWall = false;
    [SerializeField] bool deleteWall = false;
    [SerializeField] bool destroyWall = false;

    List<List<GameObject>> wallBlocks = new List<List<GameObject>>();
    XRSocketInteractor wallSocket = null;
    private Vector3 blockSize = Vector3.zero;
    private Vector3 spawnPosition = Vector3.zero;


    private void Start()
    {
    }

    private void Update()
    {
        if (buildWall)
        {
            buildWall = false;
            BuildWall();
        }
        else if (deleteWall)
        {
            deleteWall = false;
            DeleteWall();
        }
    }

    private void BuildWall()
    {
        if (wallBlocks.Count > 0) return;

        blockSize = blockPrefab.GetComponent<MeshRenderer>().bounds.size;
        spawnPosition = transform.position;

        Vector3 currentPosition = spawnPosition;

        for (int y = 0; y < blocksHigh; y++)
        {
            List<GameObject> row = new List<GameObject>();
            for (int x = 0; x < blocksWide; x++)
            {
                row.Add(Instantiate(blockPrefab, currentPosition, transform.rotation, transform));
                currentPosition.x += blockSize.x;
            }
            wallBlocks.Add(row);
            currentPosition.y += blockSize.y;
            currentPosition.x = spawnPosition.x;
        }
        
        if (hasSocketBlock)
        {
            int x = (int)socketBlockPosition.x;
            int y = (int)socketBlockPosition.y;

            if (x < 0 || x >= blocksWide) x = 0;
            if (y < 0 || y >= blocksHigh) y = 0;

            GameObject blockToReplace = wallBlocks[x][y];
            Transform socketTransform = blockToReplace.transform;
            GameObject socketBlock = Instantiate(socketBlockPrefab, socketTransform.position, socketTransform.rotation, transform);
            wallBlocks[x][y] = socketBlock;
            DestroyImmediate(blockToReplace);

            wallSocket = socketBlock.GetComponentInChildren<XRSocketInteractor>();
            wallSocket.selectEntered.AddListener(OnSocketEntered);
            wallSocket.selectExited.AddListener(OnSocketExited);
        }
    }

    private void DeleteWall()
    {
        foreach (List<GameObject> row in wallBlocks)
        {
            foreach (var block in row)
            {
                DestroyImmediate(block);
            }
        }
        wallBlocks.Clear();
    }

    private void OnSocketEntered(SelectEnterEventArgs args)
    {
        foreach (List<GameObject> row in wallBlocks)
        {
            foreach (var block in row)
            {
                Rigidbody rb = block.GetComponent<Rigidbody>();
                rb.isKinematic = false;
            }
        }
    }

    private void OnSocketExited(SelectExitEventArgs args)
    {
        foreach (List<GameObject> row in wallBlocks)
        {
            foreach (var block in row)
            {
                Rigidbody rb = block.GetComponent<Rigidbody>();
                rb.isKinematic = true;
            }
        }
    }
}
