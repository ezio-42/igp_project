using System;
using UnityEngine;

public class Player : MonoBehaviour {
    public GameState gameState = GameState.Started;

    [SerializeField] private int startCubesCount;
    [SerializeField] private CubesStack stack;
    [SerializeField] private PathFollower pathFollower;
    [SerializeField] private CollectableDetector detector;
    [SerializeField] private LevelManager levelManager;

    private void Start() {
        levelManager.LoadLevel();

        for (var i = 0; i < startCubesCount; i++) stack.Push();
    }

    private void OnEnable() {
        stack.Changed += OnChange;
        pathFollower.PathCompleted += LevelComplete;
    }

    private void OnDisable() {
        stack.Changed -= OnChange;
        pathFollower.PathCompleted -= LevelComplete;
    }

    public event Action Died;

    private void LevelComplete() {
        gameState = GameState.LevelEnded;
        Died?.Invoke();
    }

    private void OnChange(int count) {
        if (count <= 0) Die();
    }

    private void Die() {
        gameState = GameState.PlayerDied;
        Died?.Invoke();

        if (detector.lastMultiplicatorValue == 0)
            levelManager.ReloadLevel();
        else
            levelManager.LoadNextLevel(detector.lastMultiplicatorValue);
    }
}