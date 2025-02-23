using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorFollow : MonoBehaviour
{
    public Camera mainCamera; // Assign the main camera in the Inspector
    public float moveSpeed = 10f; // Speed of the ball movement
    public LayerMask interactionLayer; // Layer mask for valid objects

    void Update()
    {
        // Raycast from mouse position into the scene, considering only the specified layer
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, interactionLayer))
        {
            // Move the ball towards the hit point
            Vector3 targetPosition = hit.point;
            targetPosition.y = transform.position.y; // Keep the ball at the same height
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }
}
