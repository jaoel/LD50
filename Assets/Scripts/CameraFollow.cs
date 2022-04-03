using System.Collections;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    public Vector3 offset = new Vector3(4f, 8f, -6f);

    private void Update() {
        if (Player.Instance != null) {
            transform.position = Vector3.Lerp(transform.position, Player.Instance.transform.position + offset, 0.05f);
            transform.rotation = Quaternion.LookRotation(-offset, Vector3.up);
        }        
    }
}