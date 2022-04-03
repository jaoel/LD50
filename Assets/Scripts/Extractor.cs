using System;
using UnityEngine;

class Extractor : MonoBehaviour {
    public static Extractor Instance { get; private set; }

    [SerializeField]
    private Animator _animator = null;

    [SerializeField]
    private int _maxHealth = 1000;

    private int _currentHealth = 1000;
    public GameObject extractionText;

    public AudioSource audioSource;

    public Transform healthBar;

    private bool isExtracting = false;

    public float HealthPercent => Mathf.Clamp01(_currentHealth / (float)_maxHealth);

    public void Awake() {
        Instance = this;
        _currentHealth = _maxHealth;
    }

    private void Update() {
        healthBar.localScale = new Vector3(HealthPercent, 1f, 1f);
    }

    public void BeginExtraction() {
        if (!isExtracting) {
            _animator.SetBool("active", true);
            extractionText.SetActive(false);
            GameManager.Instance.LevelManager.StartLevel();
            isExtracting = true;
            audioSource.Play();
        }
    }

    public void EndExtraction() {
        if (isExtracting) {
            _animator.SetBool("active", false);
            extractionText.SetActive(true);
            isExtracting = false;
            audioSource.Stop();
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.TryGetComponent(out Player player)) {
            BeginExtraction();
        }
    }

    internal void ReceiveDamage(int damage) {
        _currentHealth -= damage;
        if (_currentHealth <= 0) {
            GameManager.GameOver();
            Debug.LogError("YOU LOST!");
        }
    }

    internal Vector3 CenterPos() {
        return transform.position + Vector3.up;
    }
}
