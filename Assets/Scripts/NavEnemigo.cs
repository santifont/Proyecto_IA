using UnityEngine;
using UnityEngine.AI;

public class NavEnemigo : MonoBehaviour
{
    private NavMeshAgent agent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 posicionJugador = GameObject.Find("Chomp").transform.position;
        agent.SetDestination(posicionJugador);
    }
}
