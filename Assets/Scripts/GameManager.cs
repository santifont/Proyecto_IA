using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int bolas = 0;
    private int fantasmas = 0;
    private GameObject[] cantidadFantasmas;
    private GameObject[] cantidadBolas;
    private TextMeshProUGUI enemigosRestantes;
    private TextMeshProUGUI bolasRestantes;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemigosRestantes = GameObject.Find("EnemigosRestantes").GetComponent<TextMeshProUGUI>();
        bolasRestantes = GameObject.Find("BolasRestantes").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        cantidadBolas = GameObject.FindGameObjectsWithTag("ball");
        cantidadFantasmas = GameObject.FindGameObjectsWithTag("Enemy");

        if (cantidadFantasmas.Length == null)
        {
            enemigosRestantes.text = "Enemigos\nRestantes\n" + 0;
        }
        else if (cantidadFantasmas.Length != null)
        {
            enemigosRestantes.text = "Enemigos\nRestantes\n" + cantidadFantasmas.Length;
        }

        bolasRestantes.text = "Bolas\nRestantes\n" + cantidadBolas.Length;
    }

    public void RestarBola()
    {
        bolas--;
        Debug.Log("-1 ball!");
    }

    public void Cherry()
    {
        Debug.Log("Cherry!");
        for (int i = 0; i < cantidadFantasmas.Length; i++)
        {
            Destroy(cantidadFantasmas[i]);
        }
    }
}
