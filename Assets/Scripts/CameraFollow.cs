using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Vector3 offset = Vector3.zero;
    private GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        if (Player == null)
            Player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
            if (Player == null)
            {
                return;
            }
            transform.position = Player.transform.position + offset;
    }
}
