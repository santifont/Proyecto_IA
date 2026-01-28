using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    // CANVAS y BOLAS
    private GameObject[]    enemy;
    private GameObject[]    cantidadBolas;
    private TextMeshProUGUI enemigosRestantes;
    private TextMeshProUGUI bolasRestantes;

    // CORUTINAS
    private bool loop = true;
    public  GameObject   cherry;
    public  GameObject   bigGhost;
    public  GameObject[] smallGhost;
    private GameObject[] smallEnemySpawns;
    private GameObject[] bigEnemySpawns;
    private GameObject[] cherrySpawns;

    // POWER PHASE

    private GameObject[] activeEnemies;
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
        StartCoroutine(Cherry());
    }

    // Update is called once per frame
    void Update()
    {
        cantidadBolas    = GameObject.FindGameObjectsWithTag("ball");
        enemy            = GameObject.FindGameObjectsWithTag("Enemy");
        enemigosRestantes.text = "Enemigos\nRestantes\n" + enemy.Length;
        bolasRestantes.text    = "Bolas\nRestantes\n" + cantidadBolas.Length;

        if (cantidadBolas.Length == 0)
        {
            VictoryScreen();
        }
    }

    IEnumerator Enemies()
    {
        while (loop == true)
        {
            GameObject smallInstance =
                Instantiate(smallGhost[Random.Range(0, smallGhost.Length)],
                smallEnemySpawns[Random.Range(0, smallEnemySpawns.Length)].transform.position,
                Quaternion.identity);
            smallInstance.name = "small ghost";

            GameObject bigInstance =
                Instantiate(bigGhost, bigEnemySpawns[Random.Range(0,
                bigEnemySpawns.Length)].transform.position,
                Quaternion.identity);
            bigInstance.name = "big ghost";
            yield return new WaitForSeconds(10f);
        }
    }

    IEnumerator Cherry()
    {
        GameObject cherryInstance =
                Instantiate(cherry,
                cherrySpawns[Random.Range(0, cherrySpawns.Length)].transform.position,
                Quaternion.identity);
        cherryInstance.name = "cherry";
        yield return new WaitForSeconds(10f);

    }

    /*IEnumerator PowerPhase()
    {

    }*/

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
