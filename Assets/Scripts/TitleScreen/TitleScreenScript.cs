using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenScript : MonoBehaviour
{
    public  bool difficulty = false;
    private bool titleState = true;
    private GameObject   settings;
    private GameObject   title;
    private GameObject[] buttons;
    private DataBase   dataBase;
    private TextMeshProUGUI difficultyText;

    private void Start()
    {
        dataBase = GameObject.Find("DataBase").GetComponent<DataBase>();
        // CANVASES
        settings = GameObject.Find("SettingsCanvas");
        title    = GameObject.Find("Canvas");
        // BOTONES
        buttons  = GameObject.FindGameObjectsWithTag("button");
        // TEXTO
        difficultyText = GameObject.Find("DifficultyText").GetComponent<TextMeshProUGUI>();
        // ESTADO DE LOS CANVASES
        settings.SetActive(false);
    }

    public void ChangeCanvas()
    {
        if (titleState == false)
        {
            titleState = true;
            title.SetActive(true);
            settings.SetActive(false);
        }
        else if (titleState == true)
        {
            titleState = false;
            title.SetActive(false);
            settings.SetActive(true);
        }
    }

    public void HardDifficulty()
    {
        difficulty = true;
        dataBase.difficulty = difficulty;
        StartCoroutine(DifficultyChange());
        Debug.Log(dataBase.difficulty);
    }

    public void EasyDifficulty()
    {
        difficulty = false;
        dataBase.difficulty = difficulty;
        StartCoroutine(DifficultyChange());
        Debug.Log(dataBase.difficulty);
    }

    IEnumerator DifficultyChange()
    {
        if (difficulty == false)
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].SetActive(false);
            }
            difficultyText.text = "You changed the difficulty to Easy";
            yield return new WaitForSeconds(2f);
            difficultyText.text =
            "Choose the difficulty." +
            "\nIt's currently set to Easy";
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].SetActive(true);
            }
        }
        else if (difficulty == true)
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].SetActive(false);
            }
            difficultyText.text = "You changed the difficulty to Hard";
            yield return new WaitForSeconds(2f);
            difficultyText.text =
            "Choose the difficulty." +
            "\nIt's currently set to Hard";
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].SetActive(true);
            }
        }
    }

    // CAMBIO DE ESCENA
    public void PlayGame()
    {
        SceneManager.LoadScene("ChompMan");
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
