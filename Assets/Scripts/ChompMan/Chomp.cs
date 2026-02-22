using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class Chomp : MonoBehaviour
{
    private GameManager gameManager;
    private bool difficulty;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        difficulty  = GameObject.Find("DataBase").GetComponent<DataBase>().difficulty;
    }

    // Update is called once per frame
    void Update()
    {
        if (difficulty == false)
        {
            if (gameObject.transform.position.z < -13.2f)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, 20f);
            }

            if (gameObject.transform.position.z > 26.7f)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, -7f);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ball"))
        {
            Destroy(other.gameObject);
        }

        if (other.CompareTag("cherry"))
        {
            Destroy(other.gameObject);
            Debug.Log("Cherry obtenida");
            gameManager.PowerPhaseMethod();
        }

        if (other.CompareTag("Enemy") && gameManager.danger == true)
        {
            gameManager.GameOver();
        }
        else if (other.CompareTag("Enemy") && gameManager.danger == false)
        {
            Destroy(other.gameObject);
            gameManager.enemyCounter++;
        }
    }
}
