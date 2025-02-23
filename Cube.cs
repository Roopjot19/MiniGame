using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Make sure to include this for TextMeshPro

public class Cube : MonoBehaviour
{
    public int number; // The random number on the cube
    public string operation; // The operation on the cube: "+", "-", "*", "/"

    // Possible operations
    private string[] operations = { "+", "-", "*", "/" };

    // Dictionary to hold the min and max ranges for each operation
    private Dictionary<string, (int min, int max)> operationRanges = new Dictionary<string, (int, int)>()
    {
        { "+", (1, 10) }, // Range for addition
        { "-", (1, 5) },  // Range for subtraction
        { "*", (2, 4) },  // Range for multiplication
        { "/", (1, 3) }   // Range for division
    };

    // Reference to the TextMeshPro component
    public TextMeshPro textMeshPro;

    void Start()
    {
        // Assign a random operation from the operations array
        operation = operations[Random.Range(0, operations.Length)];

        // Get the specific range for the selected operation
        (int min, int max) range = operationRanges[operation];

        // Assign a random number based on the operation's specific range
        number = Random.Range(range.min, range.max + 1);

        // Update the TextMeshPro component with the number and operation
        textMeshPro = GetComponentInChildren<TextMeshPro>();
        if (textMeshPro != null)
        {
            textMeshPro.text = $"{operation} {number}";
        }
        else
        {
            Debug.LogWarning("TextMeshPro component not found on the cube.");
        }

        // Debug to see the assigned number and operation on the cube
        Debug.Log($"Cube {gameObject.name}: Operation {operation}, Number {number} (Range: {range.min}-{range.max})");
    }

    // Display the number and operation in the scene view for better visualization
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, Vector3.one);

        // Ensure that Handles.Label only runs in the Unity Editor
#if UNITY_EDITOR
        UnityEditor.Handles.Label(transform.position, $"{operation} {number}");
#endif
    }
}
