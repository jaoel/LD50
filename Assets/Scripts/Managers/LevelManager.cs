using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    private List<MobSpawner> _spawners = null;

    private int _currentWave = 0;

    private void Awake() {
        _currentWave = 0;
    }

    private void Start() {
        _spawners = GameObject.FindGameObjectsWithTag("Spawner").Select(x => x.GetComponent<MobSpawner>()).ToList();
    }

    // Update is called once per frame
    private void Update() {

    }

    public void TriggerSpawn() {
        _currentWave++;

        foreach (MobSpawner spawner in _spawners) {
            spawner.SpawnWave(_currentWave);
        }
    }
}