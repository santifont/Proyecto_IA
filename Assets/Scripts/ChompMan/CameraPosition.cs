using UnityEngine;

public class CameraPosition : MonoBehaviour
{
    private bool difficulty;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        difficulty = GameObject.Find("DataBase").GetComponent<DataBase>().difficulty;

        if (difficulty == true)
        {
            gameObject.transform.position = new Vector3(-1.49f, 125f, 2f);
        }
        else if (difficulty == false)
        {
            gameObject.transform.position = new Vector3(-1.1f, 173.1366f, 9f);
        }
    }
}
