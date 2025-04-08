using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public class Movement : MonoBehaviour
{
    private Rigidbody rb;
    private bool jumpRequested;
    private FloatField targetHorizontalSpeed;
    [Header("Groud Detection")]
    [SerializeField] private Transform groundCheckTransform;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform bounceLayer;
    [SerializeField] private float groundCheckSize = 0.1f;
    [Header("Movement Parameters")]
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float bounceForce = 10f;
    [SerializeField] private float moveSpeed = 1f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
