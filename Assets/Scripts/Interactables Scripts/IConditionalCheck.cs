using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IConditionalCheck
{
    bool CanExecuteEffect(GameObject gameObject, Interactable interactable);
}

