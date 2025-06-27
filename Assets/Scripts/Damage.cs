using UnityEditor.UIElements;
using UnityEngine;

public class Damage : MonoBehaviour
{
    /// <summary>
    /// Tag of object to damage
    /// </summary>
    public string targetTag;

    /// <summary>
    /// Amount of damage to inflict
    /// </summary>
    public int damage;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Cause damage to intended target if there is a collision
    /// </summary>
    /// <param name="other">The object colliding with, hopefully the targetTag</param>
    private void OnTriggerEnter(Collider other)
    {
        // If other isn't the target, do nothing
        if (!other.CompareTag(targetTag))
        {
            return;
        }

        // If it is the target, get its Health script and do intended damage
        Health health = other.GetComponent<Health>();
        if (health != null)
        {
            health.ChangeHealth(-1 * damage);
        }
    }
}
