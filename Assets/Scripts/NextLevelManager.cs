﻿using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NextLevelManager : MonoBehaviour {
    [SerializeField] [Range(0.5f, 0.9f)] private float minAnimationDuration;
    [SerializeField] [Range(0.9f, 2f)] private float maxAnimationDuration;
    [SerializeField] private Ease easeType;
    [SerializeField] private TMP_Text coinsText;
    [SerializeField] private Transform spawnTransform;
    [SerializeField] private Button nextButton;
    [SerializeField] private Store store;
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private float spread;

    private Vector3 _targetPosition;
    private Vector3 _spawnPosition;
    private bool _coinsAdded = false;

    private void Awake() {
        _targetPosition = coinsText.transform.position;
        _spawnPosition = spawnTransform.position;
        nextButton.interactable = false;
        nextButton.onClick.AddListener(Next);
    }

    private void Update() {
        if (_coinsAdded) return;

        AddCoins();
        _coinsAdded = true;
        nextButton.interactable = true;
    }

    public void Next() {
        // next level is current before we called store.LevelPassed in previous scene
        SceneManager.LoadScene("Level" + store.GetCurrentLevelIndex());
    }

    private void AddCoins() {
        var lastMultiplicator = store.GetLastMultiplicator();
        // actually level is incremented, but we add for previous level, so -10
        // var coinsToAdd = (store.GetCoinsForCurrentLevel() - 10) * lastMultiplicator;
        var coinsToAdd = (store.GetCoinsForCurrentLevel()) * lastMultiplicator;
        const int step = 10; // assumed that coinsToAdd % step = 0
        var coins = new Queue<GameObject>();
        for (var i = 0; i < coinsToAdd / step; i++) {
            var coin = Instantiate(coinPrefab);
            coin.transform.position = _spawnPosition + new Vector3(Random.Range(-spread, spread), Random.Range(-spread, spread), Random.Range(-spread, spread));
            coin.transform.SetParent(spawnTransform);
            coin.SetActive(false);
            coins.Enqueue(coin);
        }

        var currentCoins = store.GetCoins();

        var duration = Random.Range(minAnimationDuration, maxAnimationDuration);
        while (coins.Count > 0) {
            var coin = coins.Dequeue();
            coin.SetActive(true);
            coin.transform.DOMove(_targetPosition, duration)
                .SetEase(easeType)
                .OnComplete(() => {
                    coin.SetActive(false);
                    currentCoins += step;
                    coinsText.SetText(currentCoins.ToString());
                });
        }

        store.AddCoins(coinsToAdd);
    }
}