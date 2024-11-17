using UnityEngine;

public class ItemDropInteractable : MonoBehaviour, IInteractionEffect
{
    [Header("Furniture Drop Settings")]
    [SerializeField] public GameObject drop;
    [SerializeField] public int dropAmount = 1;
    [SerializeField] private Vector3 offset = new Vector3(0f, 0f, 0f);

    [Header("Randomness Settings")]
    [SerializeField] public Vector3 positionRandomness = new Vector3(0.5f, 0.5f, 0.5f);
    [SerializeField] public Vector3 rotationRandomness = new Vector3(15f, 15f, 15f);

    public void ExecuteEffect(GameObject gameObject, Interactable interactable)
    {
        for (int i = 0; i < dropAmount; i++)
        {
            Vector3 randomPositionOffset = new Vector3(
                Random.Range(-positionRandomness.x, positionRandomness.x),
                Random.Range(-positionRandomness.y, positionRandomness.y),
                Random.Range(-positionRandomness.z, positionRandomness.z)
            );

            Vector3 spawnPosition = gameObject.transform.position + randomPositionOffset;

            Quaternion randomRotation = Quaternion.Euler(
                Random.Range(-rotationRandomness.x, rotationRandomness.x),
                Random.Range(-rotationRandomness.y, rotationRandomness.y),
                Random.Range(-rotationRandomness.z, rotationRandomness.z)
            );

            Instantiate(drop, spawnPosition + offset, randomRotation);
        }
    }
}
