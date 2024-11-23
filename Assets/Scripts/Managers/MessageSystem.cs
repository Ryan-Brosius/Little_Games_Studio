using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MessageSystem : MonoBehaviour
{
    public static MessageSystem Instance { get; private set; }
    [SerializeField] private TextMeshProUGUI canvasText;

    [Header("Text Settings")]
    [SerializeField] private float typingSpeed = 0.05f;
    [SerializeField] private float displayDuration = 2.0f;
    [SerializeField] private float fadeOutDuration = 1.0f;

    private Color originalColor;
    private Queue<string> messageQueue = new Queue<string>();
    private HashSet<string> inQueue = new HashSet<string>();
    private bool runningQueue = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        originalColor = canvasText.color;
    }

    public void queueMessage(string message)
    {
        if (!inQueue.Contains(message))
        {
            messageQueue.Enqueue(message);
            inQueue.Add(message);
        }

        if (!runningQueue)
        {
            runQueue();
            runningQueue = true;
        }
    }

    private void runQueue()
    {
        if (messageQueue.Count == 0)
        {
            runningQueue = false;
            return;
        }

        string message = messageQueue.Dequeue();

        StartCoroutine(TypeText(message));
    }

    IEnumerator TypeText(string message)
    {
        canvasText.color = originalColor;
        canvasText.text = "";

        for (int i = 0; i < message.Length; i++)
        {
            canvasText.text += message[i];

            // LayoutRebuilder.ForceRebuildLayoutImmediate(canvasText.rectTransform);

            yield return new WaitForSeconds(typingSpeed);
        }

        yield return new WaitForSeconds(displayDuration);

        inQueue.Remove(message);
        StartCoroutine(FadeOutText());
    }

    IEnumerator FadeOutText()
    {
        float elapsedTime = 0f;
        Color c = canvasText.color;

        while (elapsedTime < fadeOutDuration)
        {
            elapsedTime += Time.deltaTime;
            c.a = Mathf.Lerp(originalColor.a, 0, elapsedTime / fadeOutDuration);
            canvasText.color = c;
            yield return null;
        }

        runQueue();
    }
}
