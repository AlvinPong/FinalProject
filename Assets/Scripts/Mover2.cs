using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover2 : MonoBehaviour
{
    public float Speed = 5f;

    public bool Direction = true;
    //Direction = true goes left
    //Direction = false goes right

    public LayerMask TargetLayerMask;
    public float CheckDistance = 1f;

    private PauseManager _pauseManager;
    // Start is called before the first frame update
    void Start()
    {
        _pauseManager = GameObject.Find("PauseManager").GetComponent<PauseManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Check();
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
            TargetMovePos.x -= Speed * Time.deltaTime;
        }
        if (Direction == false)
        {
            TargetMovePos.x += Speed * Time.deltaTime;
        }

        transform.position = TargetMovePos;
    }
    public void Check()
    {
        bool CheckRight = Physics2D.Raycast(transform.position, Vector2.right, CheckDistance, TargetLayerMask);
        bool CheckLeft = Physics2D.Raycast(transform.position, Vector2.left, CheckDistance, TargetLayerMask);
        if (CheckRight == true)
        {
            Direction = true;
        }
        if (CheckLeft == true)
        {
            Direction = false;
        }
    }
}
