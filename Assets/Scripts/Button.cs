using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    private bool difficulty;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        difficulty = GameObject.Find("DataBase").GetComponent<DataBase>().difficulty;
    }

    public void PlayGame()
    {
        if (difficulty == false)
        {
            SceneManager.LoadScene("ChompManEASY");
        }
        else if (difficulty == true)
        {
            SceneManager.LoadScene("ChompManHARD");
        }        
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
