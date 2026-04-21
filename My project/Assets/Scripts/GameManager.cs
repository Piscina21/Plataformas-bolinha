using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // --- Singleton ---
    public static GameManager Instance { get; private set; }

    // --- Estados do Jogo ---
    public GameState CurrentState { get; private set; }

    private void Awake()
    {
        // Implementação do Singleton
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject); // Garante que o GameManager sobreviva entre cenas
    }

    private void Start()
    {
        // O jogo começa no estado "Iniciando" ao carregar a cena Boot
        ChangeState(GameState.Iniciando);
        // A partir daqui, a lógica da cena Boot (ou Splash) que comanda o próximo passo
    }

    public void ChangeState(GameState newState)
    {
        if (CurrentState == newState)
            return;

        CurrentState = newState;
        Debug.Log($"Estado do Jogo alterado para: {CurrentState}");

        // --- Lógica de transição de estado (simplificada com switch) ---
        // Aqui você pode adicionar comportamentos específicos ao entrar em um novo estado
        switch (CurrentState)
        {
            case GameState.Iniciando:
                // Geralmente não faz nada, apenas define o estado
                break;
            case GameState.MenuPrincipal:
                // Exemplo: Pausar a música do jogo, liberar o cursor do mouse
                break;
           
        }
    }

    // Método para solicitar mudança de cena. Centraliza a verificação de estado.
    public void LoadScene(string sceneName)
    {
        Debug.Log($"Solicitação para carregar cena: {sceneName} a partir do estado {CurrentState}");

        // --- Validação de Estado (Item 2 da sua atividade) ---
        // Exemplo: Só permite carregar a SampleScene se estiver no Menu Principal
        if (sceneName == "SampleScene" && CurrentState != GameState.MenuPrincipal)
        {
            Debug.LogWarning("Tentativa de iniciar o jogo fora do Menu Principal foi bloqueada.");
            return;
        }
        // Adicione outras validações conforme necessário

        // Atualiza o estado ANTES de carregar a nova cena
        if (sceneName == "MenuPrincipal")
        {
            ChangeState(GameState.MenuPrincipal);
        }
        else if (sceneName == "SampleScene")
        {
            ChangeState(GameState.Gameplay);
        }

        // Carrega a cena de forma assíncrona (boa prática para evitar travamentos)
        SceneManager.LoadSceneAsync(sceneName);
    }
}