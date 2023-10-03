using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

[RequireComponent(typeof(NavMeshAgent))]
public class NavMeshRobot : MonoBehaviour
{
    public UnityEvent OnDestroyWallCube;

    NavMeshAgent agent = null;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();  
    }

    public void MoveAgent(Vector3 move)
    {
        agent.destination = agent.transform.position + move;
    }

    public void StopAgent()
    {
        agent.ResetPath();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Wall Cube"))
        {
            Destroy(other.gameObject);
            OnDestroyWallCube?.Invoke();
        }
    }
}
