using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject Projectile;
    public Transform SpawnPos;
    public Cooldown ShootInterval;
    
    public bool IsFlip
    {
        get { return _isFlip; }
    }

    public bool _isFlip = false;

    // Update is called once per frame
    void Update()
    {
        if (ShootInterval.CurrentProgress != Cooldown.Progress.Finished)
            return;
        ShootInterval.CurrentProgress = Cooldown.Progress.Ready;
    }

    public void Shoot()
    {
        if (ShootInterval.CurrentProgress != Cooldown.Progress.Ready) 
            return;

        GameObject bullet = GameObject.Instantiate(Projectile, SpawnPos.position, SpawnPos.rotation);

        if (IsFlip)
            bullet.GetComponent<Projectile>().Speed *= -1;

        ShootInterval.StartCooldown();
    }
}
