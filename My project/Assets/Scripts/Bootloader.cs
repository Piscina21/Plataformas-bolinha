// Bootloader.cs
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bootloader : MonoBehaviour
{
    void Start()
    {
        // Carrega a cena de Splash
        SceneManager.LoadScene("Splash");
    }
}