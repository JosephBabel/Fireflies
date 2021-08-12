/* Author: Joseph Babel
*  Description: Flickers the 2D light component of an object to a specified 
*  pattern of a number of short flickers followed by a pause
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Flicker : MonoBehaviour
{
    [SerializeField] private float timeBetweenShortFlickers = 0.08f; // seconds between short flickers
    [SerializeField] private float timeBetweenlongFlickers = 1.5f; // seconds to pause after short flickers
    [SerializeField] private int timesToFlicker = 20; // times to flicker

    private float lowLightIntensity;
    private float highLightIntensity;

    private void Awake()
    {
        float lastIntensity = GetComponent<Light2D>().intensity;
        highLightIntensity = lastIntensity;
        lowLightIntensity = lastIntensity / 3;
        StartCoroutine(FlickerObject());
    }

    private IEnumerator FlickerObject()
    {
        while (true)
        {
            for (int i = 0; i < timesToFlicker; i++)
            {
                float lastIntensity = GetComponent<Light2D>().intensity;
                if (lastIntensity == highLightIntensity)
                {
                    GetComponent<Light2D>().intensity = lowLightIntensity; // switch to low light
                }
                else
                {
                    GetComponent<Light2D>().intensity = highLightIntensity; // switch to high light
                }

                yield return new WaitForSeconds(timeBetweenShortFlickers);
            }

            yield return new WaitForSeconds(timeBetweenlongFlickers);
        }
    }
}
