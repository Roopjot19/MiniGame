using UnityEngine;

public class DynamicHammerSwing : MonoBehaviour
{
    [Header("Swing Settings")]
    public float swingAngle = 45f;    // Maximum swing angle
    public float swingSpeed = 2f;     // Speed of swinging

    private Quaternion initialRotation;

    void Start()
    {
        // Set the initial rotation to be facing downward (0, 90, 0)
        initialRotation = Quaternion.Euler(0f, 90f, 180f); // Hammer faces downward initially
    }

    void Update()
    {
        // Calculate the current swing angle based on time
        float currentAngle = Mathf.Sin(Time.time * swingSpeed) * swingAngle;

        // Apply the swing to the hammer, rotating along the X-axis
        transform.localRotation = initialRotation * Quaternion.Euler(currentAngle, 0f, 0f);
    }
}
