using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float movespeed = 5f;

    // Public so your Footsteps script can see it (fixes that Red Error)
    public Vector3 moveDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Essential for 3D physics stability
        rb.freezeRotation = true;
        rb.useGravity = true;

        // This is the "Anti-Ghosting" setting for small players/big walls
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
    }

    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        // Calculate direction based on player's rotation
        moveDirection = (transform.forward * z) + (transform.right * x);

        // Fix the diagonal speed boost
        if (moveDirection.magnitude > 1f)
        {
            moveDirection.Normalize();
        }
    }

    void FixedUpdate()
    {
        // Move by setting velocity. This is the most "solid" way to hit walls.
        Vector3 targetVelocity = moveDirection * movespeed;

        // We keep the existing Y velocity so gravity still works!
        rb.linearVelocity = new Vector3(targetVelocity.x, rb.linearVelocity.y, targetVelocity.z);
    }
}