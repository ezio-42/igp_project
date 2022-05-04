using TMPro;
using UnityEngine;

public class Store : MonoBehaviour {
    public TMP_Text coinsText;
    public void Start() {
        coinsText.SetText(GetCoins().ToString());
        SetCurrentLevelIndex(0);  // TODO: remove after adding more levels
    }

    public int GetCurrentLevelIndex() {
        return PlayerPrefs.GetInt("CurrentLevelIndex", 0);
    }

    public int GetNextLevelIndex() {
        var currentLevelIndex = GetCurrentLevelIndex();
        var tutorialPassed = GetTutorialPassed();
        if (tutorialPassed && (currentLevelIndex > 4 || currentLevelIndex < 2)) {
            return 2;
        }

        return currentLevelIndex + 1;
    }

    public void LevelPassed() {
        var currentLevelIndex = GetCurrentLevelIndex();
        if (currentLevelIndex == 1) {
            TutorialPassed();
        }
        SetCurrentLevelIndex(GetNextLevelIndex());
    }

    private void SetCurrentLevelIndex(int index) {
        PlayerPrefs.SetInt("CurrentLevelIndex", index);
    }

    public void TutorialPassed() {
        PlayerPrefs.SetInt("TutorialPassed", 1);
    }

    public bool GetTutorialPassed() {
        return PlayerPrefs.GetInt("TutorialPassed", 0) == 1;
    }

    public int GetCoinsForCurrentLevel() {
        var currentLevelIndex = GetCurrentLevelIndex();
        return currentLevelIndex * 10 + 10;
    }

    public void AddCoins(int coins) {
        PlayerPrefs.SetInt("Coins", GetCoins() + coins);
    }

    public int GetCoins() {
        return PlayerPrefs.GetInt("Coins", 0);
    }

    public int GetLastMultiplicator() {
        return PlayerPrefs.GetInt("LastMultiplicator", 1);
    }

    public void SetLastMultiplicator(int lastMultiplicator) {
        PlayerPrefs.SetInt("LastMultiplicator", lastMultiplicator);
    }
}