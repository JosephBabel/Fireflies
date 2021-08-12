/* Author: Joseph Babel
*  Description: Follow behavior for player controlled firefly
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    private Rigidbody2D rb;

    private Vector3 mousePos;
    private float followForce = 100.0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        rb.AddForce((mousePos - transform.position) * followForce); // add force towards mouse position
        float maxFollowVelocity = (Vector2.Distance(transform.position, mousePos) * 6) + 5;
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxFollowVelocity); // limit speed of follow
    }
}
