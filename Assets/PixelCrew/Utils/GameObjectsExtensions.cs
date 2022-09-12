using UnityEngine;

public static class GameObjectsExtensions
{
    public static bool IsInLayer(this GameObject go, LayerMask layer)
    {
        return layer == (layer | 1 << go.layer);
    }

}
