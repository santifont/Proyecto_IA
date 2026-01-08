using UnityEngine;
using UnityEngine.AI;

public class MovimientoEnemigo : MonoBehaviour
{

    NavMeshAgent agente;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agente = this.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 posicionJugador = GameObject.Find("Jugador").transform.position;
        agente.SetDestination(posicionJugador);
    }
}
