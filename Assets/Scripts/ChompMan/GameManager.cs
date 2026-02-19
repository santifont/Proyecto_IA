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
    private TextMeshProUGUI powerUpTimer;
    private TextMeshProUGUI timer;

    // CORUTINAS
    public  bool danger = true;
    public  GameObject   cherry;
    public  GameObject   bigGhost;
    public  GameObject[] smallGhost;
    private GameObject[] smallEnemySpawns;
    private GameObject[] bigEnemySpawns;
    private GameObject randomSmallPos;
    private GameObject randomBigPos;
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
        powerUpTimer      = GameObject.Find("PowerUpTimer").GetComponent<TextMeshProUGUI>();
        powerUpTimer.enabled = false;

        // SPAWNS
        smallEnemySpawns = GameObject.FindGameObjectsWithTag("smallEnemyS");
        bigEnemySpawns   = GameObject.FindGameObjectsWithTag("bigEnemyS");
        cherrySpawns     = GameObject.FindGameObjectsWithTag("cherrySpawn");

        for (int i = 0; i < smallEnemySpawns.Length; i++)
        {
            smallEnemySpawns[i].SetActive(false);
        }

        for (int i = 0; i < bigEnemySpawns.Length; i++)
        {
            bigEnemySpawns[i].SetActive(false);
        }

        // CORUTINAS
        StartCoroutine(Enemies());
        Cherry();

        // BASE DE DATOS
        dataBase = GameObject.Find("DataBase").GetComponent<DataBase>();
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
                enemy[i].GetComponent<NavMeshAgent>().speed = 50f;
            }
        }
        else if (danger == false)
        {
            for (int i = 0; i < enemy.Length; i++)
            {
                enemy[i].GetComponent<Renderer>().material.color = Color.blue;
                enemy[i].GetComponent<NavMeshAgent>().speed = 25f;
            }
        }

        // CONTROL DE ENEMIGOS EN PANTALLA
        if (enemy.Length >= 8)
        {
            danger = false;
            Debug.Log("ENEMY LIMIT REACHED");
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
        dataBase.gameTime = gameTime;
        dataBase.enemyCounter = enemyCounter;
    }

    // MÉTODOS Y CORUTINAS DE ENEMIES

    IEnumerator Enemies()
    {
        while (danger == true)
        {
            Debug.Log(danger);
            // ENEMIGOS PEQUEÑOS Y GRANDES para acceder a su transform.position
            randomSmallPos = smallEnemySpawns[Random.Range(0, smallEnemySpawns.Length)];
            randomBigPos   = bigEnemySpawns[Random.Range(0, bigEnemySpawns.Length)];

            // Avisa de dónde aparecerán los enemigos con parpadeos.
            StartCoroutine(BlinkingIndicator());
;           yield return new WaitForSeconds(3f);

            // Instancia los enemigos
            GameObject smallInstance = Instantiate(smallGhost[Random.Range(0, smallGhost.Length)], randomSmallPos.transform.position, Quaternion.identity);
            GameObject bigInstance   = Instantiate(bigGhost, randomBigPos.transform.position, Quaternion.identity);
            smallInstance.name = "small ghost";
            bigInstance.name   = "big ghost";
            yield return new WaitForSeconds(7f);
        }
    }

    IEnumerator BlinkingIndicator()
    {
        bool blink = true;
        for (float i = 0; i < 10; i++)
        {
            randomSmallPos.SetActive(blink);
            randomBigPos.SetActive(blink);
            yield return new WaitForSeconds(0.3f);
            blink = !blink;
        }
        randomSmallPos.SetActive(false);
        randomBigPos.SetActive(false);
    }

    // MÉTODOS Y CORUTINAS DE CHERRY Y POWERPHASE

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
        StartCoroutine(PowerPhaseTimer());
    }

    IEnumerator PowerPhase()
    {
        danger = false;
        yield return new WaitForSeconds(10f);        
        danger = true;
        Cherry();
        StartCoroutine(Enemies());
    }

    IEnumerator PowerPhaseTimer()
    {
        powerUpTimer.enabled = true;
        for (int i = 10; i > 0; i--) // El valor de la i tiene que ser el mismo que el valor del WaitForSeconds de la corutina "PowerPhase()".
        {
            powerUpTimer.text = "POWER UP! " + i + "s";
            yield return new WaitForSeconds(1f);
        }
        powerUpTimer.enabled = false;
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
