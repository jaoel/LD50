using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SpawnDefinition {
    public GameObject enemyPrefab;

    public float spawnStartTime = 0f;
    public float spawnsPerMinute = 1.0f;
    public float newSpawnsPerMinute = 1.0f;
}

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/WaveContainerScriptableObject", order = 1)]
public class SpawnContainer : ScriptableObject
{
    public List<SpawnDefinition> definitions = new List<SpawnDefinition>();
}
