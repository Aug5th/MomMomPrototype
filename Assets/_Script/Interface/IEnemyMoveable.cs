using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMoveable
{
    Rigidbody2D Rigidbody { get; set; }
    bool IsFacingRight { get; set; }
    void Move(Vector2 velocity);
    void CheckForLeftOrRightFacing(Vector2 velocity);
}
