using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBase : MonoBehaviour {

    [SerializeField]
    protected int _maxHealth = 0;

    protected NavMeshAgent _navMeshAgent = null;
    protected Rigidbody _rigidBody = null;

    protected Player _player = null;

    protected int _health = 0;

    private float _stunDuration = 2f;
    private float _stunnedTime = 0f;

    public bool IsStunned => _stunnedTime > Time.time;
    private Vector3 _knockbackDir = Vector3.zero;

    protected virtual void Awake() {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _rigidBody = GetComponent<Rigidbody>();
    }

    protected virtual void Start() {
        _player = GameObject.FindGameObjectWithTag("Player")?.GetComponent<Player>();
        _health = _maxHealth;
    }

    protected virtual void Update() {
        // Do knockback when stunned
        if (IsStunned && _knockbackDir != Vector3.zero) {
            _rigidBody.velocity = _knockbackDir *= 0.95f;
        }
    }

    public virtual Vector3 CenterPos() {
        return transform.position + Vector3.up;
    }

    public virtual void Damage(int damage) {
        _maxHealth -= damage;
        _stunnedTime = Time.time + _stunDuration;
        if (_navMeshAgent != null) {
            _navMeshAgent.ResetPath();
        }
        if (_maxHealth <= 0) {
            Destroy(gameObject);
        }
    }

    public void Knockback(Vector3 dir) {
        _knockbackDir = dir;
    }

    public virtual void SetTarget(GameObject target) {
        SetTarget(target.transform.position);
    }

    public virtual void SetTarget(Vector3 target) {
        if (!IsStunned) {
            _navMeshAgent.SetDestination(target);
        }
    }

    protected void OnCollisionEnter(Collision collision) {
        
    }

    protected void OnTriggerEnter(Collider other) {
        
    }

    protected float GetDistanceToPlayer() {
        if (_player == null) {
            return Mathf.Infinity;
        }

        return Vector3.Distance(transform.position, _player.transform.position);
    }

    protected void KillMe() {

    }
}
