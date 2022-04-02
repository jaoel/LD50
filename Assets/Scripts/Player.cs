using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    private enum PlayerState {
        Moving,
        Attacking,
    }

    private CharacterController _characterController = null;

    [SerializeField]
    private GameObject _hitDiscTemplate = null;

    [SerializeField]
    private int _maxHealth = 0;

    [SerializeField]
    private float _maxVelocity = 0.0f;

    [SerializeField]
    private float _acceleration = 0.0f;

    [SerializeField]
    private float _deceleration = 0.0f;

    [SerializeField]
    private float _invulnDuration = 0.0f;

    [SerializeField]
    private Animator _animator = null;

    private float _invulnTimer = 0.0f;
    private int _health = 0;

    private float _attackCooldown = 0.5f;
    private float _attackTime = 0f;

    private PlayerState _currentState = PlayerState.Moving;
    private Vector3 _inputVector = Vector3.zero;
    private Vector3 _lastInputVector = Vector3.zero;
    private Vector3 _movementVector = Vector3.zero;
    private Vector3 _targetRotationVector = Vector3.zero;
    private bool _pressedAttack = false;
    private Vector3 _currentVelocity = Vector3.zero;
    private Camera _mainCamera = null;

    private void Awake() {
        _characterController = GetComponent<CharacterController>();
        _health = _maxHealth;
        _attackTime = -_attackCooldown;
    }

    private void Start() {
        
    }

    private void Update() {
        GatherInput();
        _movementVector = Vector3.zero;
        _targetRotationVector = Vector3.zero;

        switch (_currentState) {
            case PlayerState.Moving:
                UpdateMovementState();
                break;
            case PlayerState.Attacking:
                UpdateAttackState();
                break;
        }

        UpdateMovement();
        UpdateRotation();
    }

    private void GatherInput() {
        _lastInputVector = _inputVector;

        _inputVector.x = Input.GetAxisRaw("Horizontal");
        _inputVector.y = 0f;
        _inputVector.z = Input.GetAxisRaw("Vertical");

        if (Vector3.Distance(_lastInputVector, _inputVector) > 0.25f) {
            _mainCamera = Camera.main;
        }

        float deadzone = 0.1f;
        if (Mathf.Abs(_inputVector.x) < deadzone) {
            _inputVector.x = 0f;
        }
        if (Mathf.Abs(_inputVector.y) < deadzone) {
            _inputVector.y = 0f;
        }

        _pressedAttack = Input.GetKeyDown(KeyCode.Space);
    }

    private void SwitchState(PlayerState nextState) {
        switch (nextState) {
            case PlayerState.Moving:
                break;
            case PlayerState.Attacking:
                Attack();
                break;
        }

        _currentState = nextState;
    }

    private void Attack() {
        _attackTime = Time.time;
        _animator.SetTrigger("attack");

        GameObject discCopy = Instantiate(_hitDiscTemplate);
        discCopy.SetActive(true);
        discCopy.transform.SetParent(null);
        discCopy.transform.position = _hitDiscTemplate.transform.position;
        discCopy.transform.rotation = _hitDiscTemplate.transform.rotation;
        Destroy(discCopy, _attackCooldown);

        EnemyBase[] enemies = FindObjectsOfType<EnemyBase>();
        foreach (var enemy in enemies) {
            Vector3 toEnemy = enemy.transform.position - transform.position;
            if (toEnemy.magnitude < 4f && Vector3.Angle(transform.forward, toEnemy) < 100f) {
                enemy.Damage(10);
            }
        }
    }

    private void UpdateAttackState() {
        if (Time.time > _attackTime + _attackCooldown) {
            SwitchState(PlayerState.Moving);
        }
    }

    private void UpdateMovementState() {
        if (_pressedAttack) {
            SwitchState(PlayerState.Attacking);
            return;
        }

        Camera camera = _mainCamera != null ? _mainCamera : Camera.main;
        Vector3 cameraInputVector = Quaternion.Euler(0f, camera.transform.rotation.eulerAngles.y, 0f) * _inputVector.normalized;
        _movementVector = cameraInputVector * _maxVelocity;
        _targetRotationVector = cameraInputVector;
    }

    private float AccelDecel(float currentSpeed, float targetSpeed, float acceleration, float deceleration) {
        float delta = targetSpeed - currentSpeed;

        if (delta > 0f) {
            return Mathf.MoveTowards(currentSpeed, targetSpeed, acceleration * Time.deltaTime);
        } else {
            return Mathf.MoveTowards(currentSpeed, targetSpeed, deceleration * Time.deltaTime);
        }
    }

    private void UpdateMovement() {

        _currentVelocity.x = AccelDecel(_currentVelocity.x, _movementVector.x, _acceleration, _deceleration);
        _currentVelocity.z = AccelDecel(_currentVelocity.z, _movementVector.z, _acceleration, _deceleration);

        if (_currentVelocity.magnitude > _maxVelocity) {
            _currentVelocity = _currentVelocity.normalized * _maxVelocity;
        }

        _animator.SetFloat("speed", Mathf.Clamp01(_currentVelocity.magnitude / _maxVelocity));
        _characterController.Move(_currentVelocity * Time.deltaTime + Physics.gravity * Time.deltaTime);
    }

    private void UpdateRotation() {
        Debug.DrawLine(transform.position, transform.position + _targetRotationVector);
        if (_targetRotationVector != Vector3.zero) {
            Quaternion targetRotation = Quaternion.LookRotation(_targetRotationVector, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 2.5f);
        }
    }

    public bool ReceiveDamage(int damage) {
        if (Time.time - _invulnTimer > _invulnDuration) {
            _invulnTimer = Time.time;
            _health -= damage;
        }

        return _health > 0.0f;
    }

    private void OnCollisionEnter(Collision collision) {
        
    }
}
