using UnityEngine;

public static class Helpers {
    public static bool HasMask(GameObject go, LayerMask mask) {
        return (mask.value & (1 << go.layer)) == 1 << go.layer;
    }
}