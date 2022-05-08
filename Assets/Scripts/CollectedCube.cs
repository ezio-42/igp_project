using UnityEngine;

public class CollectedCube : MonoBehaviour {
    [SerializeField] private LayerMask wallMask;
    private CubesStack _stack;

    private void OnTriggerEnter(Collider other) {
        if (Helpers.HasMask(other.gameObject, wallMask)) _stack.Pop(this);
    }

    public void Init(CubesStack stack, Vector3 localPosition, Quaternion localRotation, Transform parent) {
        _stack = stack;
        var t = transform;
        transform.SetParent(parent);
        transform.localPosition = localPosition;
        transform.localRotation = localRotation;
    }
}