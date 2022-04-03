using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawner : MonoBehaviour {
    public void SpawnEntry(GameObject enemyPrefab) {
        Instantiate(enemyPrefab, GetRandowSpawnPosition(), Quaternion.identity);
    }

    private Vector3 GetRandowSpawnPosition() {
        return transform.position + new Vector3(Random.Range(-1.0f, 1.0f), transform.position.y, Random.Range(-1.0f, 1.0f));
    }
}
