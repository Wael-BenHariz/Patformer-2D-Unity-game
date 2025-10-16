using System.Collections;
using UnityEngine;
using TMPro; // 👈 for TextMeshProUGUI

public class PlayerManager : MonoBehaviour, IDamagable
{
    [Header("Health Settings")]
    [SerializeField] private int maxHealth = 2;
    [SerializeField] private int currentHealth;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI healthText; // 👈 assign in Inspector

    [SerializeField] private PauseMenus menuSystem;

    private Animator animator;
    private PlayerMovement playerMovement;
    private Rigidbody2D rb2d;
    private BoxCollider2D boxCollider;

    void Start()
    {
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        rb2d = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();

        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    public void takeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Max(currentHealth, 0); // prevent negative health

        UpdateHealthUI();

        if (currentHealth <= 0)
        {
            StartCoroutine(gameOverSequence());
        }
    }

    void UpdateHealthUI()
    {
        if (healthText != null)
            healthText.text = currentHealth.ToString();
    }

    public IEnumerator gameOverSequence()
{
    animator.SetBool("IsDead?", true);
    playerMovement.enabled = false;
    rb2d.velocity = Vector2.zero;

    yield return new WaitForSeconds(1.75f);

    rb2d.isKinematic = true;
    boxCollider.enabled = false;

    menuSystem.GameOver();
}
}
