
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    

    public void OnStartButtonClicked()
    {
        Debug.Log("Botão Iniciar clicado.");
        
        if (GameManager.Instance != null)
        {
            GameManager.Instance.LoadScene("SampleScene");
        }
        else
        {
            SceneManager.LoadScene("SampleScene");
        }
    }

    public void OnQuitButtonClicked()
    {
        Debug.Log("Botão Sair clicado.");
       
        Application.Quit();
        
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}