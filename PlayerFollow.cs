using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollow : MonoBehaviour
{
    public Transform ball; // Assign the Ball object in the Inspector
    public float speed = 2f; // Speed of the player
   // public float stopDistance = 1f; // Minimum distance to stop following the ball

    void Update()
    {
        if (ball == null) return;

        // Calculate the distance between the player and the ball
        //float distance = Vector3.Distance(transform.position, ball.position);


        Vector3 direction = (ball.position - transform.position).normalized;

        // Move the player
        transform.position += direction * speed * Time.deltaTime;

        // Smoothly rotate the player to face the ball
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * speed);

        //// Move towards the ball if farther than the stop distance
        //if (distance > stopDistance)
        //{
        //}
    }
}
