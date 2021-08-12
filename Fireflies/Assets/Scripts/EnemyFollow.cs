/* Author: Joseph Babel
*  Description: Enemy follow behavior. Gets player position from game manager.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public GameManager gameManager;
    private Rigidbody2D rb;

    [SerializeField] private float speed = 8.0f;
    [SerializeField] private float followDistance = 2.0f;
    [SerializeField] private float maxFollowVelocity = 8.0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce((gameManager.GetPlayerPos() - (Vector2) transform.position).normalized * speed, ForceMode2D.Impulse); // add force towards player
    }

    void FixedUpdate()
    {
        Vector2 playerPos = gameManager.GetPlayerPos();
        if (Vector2.Distance((Vector2) transform.position, playerPos) < followDistance) // add force towards player if close
        {
            rb.AddForce((playerPos - (Vector2) transform.position).normalized * speed);
            rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxFollowVelocity);
        }
    }
}
