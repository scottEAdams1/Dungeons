using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthSlider;
    public TextMeshProUGUI healthText;
    public Health health;
    public float lastHealth;
    Transform camera;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        camera = Camera.main.transform;
        healthSlider.maxValue = health.maxHealth;
        healthSlider.value = health.maxHealth;
        lastHealth = health.maxHealth;
        healthText.text = health.HealthOutOfMax();
    }

    // Update is called once per frame
    void Update()
    {
        if (health.CurrentHealth != lastHealth)
        {
            healthSlider.value = health.CurrentHealth;
            healthText.text = health.HealthOutOfMax();
            lastHealth = health.CurrentHealth;
        }
    }

    private void LateUpdate()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - camera.position);
    }
}
