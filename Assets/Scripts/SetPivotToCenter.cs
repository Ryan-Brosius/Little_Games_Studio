using UnityEngine;

public class SetPivotToCenterParent : MonoBehaviour
{
    [ContextMenu("Set Pivot to Center")]
    public void CenterPivot()
    {
        // Calculate the bounds including all child renderers
        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        if (renderers.Length == 0)
        {
            Debug.LogWarning("No renderers found on this object or its children.");
            return;
        }

        Bounds bounds = renderers[0].bounds;
        foreach (Renderer renderer in renderers)
        {
            bounds.Encapsulate(renderer.bounds);
        }

        // Find the center of the bounds
        Vector3 center = bounds.center;

        // Create a new parent object at the center
        GameObject pivotParent = new GameObject(gameObject.name + "_Pivot");
        pivotParent.transform.position = center;

        // Reparent the current object
        transform.SetParent(pivotParent.transform, true);

        Debug.Log("Pivot set to center using parent object.");
    }
}
