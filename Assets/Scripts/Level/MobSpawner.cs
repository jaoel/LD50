using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawner : MonoBehaviour {
    [SerializeField]
    private WaveContainer _waveContainer = null;

    private void Awake() {
        if (_waveContainer == null) {
            Debug.LogError("Missing wave container");
            Destroy(gameObject);
        }

    }

    // Start is called before the first frame update
    private void Start() {
        SpawnWave(0);

    }

    // Update is called once per frame
    private void Update() {

    }

    public void SpawnWave(int wave) {
        WaveEntry waveEntry = _waveContainer.Waves.SingleOrDefault(x => x.Wave == wave);

        if (waveEntry != null) {
               SpawnEntry(waveEntry);
        }
    }

    private void SpawnEntry(WaveEntry waveEntry) {
        foreach (WaveSpawn spawn in waveEntry.WaveSpawns) {
            for (int i = 0; i < spawn.Count; i++) {
                Instantiate(spawn.EnemyPrefab, GetRandowSpawnPosition(), Quaternion.identity);
            }
        }
    }

    private Vector3 GetRandowSpawnPosition() {
        return transform.position + new Vector3(Random.Range(-1.0f, 1.0f), transform.position.y, Random.Range(-1.0f, 1.0f));
    }
}
