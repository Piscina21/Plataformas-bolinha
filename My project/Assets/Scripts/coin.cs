// Coin.cs
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private int value = 1; // quantas moedas essa unidade vale (padrão 1)

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Obtém o script do jogador e adiciona a moeda
            PlayerController collector = other.GetComponent<PlayerController>();
            if (collector != null)
            {
                collector.AddCoins(value);
                Destroy(gameObject);
            }
        }
    }
}