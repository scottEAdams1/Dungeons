using UnityEngine;

public class SlimeMovement : MonoBehaviour
{
    Rigidbody rb;
    public float jumpForce;
    EnemyController enemyController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        enemyController = GetComponent<EnemyController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyController.IsMoving() && CheckGrounded())
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    bool CheckGrounded()
    {
        Collider col = GetComponent<Collider>();
        Vector3 origin = col.bounds.center - new Vector3(0.0f, col.bounds.extents.y, 0.0f);
        return Physics.Raycast(origin, Vector3.down, 0.2f);
    }
}
