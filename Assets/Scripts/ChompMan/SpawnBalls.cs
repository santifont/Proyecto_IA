using UnityEngine;

public class SpawnBalls : MonoBehaviour
{
    private GameObject[] spawn;
    public GameObject ball;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawn = GameObject.FindGameObjectsWithTag("spawnpoint");

        for (int i = 0; i < spawn.Length; i++)
        {
            Instantiate(ball, spawn[i].transform.position, Quaternion.identity);
            Destroy(spawn[i]);
        }

        Debug.Log(spawn.Length);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
