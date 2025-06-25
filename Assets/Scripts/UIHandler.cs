using UnityEngine;
using UnityEngine.UIElements;

public class UIHandler : MonoBehaviour
{
    /// <summary>
    /// Script containing Player's health
    /// </summary>
    public Health playerHealth;

    /// <summary>
    /// The Player's health bar
    /// </summary>
    private VisualElement m_healthBarFill;

    /// <summary>
    /// The text for the amount of health the Player has left
    /// </summary>
    private Label m_healthBarAmount;

    public static UIHandler instance {  get; private set; }

    private void Awake()
    {
        instance = this;
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UIDocument document = GetComponent<UIDocument>();
        m_healthBarFill = document.rootVisualElement.Q<VisualElement>("HealthBarFill");
        m_healthBarAmount = document.rootVisualElement.Q<Label>("HealthAmount");
        SetHealthValue(1.0f);
    }

    public void SetHealthValue(float percentage)
    {
        m_healthBarFill.style.width = Length.Percent(100 * percentage);
        m_healthBarAmount.text = playerHealth.HealthOutOfMax();
    }
}
