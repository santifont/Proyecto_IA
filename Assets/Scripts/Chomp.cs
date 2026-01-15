using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class Chomp : MonoBehaviour
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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
            Destroy(other);
        }

        if (other.CompareTag("cherry"))
        {
            Debug.Log("You touched a CHERRY");
        }
    }
}
