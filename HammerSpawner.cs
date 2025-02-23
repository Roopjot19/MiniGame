using UnityEngine;

public class HammerSpawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    public GameObject hammerPrefab;   // The hammer prefab to spawn
    public int numberOfHammers = 5;   // Total number of hammers to spawn
    public float minZPosition = -50f; // Minimum Z position for random spawning
    public float maxZPosition = 10f;  // Maximum Z position for random spawning
    public float spawnX = 0f;         // Fixed X position for all hammers
    public float spawnY = 5f;         // Fixed Y position for all hammers
    public float minDistance = 15f;   // Minimum distance between spawned hammers

    private void Start()
    {
        SpawnHammers();
    }

    void SpawnHammers()
    {
        // Keep track of Z positions to ensure minimum distance
        float[] zPositions = new float[numberOfHammers];

        for (int i = 0; i < numberOfHammers; i++)
        {
            float randomZ;

            // Ensure hammers spawn at least `minDistance` apart
            do
            {
                randomZ = Random.Range(minZPosition, maxZPosition);
            } while (!IsPositionValid(randomZ, zPositions, i));

            // Save the valid Z position
            zPositions[i] = randomZ;

            // Spawn the hammer at the calculated position
            Vector3 spawnPosition = new Vector3(spawnX, spawnY, randomZ);

            // Instantiate with the hammer rotation facing down and swinging along the X-axis
            Quaternion hammerRotation = Quaternion.Euler(90f, 0f, 0f); // Rotate 90 degrees along X-axis to face downward
            GameObject hammer = Instantiate(hammerPrefab, spawnPosition, hammerRotation);

            // Apply the swinging behavior via the script
            hammer.GetComponent<DynamicHammerSwing>().swingAngle = 45f;  // Set the swing angle as needed
            hammer.GetComponent<DynamicHammerSwing>().swingSpeed = 2f;  // Set the swing speed as needed
        }
    }

    // Check if the random Z position is at least `minDistance` away from existing hammers
    bool IsPositionValid(float randomZ, float[] zPositions, int currentIndex)
    {
        for (int i = 0; i < currentIndex; i++)
        {
            if (Mathf.Abs(zPositions[i] - randomZ) < minDistance)
            {
                return false; // Position is too close to an existing hammer
            }
        }
        return true;
    }
}
