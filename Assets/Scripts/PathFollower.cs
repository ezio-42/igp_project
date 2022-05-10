using System;
using BezierSolution;
using UnityEngine;

public class PathFollower : MonoBehaviour {
    [SerializeField] private BezierSpline bezierSpline;
    [SerializeField] private float speed;
    [SerializeField] private float rotationSpeed = 10;
    [SerializeField] private Player player;
    private Transform _cachedTransform;
    private bool _isPathCompleted;

    private float _passedPath;

    private void Start() {
        _cachedTransform = transform;
    }

    private void Update() {
        if (player.gameState != GameState.Started) return;
        if (_isPathCompleted) return;

        SetNextPosition();
        SetNextRotation();

        if (_passedPath > 1) {
            PathCompleted?.Invoke();
            _isPathCompleted = true;
        }
    }

    private void OnEnable() {
        player.Died += Stop;
    }

    private void OnDisable() {
        player.Died -= Stop;
    }

    public event Action PathCompleted;

    private void Stop() {
        speed = 0;
    }

    private void SetNextPosition() {
        _cachedTransform.position = bezierSpline.MoveAlongSpline(ref _passedPath, speed * Time.deltaTime);
        ;
    }

    private void SetNextRotation() {
        var segment = bezierSpline.GetSegmentAt(_passedPath);
        var targetRotation = Quaternion.LookRotation(segment.GetTangent(), segment.GetNormal());
        _cachedTransform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}