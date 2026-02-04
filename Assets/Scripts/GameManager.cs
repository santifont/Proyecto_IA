using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.AI;

public class GameManager : MonoBehaviour
{
    // CANVAS y BOLAS
    private GameObject[]    enemy;
    private GameObject[]    cantidadBolas;
    private TextMeshProUGUI enemigosRestantes;
    private TextMeshProUGUI bolasRestantes;

    // CORUTINAS
    public  bool danger = true;
    public  GameObject   cherry;
    public  GameObject   bigGhost;
    public  GameObject[] smallGhost;
    private GameObject[] smallEnemySpawns;
    private GameObject[] bigEnemySpawns;
    private GameObject[] cherrySpawns;

    // CONTADORES
    private GameObject dataBase;
    public int   enemyCounter = 0;
    public float gameTime     = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // CANVAS y BOLAS
        enemigosRestantes = GameObject.Find("EnemigosRestantes").GetComponent<TextMeshProUGUI>();
        bolasRestantes    = GameObject.Find("BolasRestantes").GetComponent<TextMeshProUGUI>();

        // CORUTINAS
        smallEnemySpawns = GameObject.FindGameObjectsWithTag("smallEnemyS");
        bigEnemySpawns   = GameObject.FindGameObjectsWithTag("bigEnemyS");
        cherrySpawns     = GameObject.FindGameObjectsWithTag("cherrySpawn");

        StartCoroutine(Enemies());
        Cherry();

        // BASE DE DATOS
        dataBase = GameObject.Find("DDOL"); // GetComponent<DataBase>();
    }

    // Update is called once per frame
    void Update()
    {
        cantidadBolas = GameObject.FindGameObjectsWithTag("ball");
        enemy         = GameObject.FindGameObjectsWithTag("Enemy");
        enemigosRestantes.text = "Enemigos\nRestantes\n" + enemy.Length;
        bolasRestantes.text    = "Bolas\nRestantes\n" + cantidadBolas.Length;

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

        gameTime = Time.deltaTime + gameTime;
        dataBase.GetComponent<DataBase>().gameTime = gameTime;
        dataBase.GetComponent<DataBase>().enemyCounter = enemyCounter;
    }

    IEnumerator Enemies()
    {
        while (danger == true)
        {
            // Small enemy spawns
            GameObject smallInstance =
                Instantiate(smallGhost[Random.Range(0, smallGhost.Length)],
                smallEnemySpawns[Random.Range(0, smallEnemySpawns.Length)].transform.position,
                Quaternion.identity);
            smallInstance.name = "small ghost";

            // Big enemy spawns
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

    // Escenas
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
