using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {
    public event Action Died;
    public GameState gameState = GameState.Started;

    [SerializeField] private int startCubesCount;
    [SerializeField] private CubesStack stack;
    [SerializeField] private PathFollower pathFollower;
    [SerializeField] private Store store;
    [SerializeField] private CollectableDetector detector;
    [SerializeField] private LevelInfo levelInfo;

    private void Start() {
        var currentLevelIndex = store.GetCurrentLevelIndex();

        if (currentLevelIndex != levelInfo.index) {
            SceneManager.LoadScene("Level" + currentLevelIndex);
        }


        for (var i = 0; i < startCubesCount; i++) {
            stack.Push();
        }
    }

    private void OnEnable() {
        stack.Changed += OnChange;
        pathFollower.PathCompleted += LevelComplete;
    }

    private void OnDisable() {
        stack.Changed -= OnChange;
        pathFollower.PathCompleted -= LevelComplete;
    }

    private void LevelComplete() {
        gameState = GameState.LevelEnded;
        Died?.Invoke();
    }

    private void OnChange(int count) {
        if (count <= 0) {
            Die();
        }
    }

    private void Die() {
        gameState = GameState.PlayerDied;
        Died?.Invoke();

        if (detector.lastMultiplicatorValue == 0) {
            SceneManager.LoadScene("Level" + store.GetCurrentLevelIndex());
        }
        else {
            store.SetLastMultiplicator(detector.lastMultiplicatorValue);
            store.LevelPassed();
            SceneManager.LoadScene("Next");
        }
    }
}