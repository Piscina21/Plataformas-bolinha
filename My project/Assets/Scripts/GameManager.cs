using UnityEngine;
using UnityEngine.SceneManagement;



public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public GameState CurrentState { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        
        ChangeState(GameState.Iniciando);
    }

    public void ChangeState(GameState newState)
    {
        if (CurrentState == newState)
            return;

        CurrentState = newState;
        Debug.Log($"Estado do Jogo alterado para: {CurrentState}");

        switch (CurrentState)
        {
            case GameState.Iniciando:
                
                break;
            case GameState.MenuPrincipal:
                break;
               
        }
    }

    public void LoadScene(string sceneName)
    {
        Debug.Log($"Solicitação para carregar cena: {sceneName} a partir do estado {CurrentState}");

        if (sceneName == "SampleScene" && CurrentState != GameState.MenuPrincipal)
        {
            Debug.LogWarning("Tentativa de iniciar o jogo fora do Menu Principal foi bloqueada.");
            return;
        }

        if (sceneName == "MenuPrincipal")
        {
            ChangeState(GameState.MenuPrincipal);
        }
        else if (sceneName == "SampleScene")
        {
            ChangeState(GameState.Gameplay);
        }

        SceneManager.LoadSceneAsync(sceneName);
    }
}