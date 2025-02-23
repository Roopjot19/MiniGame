using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public int score = 0; // The player's score
    public TextMeshProUGUI scoreText; // Reference to the TextMeshPro text object

    void Start()
    {
        // Initialize the score display at the start
        UpdateScoreDisplay();
    }

    // This method is triggered when the player collides with another object
    private void OnCollisionEnter(Collision collision)
    {
        // Check if the object the player collided with is a cube
        Cube cube = collision.gameObject.GetComponent<Cube>();

        if (cube != null)
        {
            // Update the score based on the cube's operation and number
            switch (cube.operation)
            {
                case "+":
                    score += cube.number;
                    break;
                case "-":
                    score -= cube.number;
                    break;
                case "*":
                    score *= cube.number;
                    break;
                case "/":
                    if (cube.number != 0) // Ensure division by zero doesn't happen
                    {
                        score /= cube.number;
                    }
                    break;
                default:
                    Debug.LogWarning("Unknown operation on cube");
                    break;
            }

            // Optionally, destroy the cube after collision
            Destroy(collision.gameObject);

            // Update the score display
            UpdateScoreDisplay();
        }
    }

    // Method to update the TextMeshPro text with the current score
    private void UpdateScoreDisplay()
    {
        scoreText.text = score.ToString();
    }
}
