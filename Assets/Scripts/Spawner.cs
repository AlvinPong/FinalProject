using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject _gameObject;
    public float SpawnInterval = 2f;
    public float SpawnTimer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        SpawnTimer = SpawnInterval;
    }

    // Update is called once per frame
    void Update()
    {
        if (SpawnTimer >= 0)
        {
            SpawnTimer -= Time.deltaTime;
        }
        else
        {
            Vector3 targetposition = transform.position;
            GameObject.Instantiate(_gameObject, targetposition, Quaternion.identity);
            SpawnTimer = SpawnInterval;
        }
    }
}
