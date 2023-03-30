using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mover : MonoBehaviour
{
    public float Speed = 5f;

    public bool Direction = true;
    //Direction = true goes up
    //Direction = false goes down

    private PauseManager _pauseManager;

    // Start is called before the first frame update
    void Start()
    {
        _pauseManager = GameObject.Find("PauseManager").GetComponent<PauseManager>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (_pauseManager)
        {
            if (_pauseManager.IsPause)
            {
                return;
            }
        }
        
        Vector2 TargetMovePos = transform.position;
        if (Direction)
        {
            TargetMovePos.y += Speed * Time.deltaTime;
        }
        if (Direction == false)
        {
            TargetMovePos.y -= Speed * Time.deltaTime;
        }
            
        transform.position = TargetMovePos;
    }
}
