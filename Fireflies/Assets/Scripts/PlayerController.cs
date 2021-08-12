/* Author: Joseph Babel
*  Description: Collision events between the player and food or enemies.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameManager gameManager;
    public AudioManager audioManager;
    public ParticleSystem ps;

    void Awake()
    {
        ps.Stop();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Food")
        {
            ps.Emit(1); // activate particle system
            Destroy(collision.gameObject); // destroy food

            if (gameManager.IsGameRunning())
            {
                gameManager.AddScore(); // add score
                audioManager.playEatAudio(transform.position); // play eat audio
            }
        }
        else if (collision.gameObject.tag == "Enemy")
        {
            Destroy(collision.gameObject); // destroy enemy

            if (gameManager.IsGameRunning())
            {
                gameManager.decreaseLife(); // decrease life
                audioManager.playHitAudio(transform.position); // play hit audio
            }
        }
    }
}
