using UnityEngine;
using UnityEngine.Windows;

public class EnemyController : MonoBehaviour
{
    public float detectionRadius;

    public float moveSpeed;

    Rigidbody rb;

    bool moving = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, detectionRadius);
        Transform playerTransfrom = null;
        foreach (Collider hit in hits)
        {
            if (hit.CompareTag("Player"))
            {
                playerTransfrom = hit.transform;
                break;
                
            }
        }
        if (playerTransfrom != null)
        {
            Vector3 playerPosition = playerTransfrom.position;
            Vector3 directionToPlayer = (playerPosition - transform.position).normalized;
            directionToPlayer.y = 0;
            Vector3 targetPosition = rb.position + directionToPlayer * moveSpeed * Time.deltaTime;
            Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer);
            rb.MovePosition(targetPosition);
            rb.MoveRotation(lookRotation);
            moving = true;
        }
        else
        {
            moving = false;
        }
    }

    public bool IsMoving()
    {
        return moving;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
