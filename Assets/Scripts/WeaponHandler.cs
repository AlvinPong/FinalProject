using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    public Weapon CurrentWeapon;
    public Transform RightWeaponPos;
    public Transform LeftWeaponPos;
    

    public bool _tryShoot = false;

    protected Movement _movement;

    private PauseManager _pauseManager;
    // Start is called before the first frame update
    void Start()
    {
        _movement = GetComponent<Movement>();
        _pauseManager = GameObject.Find("PauseManager").GetComponent<PauseManager>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckPause();
        if (Input.GetButtonDown("Fire1"))
            _tryShoot = true;
        if (Input.GetButtonUp("Fire1"))
            _tryShoot = false;

        if (_tryShoot)
            CurrentWeapon.Shoot();

        HandleWeapon();
    }

    protected virtual void HandleInput()
    {

    }

    protected virtual void HandleWeapon()
    {
        if (CurrentWeapon == null)
            return;

        bool isFlip = false;

        if (_movement != null && _movement.FlipAnim)
        {
            isFlip = true;
        }

        if (isFlip)
        {
            CurrentWeapon.transform.position = LeftWeaponPos.position;
            CurrentWeapon.transform.localScale = new Vector3(-1, 1, 1);
            CurrentWeapon._isFlip = isFlip;
        }
        else
        {
            CurrentWeapon.transform.position = RightWeaponPos.position;
            CurrentWeapon.transform.localScale = Vector3.one;
            CurrentWeapon._isFlip = isFlip;
        }
    }
    public void EquipWeapon(Weapon weapon)
    {
        if (weapon != null) 
            CurrentWeapon = weapon;
    }
    public void CheckPause()
    {
        if (!_pauseManager)
            return;
        if (_pauseManager.IsPause)
        {
            _tryShoot = false;
            return;
        }
    }
}
