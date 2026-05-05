
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreenController : MonoBehaviour
{
    public float displayTime = 2f; 

    IEnumerator Start()
    {
        
        yield return new WaitForSeconds(displayTime);
        
        if (GameManager.Instance != null)
        {
            GameManager.Instance.LoadScene("MenuPrincipal");
        }
        else
        {
            
            SceneManager.LoadScene("MenuPrincipal");
        }
    }
}