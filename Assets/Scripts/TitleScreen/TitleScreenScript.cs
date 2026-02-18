using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenScript : MonoBehaviour
{
    public  bool difficulty = false; // false = facil; true = difícil.
    private bool titleState = true;
    private GameObject   difficultyCanvas;
    private GameObject   titleCanvas;
    private GameObject   creditsCanvas;
    private GameObject[] buttons;
    private DataBase     dataBase;
    private TextMeshProUGUI difficultyText;

    private void Start()
    {
        // BASE DE DATOS
        dataBase = GameObject.Find("DataBase").GetComponent<DataBase>();

        // CANVASES
        difficultyCanvas = GameObject.Find("DifficultyCanvas");
        titleCanvas      = GameObject.Find("TitleCanvas");
        creditsCanvas    = GameObject.Find("CreditsCanvas");

        // BOTONES
        buttons  = GameObject.FindGameObjectsWithTag("button");

        // TEXTO
        difficultyText = GameObject.Find("DifficultyText").GetComponent<TextMeshProUGUI>();

        // ESTADO DE LOS CANVASES
        difficultyCanvas.SetActive(false);
        creditsCanvas.SetActive(false);
    }

    public void FromTitleToDifficulty()
    {
        if (titleState == false)
        {
            titleState = true;
            titleCanvas.SetActive(true);
            difficultyCanvas.SetActive(false);
        }
        else if (titleState == true)
        {
            titleState = false;
            titleCanvas.SetActive(false);
            difficultyCanvas.SetActive(true);
        }
    }

    public void FromTitleToCredits()
    {
        if (titleState == false)
        {
            titleState = true;
            titleCanvas.SetActive(true);
            creditsCanvas.SetActive(false);
        }
        else if (titleState == true)
        {
            titleState = false;
            titleCanvas.SetActive(false);
            creditsCanvas.SetActive(true);
        }
    }

    public void HardDifficulty()
    {
        difficulty = true;
        dataBase.difficulty = difficulty;
        Debug.Log("Hard = " + dataBase.difficulty); // el valor debería ser true
        StartCoroutine(DifficultyChosen());
    }

    public void EasyDifficulty()
    {
        difficulty = false;
        dataBase.difficulty = difficulty;
        Debug.Log("Easy = " + dataBase.difficulty); // el valor debería ser false
        StartCoroutine(DifficultyChosen());
    }

    IEnumerator DifficultyChosen()
    {
        if (difficulty == false) // FÁCIL
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].SetActive(false);
            }
            difficultyText.text = "You chose EASY";
            yield return new WaitForSeconds(2f);
            for (int i = 0; i < 3; i++)
            {
                difficultyText.text = "Loading game.";
                yield return new WaitForSeconds(0.3f);
                difficultyText.text = "Loading game..";
                yield return new WaitForSeconds(0.3f);
                difficultyText.text = "Loading game...";
                yield return new WaitForSeconds(0.3f);
            }
            SceneManager.LoadScene("ChompMan");
        }
        else if (difficulty == true) // DIFÍCIL
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].SetActive(false);
            }
            difficultyText.text = "You chose HARD";
            yield return new WaitForSeconds(2f);
            for (int i = 0; i < 3; i++)
            {
                difficultyText.text = "Loading game.";
                yield return new WaitForSeconds(0.3f);
                difficultyText.text = "Loading game..";
                yield return new WaitForSeconds(0.3f);
                difficultyText.text = "Loading game...";
                yield return new WaitForSeconds(0.3f);
            }
            SceneManager.LoadScene("ChompMan");
        }
    }

    // SALIR DEL JUEGO
    public void ExitGame()
    {
        Application.Quit();
    }
}
