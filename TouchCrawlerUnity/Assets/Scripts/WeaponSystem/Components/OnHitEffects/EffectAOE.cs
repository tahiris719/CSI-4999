using Assets.Scripts.Util.Latches;
using Assets.Scripts.WeaponSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectAOE : OnHitEffect
{ 
    private List<IWeaponTarget> GetTargetsInRadius(Vector3 location, float radius) {
        List<IWeaponTarget> targets = new List<IWeaponTarget>();
        Collider[] targetColliders = Physics.OverlapSphere(location, radius);
        
        foreach (Collider col in targetColliders) {
            if (col.gameObject.GetComponent(IWeaponTarget) != null) {
                targets.Add(col.gameObject.GetComponent(IWeaponTarget));
            }
        }
        return targets;
    }
}