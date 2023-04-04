using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DSManager : MonoBehaviour
{
    public GameObject DeathScreen;
    private Health _health;
    public float Cooldown = 3f;
    // Start is called before the first frame update
    void Start()
    {
        if (!DeathScreen)
            return;
        if (DeathScreen != null)
        {
            DeathScreen.SetActive(false);
        }
        _health = GameObject.Find("Player").GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_health.CurrentHealth <= 0)
        {
            Invoke("ActivateScreen", Cooldown);
        }
    }

    private void ActivateScreen()
    {
        DeathScreen.SetActive(true);
    } 
}
