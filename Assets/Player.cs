using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{

    private bool spacePressed;
    private float horizontalInput;
    private Rigidbody rb;
    private float groundCheckSize = 0.1f;
    private int score;

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private Transform groundCheckTransform = null;
    [SerializeField] private LayerMask groundLayer; // Vanlig mark
    [SerializeField] private LayerMask bounceLayer; // Bouncy
    [SerializeField] private float bounceForce; // How much bounce - Set in editor
    // Start is called before the first frame update
    void Start()
    {
        groundCheckTransform = transform.Find("GroundCheckTransform");
        Debug.Log("Start method called.");
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            spacePressed = true;
            Debug.Log("Space key was pressed.");
        }


        //Denna kör vi varje frame
        horizontalInput = Input.GetAxis("Horizontal");

    }
    void FixedUpdate()
{
    bool isGrounded = Physics.OverlapSphere(groundCheckTransform.position, groundCheckSize, groundLayer).Length > 0;
    bool isOnBouncy = Physics.OverlapSphere(groundCheckTransform.position, groundCheckSize, bounceLayer).Length > 0;
    
    // Tillåt bara hopp om vi är på marken
    // Om vi är på bouncy - Studsa.
    if (isOnBouncy)
    {
    rb.velocity = new Vector3(rb.velocity.x, 0, 0); // Clear vertical velocity
    rb.AddForce(Vector3.up * bounceForce, ForceMode.VelocityChange);
    }
    else if (spacePressed && isGrounded)
    {
        rb.AddForce(new Vector3(0, 5f, 0), ForceMode.VelocityChange);
        spacePressed = false;
    }
    // If we're in the air and space was pressed, clear the flag
    else if (!isGrounded && spacePressed)
    {
        spacePressed = false;
    }

    //Apply Horizontal inputs
    rb.velocity = new Vector3(horizontalInput, rb.velocity.y, 0);
}

    private void OnDrawGizmos() {
        if (groundCheckTransform == null) return; 
        // Detta gör vi för att undvika UnassignedReferenceException ifall vi inte länkat en
        // Transform till groundCheckTransform
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundCheckTransform.position, groundCheckSize);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            Destroy(other.gameObject);
            score++;
            UpdateScoreDisplay();
        }
    }

    private void UpdateScoreDisplay()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }
}

