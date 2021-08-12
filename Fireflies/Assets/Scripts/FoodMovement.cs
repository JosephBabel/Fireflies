/* Author: Joseph Babel
*  Description: Simple movement for food.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float speed = 1.0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(Random.insideUnitCircle.normalized * speed, ForceMode2D.Impulse); // move random direction at constance speed
    }
}
