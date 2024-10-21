using UnityEngine;

public class DisappearEffect : MonoBehaviour, IInteractionEffect
{
    public void ExecuteEffect(GameObject gameObject, Interactable interactable)
    {
        gameObject.SetActive(false);
    }
}
