using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    //public
    public Vector2 PositionOffset = Vector2.zero;
    public float LerSpeed = 5.0f;

    protected float targetXPos = 0f;
    protected float targetYPos = 0f;
    private GameObject _player;

    public float MaxPosX = 12f;
    public float MaxPosY = 5f;
    public float MinPosX = -12f;
    public float MinPosY = -5f;


    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (_player == null)
            return;


        targetXPos = Mathf.Lerp(targetXPos, _player.transform.position.x, Time.deltaTime * LerSpeed);
        targetYPos = Mathf.Lerp(targetYPos, _player.transform.position.y, Time.deltaTime * LerSpeed);

        float xPos = Mathf.Clamp(targetXPos, MinPosX + 7, MaxPosX - 7);
        float yPos = Mathf.Clamp(targetYPos, MinPosY + 2.5f, MaxPosY - 2.5f);

        transform.position = new Vector3(xPos, yPos, -10f);
    }

}
