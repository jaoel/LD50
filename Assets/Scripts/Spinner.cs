using System.Collections;
using UnityEngine;

public class Spinner : MonoBehaviour {
    public Vector3 speed;

    private Vector3 rotation = Vector3.zero;

    private void Update() {
        rotation += speed * Time.deltaTime;
        transform.localRotation = Quaternion.Euler(rotation);
    }
}