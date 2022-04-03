using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    private static GameManager _instance = null;
    private LevelManager _levelManager = null;

    public static GameManager Instance => _instance;
    public LevelManager LevelManager => _levelManager;

    public GameObject gameOverCanvas;
    private bool gameover = false;

    private void Awake() {
        gameOverCanvas.SetActive(false);
        if (_instance == null) {
            _instance = this;
        } else {
            Destroy(gameObject);
            Debug.LogWarning("A duplicate GameManager was found");
        }
        _levelManager = GetComponent<LevelManager>();
    }

    private void Start() {
       // DontDestroyOnLoad(gameObject);
    }

    private void Update() {
        if (gameover && Input.anyKeyDown) {
            Time.timeScale = 1f;
            gameOverCanvas.SetActive(false);
            gameover = false;
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.buildIndex);
        }
#if !UNITY_WEBGL
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
        }
#endif
    }

    public static void GameOver() {
        _instance.gameOverCanvas.SetActive(true);
        _instance.gameover = true;
        Time.timeScale = 0f;
    }
}
