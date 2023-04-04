using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapManager : MonoBehaviour
{
    public GameObject Trap;
    public GameObject SecondTrap;
    public float Timer = 5f;
    
    // Start is called before the first frame update
    void Start()
    {
        if (Trap == null)
            return;
        Trap.SetActive(false);
        if (SecondTrap == null)
            return;
        SecondTrap.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.gameObject.CompareTag("Player"))
            return;
        if (Trap == null)
            return;
        Trap.SetActive(true);
        //Debug.Log("Activate Trap");
        Invoke("ActivateSecondTrap", Timer);
        
    }
    private void ActivateSecondTrap()
    {
        if (SecondTrap == null) 
            return;
        SecondTrap.SetActive(true);
        //Debug.Log("Activate Second Trap");
    }
}
