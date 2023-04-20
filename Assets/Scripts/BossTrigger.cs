using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    public GameObject Slimes1;
    public GameObject Slimes2;
    public GameObject Platform;
    public GameObject Wall;

    protected int SpawnCount = 8;
    private Health _health;

    // Start is called before the first frame update
    void Start()
    {
        _health = GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Slimes1 == null) return;
        if (Slimes2 == null) return;
        if (_health.CurrentHealth == 40 && SpawnCount == 8)
        {
            Instantiate(Slimes1,transform.position,Quaternion.identity);
            SpawnCount--;
        }
        if (_health.CurrentHealth == 35 && SpawnCount == 7)
        {
            Instantiate(Slimes2, transform.position, Quaternion.identity);
            SpawnCount--;
        }
        if (_health.CurrentHealth == 30 && SpawnCount == 6)
        {
            Instantiate(Slimes1, transform.position, Quaternion.identity);
            SpawnCount--;
        }
        if (_health.CurrentHealth == 25 && SpawnCount == 5)
        {
            Instantiate(Slimes2, transform.position, Quaternion.identity);
            SpawnCount--;
        }
        if (_health.CurrentHealth == 20 && SpawnCount == 4)
        {
            Instantiate(Slimes1, transform.position, Quaternion.identity);
            Instantiate(Slimes2, transform.position, Quaternion.identity);
            SpawnCount--;
        }
        if (_health.CurrentHealth == 15 && SpawnCount == 3)
        {
            Instantiate(Slimes1, transform.position, Quaternion.identity);
            Instantiate(Slimes2, transform.position, Quaternion.identity);
            SpawnCount--;
        }
        if (_health.CurrentHealth == 10 && SpawnCount == 2)
        {
            
            Instantiate(Slimes1, transform.position, Quaternion.identity);
            Instantiate(Slimes2, transform.position, Quaternion.identity);
            SpawnCount--;
            if (Platform == null) return;
            Platform.SetActive(false);
        }
        if (_health.CurrentHealth == 5 && SpawnCount == 1)
        {
            Instantiate(Slimes1, transform.position, Quaternion.identity);
            Instantiate(Slimes2, transform.position, Quaternion.identity);
            Instantiate(Slimes1, transform.position, Quaternion.identity);
            Instantiate(Slimes2, transform.position, Quaternion.identity);
            SpawnCount--;
        }
        if (_health.CurrentHealth <= 1)
        {
            if (Wall == null) return;
            Wall.SetActive(false);
        }
    }
}
