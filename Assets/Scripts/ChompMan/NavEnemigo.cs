using UnityEngine;
using UnityEngine.AI;

public class NavEnemigo : MonoBehaviour
{
    private NavMeshAgent agent;
    private GameManager gameManager;
    private Vector3 playerPos;
    private Vector3 runawayPos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        agent = this.GetComponent<NavMeshAgent>();
        runawayPos = GameObject.Find("EnemiesRunaway").transform.position;

    }

    // Update is called once per frame
    void Update()
    {

        if (gameManager.danger == true)
        {
            //Vector3 posicionJugador = GameObject.Find("Chomp").transform.position;
            playerPos = GameObject.Find("Chomp").transform.position;
            agent.SetDestination(playerPos);
        }
        else if (gameManager.danger == false)
        {
            agent.SetDestination(runawayPos);
        }
    }
}