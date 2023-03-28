using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public GameObject WeaponPickUp;
   
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("Player"))
            return;
        if (WeaponPickUp == null)
            return;
        WeaponHandler _weaponHandler = col.GetComponent<WeaponHandler>();

        GameObject _gameObject = GameObject.Instantiate(WeaponPickUp, transform.position, transform.rotation);

        Weapon weapon = _gameObject.GetComponent<Weapon>();
        
        _weaponHandler.EquipWeapon(weapon);

        Destroy(this.gameObject);
    }
}
