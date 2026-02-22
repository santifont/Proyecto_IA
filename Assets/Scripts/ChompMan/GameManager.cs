using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.AI;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    // DIFICULTAD
    [Header("     MONSTERS' SPEED")]
    private bool difficulty; // Easy = false; Hard = true;

    // CORUTINAS
    public  bool danger = true;
    public  GameObject   cherry;
    public  GameObject   bigGhost;
    public  GameObject[] smallGhost;
    private GameObject[] smallEnemySpawns;
    private GameObject[] bigEnemySpawns;
    private GameObject   randomSmallPos;
    private GameObject   randomBigPos;
    private GameObject[] cherrySpawns;

    // CANVAS y BOLAS
    private GameObject[]    enemy;
    private GameObject[]    cantidadBolas;
    private TextMeshProUGUI enemigosRestantes;
    private TextMeshProUGUI bolasRestantes;
    private TextMeshProUGUI powerUpTimer;
    private TextMeshProUGUI timer;

    // CONTADORES
    private DataBase dataBase;
    public int   enemyCounter = 0;
    public float gameTime     = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // DIFICULTAD
        difficulty = GameObject.Find("DataBase").GetComponent<DataBase>().difficulty; // Coge la dificultad de la base de datos

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

        if (difficulty == true)
        {
            if (danger == true)
            {
                for (int i = 0; i < enemy.Length; i++)
                {
                    enemy[i].GetComponent<Renderer>().material.color = Color.red;
                    enemy[i].GetComponent<NavMeshAgent>().speed = 12f;
                }
            }
            else if (danger == false)
            {
                for (int i = 0; i < enemy.Length; i++)
                {
                    enemy[i].GetComponent<Renderer>().material.color = Color.blue;
                    enemy[i].GetComponent<NavMeshAgent>().speed = 6f;
                }
            }
        }
        else if (difficulty == false)
        {
            if (danger == true)
            {
                for (int i = 0; i < enemy.Length; i++)
                {
                    enemy[i].GetComponent<Renderer>().material.color = Color.red;
                    enemy[i].GetComponent<NavMeshAgent>().speed = 10f;
                }
            }
            else if (danger == false)
            {
                for (int i = 0; i < enemy.Length; i++)
                {
                    enemy[i].GetComponent<Renderer>().material.color = Color.blue;
                    enemy[i].GetComponent<NavMeshAgent>().speed = 5f;
                }
            }
        }

        // CONTROL DE ENEMIGOS EN PANTALLA
        if (enemy.Length >= 8)
        {
            danger = false;
        }

        // TEMPORIZADOR
        gameTime = Time.deltaTime + gameTime;
        float minutes = Mathf.FloorToInt(gameTime / 60);
        float seconds = Mathf.FloorToInt(gameTime % 60);
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
            if (difficulty == true)
            {
                yield return new WaitForSeconds(2f); // 5 segundos de powerup
            }
            else
            {
                yield return new WaitForSeconds(7f); // 10 segundos de powerup
            }
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
        if ( difficulty == true)
        {
            danger = false;
            yield return new WaitForSeconds(5f);
            danger = true;
            Cherry();
            StartCoroutine(Enemies());
        }
        else 
        {
            danger = false;
            yield return new WaitForSeconds(10f);        
            danger = true;
            Cherry();
            StartCoroutine(Enemies());
        }
    }

    IEnumerator PowerPhaseTimer()
    {
        powerUpTimer.enabled = true;
        if (difficulty == true)
        {
            for (int i = 5; i > 0; i--) // El valor de la i tiene que ser el mismo que el valor del WaitForSeconds de la corutina "PowerPhase()".
            {
                powerUpTimer.text = "POWER UP! " + i + "s";
                yield return new WaitForSeconds(1f);
            }
        }
        else if (difficulty == false)
        {
            for (int i = 10; i > 0; i--) // El valor de la i tiene que ser el mismo que el valor del WaitForSeconds de la corutina "PowerPhase()".
            {
                powerUpTimer.text = "POWER UP! " + i + "s";
                yield return new WaitForSeconds(1f);
            }
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
