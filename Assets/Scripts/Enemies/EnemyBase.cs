using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBase : MonoBehaviour {
    
    [SerializeField]
    protected float _maxHealth = 0.0f;

    protected NavMeshAgent _navMeshAgent = null;
    protected Rigidbody _rigidBody = null;

    protected Player _player = null;

    protected float _health = 0.0f;

    protected virtual void Awake() {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _rigidBody = GetComponent<Rigidbody>();

        _player = GameObject.FindGameObjectWithTag("Player")?.GetComponent<Player>();
        _health = _maxHealth;
    }

    protected virtual void Start() {
    }

    protected virtual void Update() {

    }

    public virtual void SetTarget(GameObject target) {
        SetTarget(target.transform.position);
    }

    public virtual void SetTarget(Vector3 target) {
        _navMeshAgent.SetDestination(target);
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