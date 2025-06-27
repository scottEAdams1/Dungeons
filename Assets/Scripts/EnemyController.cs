using UnityEngine;
using UnityEngine.Windows;

public class EnemyController : MonoBehaviour
{
    /// <summary>
    /// The radius in which the Enemy can detect the Player
    /// </summary>
    public float detectionRadius;

    /// <summary>
    /// The speed at which the Enemy can move
    /// </summary>
    public float moveSpeed;

    /// <summary>
    /// The Enemy's rigidbody
    /// </summary>
    Rigidbody rb;

    /// <summary>
    /// Whether the Enemy is currently moving, default false
    /// </summary>
    bool moving = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Get components from Enemy for private variables
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Detects any colliders within the Enemy's sphere collider (Area of detection)
        Collider[] hits = Physics.OverlapSphere(transform.position, detectionRadius);
        Transform playerTransfrom = null;

        // Checks to see if the Player is within range
        foreach (Collider hit in hits)
        {
            if (hit.CompareTag("Player"))
            {
                playerTransfrom = hit.transform;
                break;
                
            }
        }

        // If the Player is within range, head towards the Player
        if (playerTransfrom != null)
        {
            // Get Player position and direction relative to enemy
            Vector3 playerPosition = playerTransfrom.position;
            Vector3 directionToPlayer = (playerPosition - transform.position).normalized;
            directionToPlayer.y = 0;

            // Get position and rotation relative to world
            Vector3 targetPosition = rb.position + directionToPlayer * moveSpeed * Time.deltaTime;
            Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer);

            // Move in right direction to right position
            rb.MovePosition(targetPosition);
            rb.MoveRotation(lookRotation);
            moving = true;
        }
        else
        {
            moving = false;
        }
    }

    /// <summary>
    /// Checks whether the Enemy is moving
    /// </summary>
    /// <returns>True if Enemy is moving, false otherwise</returns>
    public bool IsMoving()
    {
        return moving;
    }

    /// <summary>
    /// Draws sphere framework around Enemy of detection radius
    /// </summary>
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
