/* Author: Joseph Babel
*  Description: Manages enemy spawn and destruction. Enemies will 
*  decrease in size and light intensity before they are destroyed.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class EnemyManager : MonoBehaviour
{
    private GameManager gameManager;
    [SerializeField] private GameObject enemy;

    [SerializeField] private float spawnDistance = 10.0f;
    [SerializeField] private float spawnTimer = 1.5f;
    [SerializeField] private float reduceTimerFactor = 0.9f;
    [SerializeField] private float timeToDestroyEnemy = 3.0f;

    private float initialLightOuterRadius;
    private float initialLightIntensity;

    private void Awake()
    {
        gameManager = gameObject.GetComponent<GameManager>();
    }

    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    private IEnumerator SpawnEnemy()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnTimer);
            GameObject enemyInstance = Instantiate(enemy, Random.insideUnitCircle.normalized * spawnDistance, Quaternion.identity);
            initialLightOuterRadius = enemyInstance.GetComponent<Light2D>().pointLightOuterRadius;
            initialLightIntensity = enemyInstance.GetComponent<Light2D>().intensity;
            enemyInstance.GetComponent<EnemyFollow>().gameManager = gameManager;
            StartCoroutine(DestroyEnemy(enemyInstance));
        }
    }

    private IEnumerator DestroyEnemy(GameObject enemyInstance)
    {
        yield return new WaitForSeconds(timeToDestroyEnemy);

        // Shrink light to be invisible
        float time = 0.0f;
        float duration = 0.5f;
        while (enemyInstance && time < duration)
        {
            enemyInstance.GetComponent<Light2D>().pointLightOuterRadius = Mathf.Lerp(initialLightOuterRadius, 0, time / duration);
            enemyInstance.GetComponent<Light2D>().intensity = Mathf.Lerp(initialLightIntensity, 0, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        Destroy(enemyInstance);
    }

    public void ReduceSpawnTimer()
    {
        spawnTimer *= reduceTimerFactor;
    }
}
