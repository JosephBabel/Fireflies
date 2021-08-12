/* Author: Joseph Babel
*  Description: Manages food behavior for spawning and destruction
*  food increases in size and light intensity on spawn and decreases
*  before destruction.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class FoodManager : MonoBehaviour
{
    [SerializeField] private GameObject food;

    [SerializeField] private float maxSpawnDistance = 7.0f;
    [SerializeField] private float spawnTimer = 1.5f; // seconds
    [SerializeField] private float reduceTimerFactor = 0.9f;
    [SerializeField] private float timeToDestroyFood = 5.0f; // seconds

    private float initialLightOuterRadius;
    private float initialLightIntensity;

    void Start()
    {
        StartCoroutine(SpawnFood());
    }

    private IEnumerator SpawnFood()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnTimer);
            GameObject foodInstance = Instantiate(food, Random.insideUnitCircle * maxSpawnDistance, Quaternion.identity); // spawn random location along edge of circle
            initialLightOuterRadius = foodInstance.GetComponent<Light2D>().pointLightOuterRadius; 
            initialLightIntensity = foodInstance.GetComponent<Light2D>().intensity;
            foodInstance.GetComponent<Light2D>().pointLightOuterRadius = 0;
            foodInstance.GetComponent<Light2D>().intensity = 0;
            StartCoroutine(DestroyFood(foodInstance));
        }
    }

    private IEnumerator DestroyFood(GameObject foodInstance)
    {
        // Grow light to be visible for period of time
        float time = 0.0f;
        float duration = 0.5f;
        while (foodInstance && time < duration)
        {
            foodInstance.GetComponent<Light2D>().pointLightOuterRadius = Mathf.Lerp(0, initialLightOuterRadius, time / duration);
            foodInstance.GetComponent<Light2D>().intensity = Mathf.Lerp(0, initialLightIntensity, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        yield return new WaitForSeconds(timeToDestroyFood);

        // Shrink light to be invisible
        time = 0.0f;
        while (foodInstance && time < duration)
        {
            foodInstance.GetComponent<Light2D>().pointLightOuterRadius = Mathf.Lerp(initialLightOuterRadius, 0, time / duration);
            foodInstance.GetComponent<Light2D>().intensity = Mathf.Lerp(initialLightIntensity, 0, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        Destroy(foodInstance);
    }

    public void ReduceSpawnTimer()
    {
        spawnTimer *= reduceTimerFactor;
    }
}
