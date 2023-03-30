using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover2 : MonoBehaviour
{
    public float Speed = 5f;

    public bool Direction = true;
    //Direction = true goes left
    //Direction = false goes right

    private PauseManager _pauseManager;
    // Start is called before the first frame update
    void Start()
    {
        _pauseManager = GameObject.Find("PauseManager").GetComponent<PauseManager>();
    }

    // Update is called once per frame
    void Update()
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
            TargetMovePos.x -= Speed * Time.deltaTime;
        }
        if (Direction == false)
        {
            TargetMovePos.x += Speed * Time.deltaTime;
        }

        transform.position = TargetMovePos;
    }
}
