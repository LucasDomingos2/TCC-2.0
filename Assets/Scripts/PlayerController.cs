using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public Transform startPosition;
    public Transform attackPosition;
    public float moveSpeed = 5f;
    public Animator animator;

    private bool isAttacking = false;

    // Variáveis de vida
    public int maxHealth = 5; // Quantidade máxima de corações
    private int currentHealth;

    public HealthUI healthUI; // Referência ao script de interface para a vida

    void Start()
    {
        // Inicializa a saúde atual com a quantidade máxima
        currentHealth = maxHealth;

        // Atualiza a interface com a vida inicial
        healthUI.UpdateHearts(currentHealth, maxHealth);
    }

    public void MoveToAttackPosition()
    {
        if (!isAttacking) // Evita iniciar outra sequência de ataque se já estiver atacando
        {
            isAttacking = true;
            StartCoroutine(MoveAndAttackSequence());
        }
    }

    private IEnumerator MoveAndAttackSequence()
    {
        animator.SetBool("Run", true);

        while (Vector3.Distance(transform.position, attackPosition.position) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, attackPosition.position, moveSpeed * Time.deltaTime);
            yield return null;
        }

        animator.SetBool("Run", false);

        // Ativar animação de ataque com um tempo de espera fixo
        animator.SetTrigger("Attack");

        // Use uma duração fixa para garantir que o ataque seja concluído antes do retorno
        yield return new WaitForSeconds(1.0f); // Ajuste para a duração exata da sua animação de ataque

        // Voltar para a posição inicial com a animação de correr novamente
        animator.SetBool("Run", true);

        while (Vector3.Distance(transform.position, startPosition.position) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, startPosition.position, moveSpeed * Time.deltaTime);
            yield return null;
        }

        animator.SetBool("Run", false);
        isAttacking = false;
    }

    public void TakeDamage(int damage)
    {
        Debug.Log($"Tomando dano: {damage}"); // Log para saber se o método foi chamado
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        healthUI.UpdateHearts(currentHealth, maxHealth);

        if (currentHealth <= 0)
        {
            Debug.Log("Game Over");
        }
    }

}
