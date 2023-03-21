using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxMovement : Movement
{
    protected override void HandleInput()
    {
        _inputDirection = MovingDirection;
        
    }
}
