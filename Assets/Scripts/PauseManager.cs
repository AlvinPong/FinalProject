using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    
    public GameObject PauseMenu;
    public bool IsPause = false;
    // Start is called before the first frame update
    void Start()
    {
        if (!PauseMenu)
            return;
        if (PauseMenu != null)
        {
            PauseMenu.SetActive(false);
            IsPause = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (IsPause == false && Input.GetKeyDown(KeyCode.P))
        {
            LoadPauseMenu();
        }
        if (IsPause == true && Input.GetKeyDown(KeyCode.R)) 
        {
            UnloadPauseMenu();
        }
    }

    private void LoadPauseMenu()
    {
        if (PauseMenu != null)
        {
            PauseMenu.SetActive(true);
            IsPause = true;
        }
    }
    private void UnloadPauseMenu()
    {
        if(PauseMenu != null)
        {
            PauseMenu.SetActive(false);
            IsPause= false;
        }
    }
    
}
