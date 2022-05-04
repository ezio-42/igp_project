using UnityEngine;

public class CollectedCube : MonoBehaviour {
    private CubesStack _stack;
    [SerializeField] private LayerMask wallMask;

    public void Init(CubesStack stack, Vector3 localPosition, Quaternion localRotation, Transform parent) {
        _stack = stack;
        var t = transform;
        transform.SetParent(parent);
        transform.localPosition = localPosition;
        transform.localRotation = localRotation;
    }

    private void OnTriggerEnter(Collider other) {
        // if (other.gameObject.TryGetComponent<Wall>(out _)) {
        if (Helpers.HasMask(other.gameObject, wallMask)) {
            _stack.Pop(this);
        }
    }
}