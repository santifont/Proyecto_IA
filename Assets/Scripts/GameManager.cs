using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private GameObject[] cantidadFantasmas;
    private GameObject[] cantidadBolas;
    private TextMeshProUGUI enemigosRestantes;
    private TextMeshProUGUI bolasRestantes;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemigosRestantes = GameObject.Find("EnemigosRestantes").GetComponent<TextMeshProUGUI>();
        bolasRestantes    = GameObject.Find("BolasRestantes").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        cantidadBolas     = GameObject.FindGameObjectsWithTag("ball");
        cantidadFantasmas = GameObject.FindGameObjectsWithTag("Enemy");
        enemigosRestantes.text = "Enemigos\nRestantes\n" + cantidadFantasmas.Length;
        bolasRestantes.text    = "Bolas\nRestantes\n" + cantidadBolas.Length;

        if (cantidadBolas.Length == 0)
        {
            VictoryScreen();
        }
    }

    public void Cherry()
    {
        Debug.Log("Cherry!");
        for (int i = 0; i < cantidadFantasmas.Length; i++)
        {
            Destroy(cantidadFantasmas[i]);
        }
    }

    // Escenas
    public void VictoryScreen()
    {
        SceneManager.LoadScene("WinScene");
    }

    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("ChompMan");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
