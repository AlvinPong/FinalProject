using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDisable : MonoBehaviour
{
    public GameObject Trap;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.gameObject.CompareTag("Player"))
            return;
        if(Trap != null)
            Trap.SetActive(false);
    }
}
