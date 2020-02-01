﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.WeaponSystem
{
    public interface IWeaponTarget
    {
        GameObject gameObject { get; }

        bool DoDamage(DummyDamage dd) {
            
        }
    }
}