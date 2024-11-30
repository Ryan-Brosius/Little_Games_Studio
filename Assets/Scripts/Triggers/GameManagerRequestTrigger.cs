using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerRequestTrigger : MonoBehaviour
{
    [SerializeField] private int message;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.recieveMessage(message);
            Destroy(this);
        }
    }
}
