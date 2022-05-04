using System;
using System.Collections.Generic;
using UnityEngine;

public class CubesStack : MonoBehaviour {
    public event Action<int> Changed;

    [SerializeField] private CollectedCube cubePrefab;
    [SerializeField] private Transform cubeSpawnPosition;

    private List<CollectedCube> _cubes = new List<CollectedCube>();
    private int _cubesCount = 0;

    public void Push() {
        var spawnPosition = GetSpawnPosition();
        var newCube = Instantiate(cubePrefab);
        newCube.Init(this, spawnPosition, Quaternion.identity, transform);
        _cubes.Add(newCube);
        _cubesCount++;
        Changed?.Invoke(_cubesCount);
    }

    public void Pop(CollectedCube cube, bool destroy = false) {
        _cubes.Remove(cube);
        cube.gameObject.transform.SetParent(null);
        _cubesCount--;
        Changed?.Invoke(_cubesCount);
        if (destroy) {
            Destroy(cube.gameObject);
        }
    }

    public void Pop() {
        Pop(_cubes[0], true);
    }

    private Vector3 GetSpawnPosition() {
        var spawnPoint = cubeSpawnPosition.localPosition;
        cubeSpawnPosition.localPosition += Vector3.up;
        return spawnPoint;
    }
}