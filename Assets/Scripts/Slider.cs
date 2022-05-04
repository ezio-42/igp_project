using UnityEngine;


public class Slider : MonoBehaviour {
    [SerializeField] private float screenPart = 0.3f;

    private float _savedTouchPosition;
    private float _center;
    private readonly float _screenWidth = Screen.width;

    private float _horizontalPosition;

    private float GetScreenPoint() {
        return (_screenWidth / 2 * screenPart);
    }

    public float GetHorizontalPosition() {
        return _horizontalPosition;
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            _center = Input.mousePosition.x - _savedTouchPosition * GetScreenPoint();
        }

        if (Input.GetMouseButton(0)) {
            var distanceNormalized = (Input.mousePosition.x - _center) / GetScreenPoint();

            if (distanceNormalized > 1) {
                _center += (distanceNormalized - 1) * GetScreenPoint();
                distanceNormalized = 1;
            }
            else if (distanceNormalized < -1) {
                _center += (distanceNormalized + 1) * GetScreenPoint();
                distanceNormalized = -1;
            }

            _horizontalPosition = distanceNormalized;
        }

        if (Input.GetMouseButton(0)) {
            _savedTouchPosition = _horizontalPosition;
        }
    }
}