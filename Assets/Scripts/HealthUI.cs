using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class HealthUI : MonoBehaviour
{
    public List<Image> heartImages; // Lista de imagens dos corações

    public void UpdateHearts(int currentHealth, int maxHealth)
    {
        Debug.Log($"Atualizando corações: Vida Atual = {currentHealth}, Vida Máxima = {maxHealth}");

        // Garante que o número de corações na interface corresponda à quantidade máxima de saúde
        while (heartImages.Count > maxHealth)
        {
            Destroy(heartImages[heartImages.Count - 1].gameObject);
            heartImages.RemoveAt(heartImages.Count - 1);
        }

        // Atualiza a cor de cada coração
        for (int i = 0; i < heartImages.Count; i++)
        {
            if (i < currentHealth)
            {
                heartImages[i].enabled = true; // Certifica-se de que está ativo
                heartImages[i].color = Color.red; // Coração cheio (vermelho)
            }
            else
            {
                heartImages[i].enabled = true;
                heartImages[i].color = Color.gray; // Coração vazio (cinza)
            }
        }
    }
}
