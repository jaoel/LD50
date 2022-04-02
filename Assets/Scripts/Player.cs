using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    private Rigidbody _rigidBody = null;

    [SerializeField]
    private float _maxHealth = 0.0f;

    [SerializeField]
    private float _maxVelocity = 0.0f;

    [SerializeField]
    private float _acceleration = 0.0f;

    [SerializeField]
    private float _deceleration = 0.0f;

    [SerializeField]
    private float _invulnDuration = 0.0f;

    private float _velocity = 0.0f;
    private Vector3 _oldDir = Vector3.zero;

    private float _invulnTimer = 0.0f;
    private float _health = 0.0f;

    private void Awake() {
        _rigidBody = GetComponent<Rigidbody>();
        _health = _maxHealth;
    }

    private void Start() {
        
    }

    private void Update() {
        Movement();
    }

    private void Movement() {
        Rotate();

        Vector3 forwardDir = Vector3.zero;
        Vector3 rightDir = Vector3.zero;

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) {
            forwardDir = Camera.main.transform.forward;
        } else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) {
            forwardDir = -Camera.main.transform.forward;
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
            rightDir = Camera.main.transform.right;
        } else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
            rightDir = -Camera.main.transform.right;
        }

        Vector3 direction = (new Vector3(forwardDir.x, 0.0f, forwardDir.z) + new Vector3(rightDir.x, 0.0f, rightDir.z)).normalized;
        direction.y = 1.0f;

        if (direction.sqrMagnitude > 0.0f) {
            _velocity += _acceleration * Time.deltaTime;
            _velocity = Mathf.Clamp(_velocity, 0.0f, _maxVelocity);
        }
        else {
            _velocity -= _deceleration * Time.deltaTime;
            _velocity = Mathf.Clamp(_velocity, 0.0f, _velocity);
            direction = _oldDir;
        }

        _rigidBody.velocity = Vector3.Scale(direction, new Vector3(_velocity, Physics.gravity.y, _velocity));
        _oldDir = direction;
    }

    private void Rotate() {
        Plane playerGroundPlane = new Plane(Vector3.up, new Vector3(transform.position.x, transform.position.y, transform.position.z));
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float distance;
        if (playerGroundPlane.Raycast(ray, out distance)) {
            Vector3 point = ray.GetPoint(distance);
            transform.LookAt(point);
        }
    }

    public bool ReceiveDamage(float damage) {
        if (Time.time - _invulnTimer > _invulnDuration) {
            _invulnTimer = Time.time;
            _health -= damage;
        }

        return _health > 0.0f;
    }

    private void OnCollisionEnter(Collision collision) {
        
    }
}
