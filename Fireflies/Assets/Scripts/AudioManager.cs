/* Author: Joseph Babel
*  Description: Manages audio for sound effects, looping music, and ambient sound.
*  Sound effects are spatialized.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static GameObject instance;
    [SerializeField] private AudioClip backgroundAmbience;
    [SerializeField] private AudioClip eatAudio;
    [SerializeField] private AudioClip hitAudio;
    [SerializeField] private AudioClip gameOverAudio;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (instance == null)
        {
            instance = gameObject;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void playEatAudio(Vector3 position) {
        AudioSource.PlayClipAtPoint(eatAudio, 0.9f * Camera.main.transform.position + 0.1f * position, 0.6f);
    }

    public void playHitAudio(Vector3 position)
    {
        AudioSource.PlayClipAtPoint(hitAudio, 0.9f * Camera.main.transform.position + 0.1f * position, 0.4f);
    }

    public void playGameOverAudio()
    {
        instance.GetComponent<AudioSource>().PlayOneShot(gameOverAudio);
    }
}
