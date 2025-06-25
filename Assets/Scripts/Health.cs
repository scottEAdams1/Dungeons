using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.VFX;

public class Health : MonoBehaviour
{
    /// <summary>
    /// The entities maximum amount of health
    /// </summary>
    public int maxHealth = 10;

    /// <summary>
    /// The entities current health
    /// </summary>
    public int CurrentHealth { get; private set; }

    // Effects for the Player, to prevent damage
    bool isInvincible = false;
    float damageCooldown;
    public float timeInvincible = 1.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        CurrentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        // Countdown until no longer invincible
        if (isInvincible)
        {
            damageCooldown -= Time.deltaTime;
            if (damageCooldown < 0)
            {
                isInvincible = false;
            }
        }

        // Death
        if (CurrentHealth <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Alter the entities health, both positive and negative
    /// </summary>
    /// <param name="amount">The amount by which to change the health (+ heals, - damages)</param>
    public void ChangeHealth(int amount)
    {
        // Damage to the Player
        if (amount < 0 && gameObject.CompareTag("Player"))
        {
            // No effect if recently damaged
            if (isInvincible)
            {
                return;
            }
            // Set to invincible to prevent too much damage at once
            isInvincible = true;
            damageCooldown = timeInvincible;
        }
        // Alter health, clamped between 0 and max health
        CurrentHealth = Mathf.Clamp(CurrentHealth + amount, 0, maxHealth);

        // Show changes to the Player's health in the UI
        if (gameObject.CompareTag("Player"))
        {
            UIHandler.instance.SetHealthValue(CurrentHealth / (float)maxHealth);
        }
    }

    public string HealthOutOfMax()
    {
        return CurrentHealth.ToString() + "/" + maxHealth.ToString();
    }
}
