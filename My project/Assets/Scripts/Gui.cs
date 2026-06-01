// CoinUIController.cs
using UnityEngine;
using UnityEngine.UI; // Para UI.Text (UGUI)

public class CoinUIController : MonoBehaviour
{
    [SerializeField] private Text coinText;   // arraste o objeto Text aqui

    private void OnEnable()
    {
        // Assina o evento quando o objeto é ativado
        PlayerObserverManager.OnCoinCollected += UpdateCoinDisplay;
    }

    private void OnDisable()
    {
        // Cancela a assinatura para evitar erros
        PlayerObserverManager.OnCoinCollected -= UpdateCoinDisplay;
    }

    private void UpdateCoinDisplay(int totalCoins)
    {
        if (coinText != null)
            coinText.text = $"Moedas: {totalCoins}";
    }
}