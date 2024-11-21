using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickerLightsEffect : MonoBehaviour, IInteractionEffect
{
    [SerializeField] private List<Light> lights; // List of lights to flicker
    [SerializeField] private float flickerDelayMin = 0.05f; // Minimum delay between flickers
    [SerializeField] private float flickerDelayMax = 0.2f; // Maximum delay between flickers
    [SerializeField] private float lowIntensity = 0.2f; // Intensity for "flickering off"
    [SerializeField] private float highIntensity = 1f; // Full light intensity
    [SerializeField] private float flickerDuration = 5f; // Duration of the flickering effect

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
            light.intensity = Random.value > 0.5f ? highIntensity : lowIntensity;

            yield return new WaitForSeconds(Random.Range(flickerDelayMin, flickerDelayMax));
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
