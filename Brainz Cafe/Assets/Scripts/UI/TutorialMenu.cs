using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMenu : MonoBehaviour
{
    [SerializeField]
    CustomerSpawner cs;

    [SerializeField]
    Timer timer;

    void Start()
    {
        cs.gameObject.SetActive(false);
        timer.timerOn = false;
    }
    
    public void StartGame()
    {
        cs.gameObject.SetActive(true);
        timer.timerOn = true;
    }
}
