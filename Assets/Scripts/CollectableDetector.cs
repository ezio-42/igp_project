using System;
using UnityEngine;

public class CollectableDetector : MonoBehaviour {
    [SerializeField] private CubesStack stack;
    [SerializeField] private LayerMask multiplicatorMask;
    [SerializeField] private LayerMask collectableMask;
    [SerializeField] private LayerMask lavaMask;
    private DateTime _lastLavaTouchTime;
    private DateTime _lastMultiplicatorTouchTime;
    public int lastMultiplicatorValue;

    private void OnTriggerEnter(Collider other) {
        // if (other.gameObject.TryGetComponent(out Collectable _)) {
        if (Helpers.HasMask(other.gameObject, collectableMask)) {
            other.gameObject.SetActive(false);
            stack.Push();
        } else if (Helpers.HasMask(other.gameObject, lavaMask)) {
            Lava();
        } else if (Helpers.HasMask(other.gameObject, multiplicatorMask)) {
            var now = DateTime.Now;
            lastMultiplicatorValue = other.gameObject.GetComponent<MultiplicatorValue>().value;
            if ((now - _lastMultiplicatorTouchTime).TotalMilliseconds > 200) {
                stack.Pop();
                _lastMultiplicatorTouchTime = now;
            }
        }
    }

    private void Lava() {
        var now = DateTime.Now;
        if ((now - _lastLavaTouchTime).TotalMilliseconds > 50) {
            stack.Pop();
            _lastLavaTouchTime = now;
        }
    }

    private void OnCollisionStay(Collision other) {
        if (Helpers.HasMask(other.gameObject, lavaMask)) {
            Lava();
        }
    }
}