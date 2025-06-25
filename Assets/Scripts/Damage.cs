using UnityEditor.UIElements;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public string targetTag;
    public int damage;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(targetTag))
        {
            return;
        }
        Health health = other.GetComponent<Health>();
        if (health != null)
        {
            health.ChangeHealth(-1 * damage);
        }
    }
}
