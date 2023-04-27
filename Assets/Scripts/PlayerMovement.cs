using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : Movement
{
    protected override void HandleInput()
    {
        //if (!_isGrounded) { return; } //for stopping movement while in air
        _inputDirection = new Vector2(Input.GetAxis("Horizontal"), 0f);
        if (Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.UpArrow))
        {
            DoJump();
        }
    }

}
