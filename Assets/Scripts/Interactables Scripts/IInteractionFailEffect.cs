using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractionFailEffect
{
    void ExecuteEffect(GameObject gameObject, Interactable interactable);
}
