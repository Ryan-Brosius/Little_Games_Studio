using UnityEngine;

public class SpawnParticleEffect : MonoBehaviour, IInteractionEffect
{
    [SerializeField] private GameObject particlePrefab;
    [SerializeField] private Vector3 offset = new Vector3(0f, 0f, 0f);

    public void ExecuteEffect(GameObject gameObject, Interactable interactable)
    {
        Instantiate(particlePrefab, transform.position + offset, particlePrefab.transform.rotation);
    }
}
