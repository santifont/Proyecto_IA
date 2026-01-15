using UnityEngine;

public class Puntaje : MonoBehaviour
{
    private int puntaje = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void sumarPunto()
    {
        puntaje++;
        Debug.Log("Punto sumado");
    }
}
