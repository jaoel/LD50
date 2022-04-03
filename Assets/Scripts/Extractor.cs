using UnityEngine;

class Extractor : MonoBehaviour {
    [SerializeField]
    private Animator _animator = null;

    [SerializeField]
    private int _maxHealth = 1000;

    public GameObject extractionText;

    private bool isExtracting = false;

    public void Start() {
        
    }

    private void Update() {
        if (_maxHealth <= 0) {
            Debug.LogError("YOU LOST!");
        }
    }

    public void BeginExtraction() {
        if (!isExtracting) {
            _animator.SetBool("active", true);
            extractionText.SetActive(false);
            GameManager.Instance.LevelManager.StartLevel();
            isExtracting = true;
        }
    }

    public void EndExtraction() {
        if (isExtracting) {
            _animator.SetBool("active", false);
            extractionText.SetActive(true);
            isExtracting = false;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.TryGetComponent(out Player player)) {
            BeginExtraction();
        }
    }
}
