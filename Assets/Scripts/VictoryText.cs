using TMPro;
using UnityEngine;

public class VictoryText : MonoBehaviour
{
    private TextMeshProUGUI puntaje;
    private GameObject dataBase;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        puntaje  = GameObject.Find("Puntaje").GetComponent<TextMeshProUGUI>();
        dataBase = GameObject.Find("DDOL");

        //dataBase.GetComponent<DataBase>().gameTime;

        float minutes = Mathf.FloorToInt(dataBase.GetComponent<DataBase>().gameTime / 60);
        float seconds = Mathf.FloorToInt(dataBase.GetComponent<DataBase>().gameTime % 60);

        puntaje.text =
            "Tiempo empleado: " + string.Format("{0:00}:{1:00}", minutes, seconds) +
            "\nEnemigos destruidos: " + dataBase.GetComponent<DataBase>().enemyCounter;
    }
}
