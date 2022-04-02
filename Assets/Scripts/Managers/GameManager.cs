using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    private static GameManager _instance = null;
    private LevelManager _levelManager = null;

    public static GameManager Instance => _instance;
    public LevelManager LevelManager => _levelManager;

    private void Awake() {
        if (_instance == null) {
            _instance = this;
        } else {
            Destroy(gameObject);
            Debug.LogWarning("A duplicate GameManager was found");
        }
        _levelManager = GetComponent<LevelManager>();
    }

    private void Start() {
        DontDestroyOnLoad(gameObject);
    }

    private void Update() {

    }
}
