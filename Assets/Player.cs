using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{

    private MovementController movement;
    private int score;
    [SerializeField] private TextMeshProUGUI scoreText;
    void Start()
    {
        movement = GetComponent<MovementController>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float depthInput = Input.GetAxis("Vertical");
        movement.Move(horizontalInput, depthInput);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            movement.RequestJump();
        }
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

