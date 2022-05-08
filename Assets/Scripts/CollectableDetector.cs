using UnityEngine;

public class CollectableDetector : MonoBehaviour {
    [SerializeField] private CubesStack stack;
    [SerializeField] private LayerMask multiplicatorMask;
    [SerializeField] private LayerMask collectableMask;
    [SerializeField] private LayerMask lavaMask;
    public int lastMultiplicatorValue;
    public int lavaDestroyTimeInMilliseconds = 50;
    public int multiplicatorDestroyTimeInMilliseconds = 50;
    private float _lastLavaTouchTime;
    private float _lastMultiplicatorTouchTime;

    private void Start() {
        _lastLavaTouchTime = Time.time;
        _lastMultiplicatorTouchTime = Time.time;
    }

    private void OnCollisionStay(Collision other) {
        if (Helpers.HasMask(other.gameObject, lavaMask)) CollideWithLava();
    }

    private void OnTriggerEnter(Collider other) {
        if (Helpers.HasMask(other.gameObject, collectableMask)) {
            other.gameObject.SetActive(false);
            stack.Push();
        }
        else if (Helpers.HasMask(other.gameObject, lavaMask)) {
            CollideWithLava();
        }
        else if (Helpers.HasMask(other.gameObject, multiplicatorMask)) {
            var now = Time.time;
            lastMultiplicatorValue = other.gameObject.GetComponent<MultiplicatorValue>().value;
            if ((now - _lastMultiplicatorTouchTime) * 1000 > multiplicatorDestroyTimeInMilliseconds) {
                stack.Pop();
                _lastMultiplicatorTouchTime = now;
            }
        }
    }

    private void CollideWithLava() {
        var now = Time.time;
        if ((now - _lastLavaTouchTime) * 1000 > lavaDestroyTimeInMilliseconds) {
            stack.Pop();
            _lastLavaTouchTime = now;
        }
    }
}