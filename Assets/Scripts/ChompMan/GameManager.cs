using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.AI;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    // DIFICULTAD
    private bool difficulty = false; // Easy = false; Hard = true;

    // CANVAS y BOLAS
    private GameObject[]    enemy;
    private GameObject[]    cantidadBolas;
    private TextMeshProUGUI enemigosRestantes;
    private TextMeshProUGUI bolasRestantes;
    private TextMeshProUGUI timer;

    // CORUTINAS
    public  bool danger = true;
    public  GameObject   cherry;
    public  GameObject   bigGhost;
    public  GameObject[] smallGhost;
    private GameObject[] smallEnemySpawns;
    private GameObject[] bigEnemySpawns;
    private GameObject[] cherrySpawns;

    // CONTADORES
    private DataBase dataBase;
    public int   enemyCounter = 0;
    public float gameTime     = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // CANVAS y BOLAS
        enemigosRestantes = GameObject.Find("EnemigosRestantes").GetComponent<TextMeshProUGUI>();
        bolasRestantes    = GameObject.Find("BolasRestantes").GetComponent<TextMeshProUGUI>();
        timer             = GameObject.Find("Timer").GetComponent<TextMeshProUGUI>();

        // CORUTINAS
        smallEnemySpawns = GameObject.FindGameObjectsWithTag("smallEnemyS");
        bigEnemySpawns   = GameObject.FindGameObjectsWithTag("bigEnemyS");
        cherrySpawns     = GameObject.FindGameObjectsWithTag("cherrySpawn");

        StartCoroutine(Enemies());
        Cherry();

        // BASE DE DATOS
        //dataBase = GameObject.Find("DataBase").GetComponent<DataBase>();
    }

    // Update is called once per frame
    void Update()
    {
        // PARÁMETROS
        cantidadBolas = GameObject.FindGameObjectsWithTag("ball");
        enemy         = GameObject.FindGameObjectsWithTag("Enemy");
        enemigosRestantes.text = enemy.Length + "";
        bolasRestantes.text    = cantidadBolas.Length + "";

        if (cantidadBolas.Length == 0)
        {
            VictoryScreen();
        }
        if (danger == true)
        {
            for (int i = 0; i < enemy.Length; i++)
            {
                enemy[i].GetComponent<Renderer>().material.color = Color.red;
                enemy[i].GetComponent<NavMeshAgent>().speed = 3.5f;
            }
        }
        else if (danger == false)
        {
            for (int i = 0; i < enemy.Length; i++)
            {
                enemy[i].GetComponent<Renderer>().material.color = Color.blue;
                enemy[i].GetComponent<NavMeshAgent>().speed = 2f;
            }
        }

        // TEMPORIZADOR
        gameTime = Time.deltaTime + gameTime;
        // "Mathf.Round redonde al número entero más cercano"
        // "Mathf.Floor" trunca al número entero actual o anterior. "Mathf.FloorToInt" hace lo mismo pero convierte a int".
        // "Mathf.Ceil" y "Math.CeilToInt" hace lo contrario.
        float minutes = Mathf.FloorToInt(gameTime / 60);
        float seconds = Mathf.FloorToInt(gameTime % 60);
        // Formato de minutos con segundos
        timer.text = string.Format("{0:00}:{1:00}", minutes, seconds);



        // BASE DE DATOS
       // dataBase.gameTime = gameTime;
       //dataBase.enemyCounter = enemyCounter;
    }

    IEnumerator Enemies()
    {
        while (danger == true)
        {
            // ENEMIGOS PEQUEÑOS
            GameObject smallInstance =
                Instantiate(smallGhost[Random.Range(0, smallGhost.Length)],
                smallEnemySpawns[Random.Range(0, smallEnemySpawns.Length)].transform.position,
                Quaternion.identity);
            smallInstance.name = "small ghost";

            // ENEMIGOS GRANDES
            GameObject bigInstance =
                Instantiate(bigGhost, bigEnemySpawns[Random.Range(0,
                bigEnemySpawns.Length)].transform.position,
                Quaternion.identity);
            bigInstance.name = "big ghost";
            yield return new WaitForSeconds(10f);
        }
    }

    public void Cherry()
    {
        GameObject cherryInstance =
                Instantiate(cherry,
                cherrySpawns[Random.Range(0, cherrySpawns.Length)].transform.position,
                Quaternion.identity);
        cherryInstance.name = "cherry";
    }

    public void PowerPhaseMethod()
    {
            StartCoroutine(PowerPhase());
    }

    IEnumerator PowerPhase()
    {
        danger = false;
        yield return new WaitForSeconds(10f);        
        danger = true;
        Cherry();
        StartCoroutine(Enemies());
    }

    // ESCENAS
    public void VictoryScreen()
    {
        StopAllCoroutines();
        danger = false;
        SceneManager.LoadScene("WinScene");
    }

    public void GameOver()
    {
        StopAllCoroutines();
        SceneManager.LoadScene("GameOver");
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("ChompMan2");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
