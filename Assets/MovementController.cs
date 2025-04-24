using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public class MovementController : MonoBehaviour
{
    private Rigidbody rb;
    private bool jumpRequested;
    private float targetHorizontalSpeed;
    private float targetDepthSpeed;

    [Header("Groud Detection")]
    [SerializeField] private Transform groundCheckTransform;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask bounceLayer;
    [SerializeField] private float groundCheckSize = 0.1f;
    [Header("Movement Parameters")]
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float bounceForce = 10f;
    [SerializeField] private float moveSpeed = 1f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (groundCheckTransform == null)
        {
            groundCheckTransform = transform.Find("GroundCheckTransform");
        }
    }

    public void Move(float horizontalInput, float depthInput)
    {
        targetHorizontalSpeed = horizontalInput * moveSpeed;
        targetDepthSpeed = depthInput * moveSpeed;
    }

    public void RequestJump()
    {
        jumpRequested = true;
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector3(targetHorizontalSpeed, rb.linearVelocity.y, targetDepthSpeed);
        bool isGrounded = Physics.OverlapSphere(groundCheckTransform.position, groundCheckSize, groundLayer).Length > 0;
        bool isOnBouncy = Physics.OverlapSphere(groundCheckTransform.position, groundCheckSize, bounceLayer).Length > 0;

        // Tillåt bara hopp om vi är på marken
        // Om vi är på bouncy - Studsa.
        if (isOnBouncy)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z); // Clear vertical velocity
            rb.AddForce(Vector3.up * bounceForce, ForceMode.VelocityChange);
        }
        else if (jumpRequested && isGrounded)
        {
            rb.AddForce(new Vector3(0, 5f, 0), ForceMode.VelocityChange);
            jumpRequested = false;
        }
        // If we're in the air and space was pressed, clear the flag
        else if (!isGrounded && jumpRequested)
        {
            jumpRequested = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmos() {
        if (groundCheckTransform == null) return; 
        // Detta gör vi för att undvika UnassignedReferenceException ifall vi inte länkat en
        // Transform till groundCheckTransform
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundCheckTransform.position, groundCheckSize);
    }
}
