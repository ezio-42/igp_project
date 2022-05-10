using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {
    [SerializeField] private int currentLevelIndex;
    [SerializeField] private Store store;

    private void LoadLevel(string levelName) {
        SceneManager.LoadScene(levelName);
    }

    public void ReloadLevel() {
        LoadLevel(store.GetCurrentLevelIndex());
    }

    public void LoadLevel() {
        var currentLevelIndexFromStore = store.GetCurrentLevelIndex();

        if (currentLevelIndexFromStore != currentLevelIndex) LoadLevel(currentLevelIndexFromStore);
    }

    public void LoadLevel(int index) {
        LoadLevel("Level" + index);
    }

    public void LoadNextLevel(int lastMultiplicatorValue) {
        store.SetLastMultiplicator(lastMultiplicatorValue);
        store.LevelPassed();
        LoadLevel("Next");
    }
}