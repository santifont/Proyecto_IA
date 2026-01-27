using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class Chomp : MonoBehaviour
{
    private GameManager gameManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.position.x < -20.95f)
        {
            transform.position = new Vector3(14.0f, transform.position.y, transform.position.z);
        }

        if (gameObject.transform.position.x > 18.4f)
        {
            transform.position = new Vector3(-18.5f, transform.position.y, transform.position.z);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ball"))
        {
            Destroy(other.gameObject);
            Debug.Log("BOLA");
        }

        if (other.CompareTag("cherry"))
        {
            Destroy(other.gameObject);
            gameManager.Cherry();
        }

        if (other.CompareTag("Enemy"))
        {
            gameManager.GameOver();
        }
    }
}
