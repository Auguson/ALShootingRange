using UnityEngine;
using UnityEngine.AI;

public class EnemyFollowPlayer : MonoBehaviour
{
    private NavMeshAgent agent;
    [SerializeField]
    private Transform player;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (!player)
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (player == null) return;
        agent.SetDestination(player.position);
    }
}