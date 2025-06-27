using UnityEngine;

public class SlimeMovement : MonoBehaviour
{
    /// <summary>
    /// The Slime's rigidbody
    /// </summary>
    Rigidbody rb;

    /// <summary>
    /// The amount the Slime can jump
    /// </summary>
    public float jumpForce;

    /// <summary>
    /// The Slime's EnemyController script, basic movement for all enemies
    /// </summary>
    EnemyController enemyController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Get components from Slime for private variables
        rb = GetComponent<Rigidbody>();
        enemyController = GetComponent<EnemyController>();
    }

    // Update is called once per frame
    void Update()
    {
        // If the Slime is moving and on the ground, jump
        if (enemyController.IsMoving() && CheckGrounded())
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    /// <summary>
    /// Check the slime is on the ground
    /// </summary>
    /// <returns></returns>
    bool CheckGrounded()
    {
        Collider col = GetComponent<Collider>();
        Vector3 origin = col.bounds.center - new Vector3(0.0f, col.bounds.extents.y, 0.0f);
        return Physics.Raycast(origin, Vector3.down, 0.2f);
    }
}
