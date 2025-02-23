using UnityEngine;

public class SpinnerSpawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    public GameObject spinnerPrefab;   // The blade spinner prefab to spawn
    public int numberOfSpinners = 5;   // Total number of spinners to spawn
    public float spawnSpacing = 5f;    // Distance between each spinner along the Z-axis
    public Vector3 spawnPosition = new Vector3(0f, 0f, -26f); // Initial spawn position

    private Quaternion spinnerRotation = Quaternion.Euler(90f, 0f, 0f); // Correct initial rotation

    void Start()
    {
        SpawnSpinners();
    }

    void SpawnSpinners()
    {
        for (int i = 0; i < numberOfSpinners; i++)
        {
            // Calculate the spawn position for each spinner
            Vector3 position = spawnPosition + new Vector3(0f, 0f, i * -spawnSpacing);

            // Instantiate with the defined initial rotation
            Instantiate(spinnerPrefab, position, spinnerRotation);
        }
    }
}
