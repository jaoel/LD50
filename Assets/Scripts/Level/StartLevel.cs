using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartLevel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter(Collider other) {
        if (GameManager.Instance.LevelManager.CurrentWave == 0) {
            GameManager.Instance.LevelManager.StartLevel();
        }
    }
}
