// PlayerObserverManager.cs
using System;

public static class PlayerObserverManager
{
    // Evento disparado quando o jogador coleta uma moeda. Passa o total acumulado.
    public static event Action<int> OnCoinCollected;

    public static void CoinCollected(int totalCoins)
    {
        OnCoinCollected?.Invoke(totalCoins);
    }
}