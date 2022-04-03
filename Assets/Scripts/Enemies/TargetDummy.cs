using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDummy : EnemyBase
{
    protected override void Awake() {
        base.Awake();
    }

    protected override void Start() {
        base.Start();
    }

    protected override void Update() {
        base.Update();
    }

    public override void Damage(int damage) {
        base.Damage(100);
    }
}
