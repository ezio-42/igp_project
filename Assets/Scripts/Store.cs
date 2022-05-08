using TMPro;
using UnityEngine;

public class Store : MonoBehaviour {
    // levels indexed starting from 0
    private const int MaxLevelIndex = 4;
    private const int AmountOfLevelsInTutorial = 2;
    public TMP_Text coinsText;
    public int coinsPerLevelIndexMultiplicator = 10;

    public void Start() {
        coinsText.SetText(GetCoins().ToString());
    }

    public int GetCurrentLevelIndex() {
        return PlayerPrefs.GetInt("CurrentLevelIndex", 0);
    }

    public int GetNextLevelIndex() {
        var currentLevelIndex = GetCurrentLevelIndex();
        var tutorialPassed = GetTutorialPassed();
        var nextLevelIndex = (currentLevelIndex + 1) % (MaxLevelIndex + 1);

        if (tutorialPassed)
            if (nextLevelIndex < AmountOfLevelsInTutorial)
                return AmountOfLevelsInTutorial;

        return nextLevelIndex;
    }

    public void LevelPassed() {
        var currentLevelIndex = GetCurrentLevelIndex();
        if (currentLevelIndex == AmountOfLevelsInTutorial - 1) TutorialPassed();
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
        return currentLevelIndex * coinsPerLevelIndexMultiplicator + coinsPerLevelIndexMultiplicator;
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