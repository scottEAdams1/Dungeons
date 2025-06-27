using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    /// <summary>
    /// The slider for the Health bar
    /// </summary>
    public Slider healthSlider;

    /// <summary>
    /// The text displaying the current health / max health
    /// </summary>
    public TextMeshProUGUI healthText;

    /// <summary>
    /// The health script
    /// </summary>
    public Health health;

    /// <summary>
    /// The previous health amount
    /// </summary>
    public float lastHealth;

    /// <summary>
    /// The Player's camera
    /// </summary>
    Transform camera;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Set starting values
        camera = Camera.main.transform;
        healthSlider.maxValue = health.maxHealth;
        healthSlider.value = health.maxHealth;
        lastHealth = health.maxHealth;
        healthText.text = health.HealthOutOfMax();
    }

    // Update is called once per frame
    void Update()
    {
        // If there has been a change in health, update the health bar
        if (health.CurrentHealth != lastHealth)
        {
            healthSlider.value = health.CurrentHealth;
            healthText.text = health.HealthOutOfMax();
            lastHealth = health.CurrentHealth;
        }
    }

    private void LateUpdate()
    {
        // Move the health bar so it is always visible to the camera, rotation wise
        transform.rotation = Quaternion.LookRotation(transform.position - camera.position);
    }
}
