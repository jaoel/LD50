using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {
    private class SpawnInfo {
        public SpawnDefinition definition;
        public float spawnsPerMinute;
        public float accumulatedSpawnCount = 0f;
    }

    public SpawnContainer spawnDefinitions;
    private List<SpawnInfo> _spawnInfos = new List<SpawnInfo>();
    private List<MobSpawner> _spawners = null;

    public bool Started { get; private set; } = false;
    private float levelStartTime = 0f;

    public float LevelDuration => Time.time - levelStartTime;

    private Dictionary<SpawnDefinition, float> _spawnAccumulator = new Dictionary<SpawnDefinition, float>();

    private void Awake() {
    }

    private void Start() {
        _spawners = FindObjectsOfType<MobSpawner>().ToList();

        foreach (var spawnDef in spawnDefinitions.definitions) {
            _spawnInfos.Add(new SpawnInfo() {
                definition = spawnDef,
                spawnsPerMinute = spawnDef.spawnsPerMinute,
                accumulatedSpawnCount = 0f,
            });
        }
    }

    // Update is called once per frame
    private void Update() {

    }

    public void StartLevel() {
        Started = true;
        levelStartTime = Time.time;
        StartCoroutine(SpawnNextWaveCoroutine());
    }

    public void EndLevel() {
        Started = false;
    }

    IEnumerator SpawnNextWaveCoroutine() {
        while (Started) {
            NextSpawn();
            yield return new WaitForSeconds(1f);
        }
    }

    public void NextSpawn() {
        List<(GameObject enemyPrefab, int count)> toSpawn = new List<(GameObject enemyPrefab, int count)>();

        foreach (var info in _spawnInfos) {
            if (LevelDuration >= info.definition.spawnStartTime) {
                info.accumulatedSpawnCount += info.spawnsPerMinute / 60f; // We're running this logic once every second
                info.spawnsPerMinute += info.definition.newSpawnsPerMinute / 60f;
                Debug.Log($"{info.definition.enemyPrefab.name} - accumulatedSpawnCount: {info.accumulatedSpawnCount} spawnsPerMinute: {info.spawnsPerMinute}");

                int numToSpawn = (int)info.accumulatedSpawnCount;
                if (numToSpawn > 0) {
                    toSpawn.Add((info.definition.enemyPrefab, numToSpawn));
                    info.accumulatedSpawnCount -= numToSpawn;
                }
            }
        }

        for (int i = 0; i < toSpawn.Count; i++) {
            for (int ii = 0; ii < toSpawn[i].count; ii++) {
                _spawners[Random.Range(0, _spawners.Count)].SpawnEntry(toSpawn[i].enemyPrefab);
            }
        }
    }
}
