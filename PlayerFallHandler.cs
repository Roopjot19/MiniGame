using UnityEngine;
using UnityEngine.UI;
using TMPro; // Import for TextMeshProUGUI
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerFallHandler : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject gameOverPanel;   // Reference to the Game Over Panel
    public Image fadeImage;            // Black fade image
    public TextMeshProUGUI scoreText;   // Reference to the current score TextMeshProUGUI
    public TextMeshProUGUI lastScoreText;// Reference to the score TextMeshProUGUI

    [Header("Game Settings")]
    public Transform spawnPoint;       // Player's spawn point
    public float fallThreshold = 6f;   // Y position threshold for detecting falling
    public float fadeDuration = 1f;    // Duration of the fade-out effect

    [Header("Ball Settings")]
    public GameObject ball;            // Reference to the ball object
    public Vector3 ballSpawnPosition = new Vector3(0.1f, 6.89f, 4.58f); // Ball's respawn position

    //[Header("Spinner Settings")]
    //public GameObject spinner;         // Reference to the spinner object

    private int score = 0;
    private int lastScore = 0;   // Player's score (to reset on restart)
    private bool isFading = false;     // Check if fade is in progress
    private Transform playerTransform; // Reference to the player's transform

    private void Start()
    {
        // Ensure Game Over panel and fade image are hidden initially
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }

        if (fadeImage != null)
        {
            fadeImage.color = new Color(0f, 0f, 0f, 0f); // Fully transparent
        }

        // Cache the player's transform
        playerTransform = transform;

         UpdateLastScoreUI();
        // Initialize score UI
        UpdateScoreUI();
       
    }

    private void Update()
    {
        // Check if the player falls below the threshold
        if (playerTransform.position.y < fallThreshold && !isFading)
        {
            StartCoroutine(HandlePlayerFall());
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the player collides with an object tagged as "Spinner"
        if (collision.gameObject.CompareTag("Spinner"))
        {
            StartCoroutine(HandlePlayerSpinnerCollision());
        }
    }

    private IEnumerator HandlePlayerSpinnerCollision()
    {
        // Fade out the screen
        isFading = true;
        yield return StartCoroutine(FadeOutScreen());

        // Show Game Over panel after fade-out
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }
    }

    private IEnumerator HandlePlayerFall()
    {
        isFading = true;

        // Fade out the screen
        yield return StartCoroutine(FadeOutScreen());

        // Show Game Over Panel after fade-out
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }
    }

    public void RestartGame()
    {
        // Update last score before resetting the score to 0
        lastScore = score;
        UpdateLastScoreUI(); // Update the UI to show the last score

        // Reset the current score to 0
        score = 0;
        UpdateScoreUI(); // Update the UI to reflect the new score

        // Reload the current scene to reset everything
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    public void ExitGame()
    {
        // Quit the application
        Application.Quit();
    }

    private void RespawnPlayer()
    {
        if (spawnPoint != null)
        {
            playerTransform.position = spawnPoint.position;
            playerTransform.rotation = spawnPoint.rotation;

            Rigidbody rb = playerTransform.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }
        }
    }

    private void RespawnBall()
    {
        if (ball != null)
        {
            ball.transform.position = ballSpawnPosition; // Set ball to the spawn position
        }
    }

    private void UpdateScoreUI()
    {
        // Update the score text in the UI
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }

    private void UpdateLastScoreUI()
    {
        // Update the last score text in the UI
        if (lastScoreText != null)
        {
            lastScoreText.text = lastScore.ToString();
        }
    }

    private IEnumerator FadeOutScreen()
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
            fadeImage.color = new Color(0f, 0f, 0f, alpha); // Black fade-in
            yield return null;
        }

        fadeImage.color = new Color(0f, 0f, 0f, 1f); // Fully opaque at the end
    }

    private IEnumerator FadeInScreen()
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(1f - (elapsedTime / fadeDuration));
            fadeImage.color = new Color(0f, 0f, 0f, alpha);
            yield return null;
        }

        fadeImage.color = new Color(0f, 0f, 0f, 0f);
        isFading = false;
    }
}
