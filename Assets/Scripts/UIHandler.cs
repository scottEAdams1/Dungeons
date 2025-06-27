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

    /// <summary>
    /// Instance of the UIHandler
    /// </summary>
    public static UIHandler instance {  get; private set; }

    private void Awake()
    {
        // Set instance to the UIHandler
        instance = this;
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Get component from UI document for private variable
        UIDocument document = GetComponent<UIDocument>();

        // Get UI components from UI
        m_healthBarFill = document.rootVisualElement.Q<VisualElement>("HealthBarFill");
        m_healthBarAmount = document.rootVisualElement.Q<Label>("HealthAmount");
        SetHealthValue(1.0f);
    }

    /// <summary>
    /// Set the bar and text in the UI to the Player's current health
    /// </summary>
    /// <param name="percentage">The percentage of health out of max the player has remaining, set between 0 and 1</param>
    public void SetHealthValue(float percentage)
    {
        m_healthBarFill.style.width = Length.Percent(100 * percentage);
        m_healthBarAmount.text = playerHealth.HealthOutOfMax();
    }
}
