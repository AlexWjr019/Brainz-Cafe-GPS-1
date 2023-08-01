using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    private bool inRange, triggered = false;
    public KeyCode interactKey;

    void Start()
    {
        TriggerDialogue();

        Time.timeScale = 0f;
    }

    private void Update()
    {
        if (!triggered)
        {
            if (inRange)
            {
                if (Input.GetKeyDown(interactKey))
                {
                    TriggerDialogue();
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inRange = true;
        }
    }

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        triggered = true;
    }
}
