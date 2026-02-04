using UnityEngine;

public class DataBase : MonoBehaviour
{
    public float gameTime     = 0f;
    public int   enemyCounter = 0;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    /*
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }*/
}
