using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class Chomp : MonoBehaviour
{
    private GameObject gameManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameObject.Find("GameManager");
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ball"))
        {
            Debug.Log("BALL");
            Destroy(other.gameObject);
            gameManager.GetComponent<Puntaje>().sumarPunto();

        }

        if (other.CompareTag("cherry"))
        {
            Debug.Log("You touched a CHERRY");
        }
    }
}
