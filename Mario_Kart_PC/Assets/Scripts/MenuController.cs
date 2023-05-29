using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [Header("Escenas a cargar")]
    public string unJugador;
    public string multiJugador;

    public void singlePlayer()
    {
        SceneManager.LoadScene(unJugador);
    }
    public void multyPlayer()
    {
        SceneManager.LoadScene(multiJugador);
    }

    public void botonSalir()
    {
        Application.Quit();
    }
}
