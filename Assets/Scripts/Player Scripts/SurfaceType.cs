using UnityEngine;

public class SurfaceType : MonoBehaviour
{
    public enum Surface
    {
        Default,
        Grass,
        Wood,
    }

    [SerializeField] public Surface surfaceType;
}
