﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICameraTarget
{
    Vector2 GetTarget(Vector2 trackedObjectPosition);
}