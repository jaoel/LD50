using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class WaveSpawn {
    public int Count = 0;
    public GameObject EnemyPrefab = null;
}

[Serializable]
public class WaveEntry {
    public int Wave = 0;
    public List<WaveSpawn> WaveSpawns = new List<WaveSpawn>();
}

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/WaveContainerScriptableObject", order = 1)]
public class WaveContainer : ScriptableObject
{
    public List<WaveEntry> Waves = new List<WaveEntry>();
}
