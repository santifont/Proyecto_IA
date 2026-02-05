using UnityEngine;

public class DataBase : MonoBehaviour
{
    public bool  difficulty = false;
    public float gameTime     = 0f;
    public int   enemyCounter = 0;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
