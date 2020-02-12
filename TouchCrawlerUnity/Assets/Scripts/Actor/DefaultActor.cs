﻿using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Events;
using Assets.Scripts.WeaponSystem;
using Assets.WeaponSystem;
using UnityEngine;

[RequireComponent(typeof(MovementController))]
[RequireComponent(typeof(StatsController))]
public class DefaultActor : MonoBehaviour, IActor, IEventListener, IWeaponOwner
{
    public int actorLevel { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public IActor target { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public Weapon weapon { get; set; }

    public MovementController movementController
    {
        get;
        private set;
    }

    public virtual Weapon.WeaponTargetType AttackWeaponTargetType { get => attackWeaponTargetType; set => attackWeaponTargetType = value; }

    public Weapon.WeaponTargetType attackWeaponTargetType;

    public virtual Weapon.WeaponTargetType DefenseWeaponTargetType { get => defenseWeaponTargetType; set => defenseWeaponTargetType = value; }

    public Weapon.WeaponTargetType defenseWeaponTargetType;

    public IActor actor => this;

    public void Start()
    {
        movementController = GetComponent<MovementController>();
        weapon = GetComponentInChildren<Weapon>();
        if (IsPlayer())
        {
            EventSystem.AddEventListener(EventSystem.EventChannel.player, EventSystem.EventSubChannel.input, this);
        }
    }
    public bool IsPlayer()
    {
        if (this is PlayerActor)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void PickUpItem(object item)
    {
        throw new System.NotImplementedException();
    }

    public void UseItem(object item)
    {
        throw new System.NotImplementedException();
    }

    public void AcceptEvent(IEvent e)
    {
        if (e is MoveInputEvent moveInputEvent)
        {
            Vector2 nextLocation = moveInputEvent.ray.origin;
            movementController.Move(nextLocation);
        }
        if (e is AttackInputEvent attackInputEvent)
        {
            this.weapon?.Fire(attackInputEvent.attackable.GetTarget());
        }
    }

    public bool DoDamage(Damage damage)
    {
        Debug.Log(damage.ToString());
        return true;
    }
}