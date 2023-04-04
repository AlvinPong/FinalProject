using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Dialog : MonoBehaviour
{
    public GameObject Dialogpanel;
    public TMP_Text TextBox;
    public string[] Dialogues;

    private bool _inRange = false;
    private int _currentDialogue = 0;
    // Start is called before the first frame update
    void Start()
    {
        //Dialogpanel?.SetActive(false);
        if (Dialogpanel != null)
        {
            Dialogpanel.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!_inRange)
            return;
        if (Input.GetButtonDown("Submit"))
        {
            if (_currentDialogue >= Dialogues.Length)
                return;

            if (Dialogpanel != null)
            {
                Dialogpanel.SetActive(true);
            }

            if (TextBox != null)
            {
                TextBox.text = Dialogues[_currentDialogue];
            }

            _currentDialogue++;
            _currentDialogue = Mathf.Clamp(_currentDialogue, 0, Dialogues.Length);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.gameObject.CompareTag("Player"))
            return;
        _inRange = true;
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (!col.gameObject.CompareTag("Player"))
            return;
        _inRange = false;
        Dialogpanel.SetActive(false);
    }
}
