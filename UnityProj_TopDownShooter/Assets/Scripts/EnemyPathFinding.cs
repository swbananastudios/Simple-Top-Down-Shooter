using UnityEngine;
using UnityEngine.AI;

public class EnemyPathFinding : MonoBehaviour
{
    private NavMeshAgent agent;
    public Transform destinationTransform;

    // Monobehaviour Function: Called when the script instance is loaded
    void Awake()
    {
        // Get NavMeshAgent component that is attachedc to the gameobject
        agent = GetComponent<NavMeshAgent>();
    }

    // Monobehaviour Function: Called when the gameobject/component becomes active
    private void OnEnable() 
    {
        // Have OnGameOver() be called whenever GameOverAction is invoked
        GameManager.GameOverAction += OnGameOver; 
    }

    // Monobehaviour Function: Called when the gameobject/component becomes inactive
    private void OnDisable() 
    {
        // Stop having OnGameOver() be called whenever GameOverAction is invoked
        GameManager.GameOverAction -= OnGameOver;
    }

    // Monobehaviour Function: Called every frame
    void Update()
    {
        // Make enemy move towards the destination, which in our case, is the player
        agent.destination = destinationTransform.position;
    }

    public void OnGameOver() 
    {
        // Make enemy stop moving
        agent.isStopped = true;
    }
}
