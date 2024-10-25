using UnityEngine;

public class SpawnParticleEffect : MonoBehaviour, IInteractionEffect
{
    [SerializeField] private GameObject particlePrefab;

    public void ExecuteEffect(GameObject gameObject, Interactable interactable)
    {
        Instantiate(particlePrefab, transform.position, particlePrefab.transform.rotation);
    }
}
