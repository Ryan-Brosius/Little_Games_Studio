using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickerLightsEffect : MonoBehaviour, IInteractionEffect
{
    [SerializeField] private List<Light> lights;
    [SerializeField] private float flickerDelayOnMin = .9f;
    [SerializeField] private float flickerDelayOnMax = 1.3f;
    [SerializeField] private float flickerDelayOffMin = 0.05f;
    [SerializeField] private float flickerDelayOffMax = 0.2f;
    [SerializeField] private float lowIntensity = 0.2f;
    [SerializeField] private float highIntensity = 1f;
    [SerializeField] private float flickerDuration = 5f;

    private List<Coroutine> flickerCoroutines = new List<Coroutine>();

    public void ExecuteEffect(GameObject gameObject, Interactable interactable)
    {
        foreach (Light light in lights)
        {
            if (light != null)
            {
                Coroutine flickerCoroutine = StartCoroutine(FlickerLight(light));
                flickerCoroutines.Add(flickerCoroutine);
            }
        }

        StartCoroutine(StopFlickeringAfterDuration());
    }

    private IEnumerator FlickerLight(Light light)
    {
        while (true)
        {
            light.intensity = highIntensity;

            yield return new WaitForSeconds(Random.Range(flickerDelayOnMin, flickerDelayOnMax));

            light.intensity = lowIntensity;

            yield return new WaitForSeconds(Random.Range(flickerDelayOffMin, flickerDelayOffMax));
        }
    }

    private IEnumerator StopFlickeringAfterDuration()
    {
        yield return new WaitForSeconds(flickerDuration);

        foreach (Coroutine coroutine in flickerCoroutines)
        {
            if (coroutine != null)
            {
                StopCoroutine(coroutine);
            }
        }
        flickerCoroutines.Clear();

        foreach (Light light in lights)
        {
            if (light != null)
            {
                light.intensity = highIntensity;
            }
        }
    }
}
