using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITriggerCheckable
{
    bool IsWithinAttackDistance { get; set; }
    void SetAttackDistanceBool(bool isWithinAttackDistance);
}
