using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackButton : MonoBehaviour
{
    public GameObject statsCanvas;
    public GameObject shieldCanvas;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (statsCanvas.activeInHierarchy == true)
            {
                statsCanvas.SetActive(false);
            }
            else if (shieldCanvas.activeInHierarchy == true)
            {
                shieldCanvas.SetActive(false);
            }
            else
            {
                Application.Quit();
                Debug.Log("Quit");
            }
        }
    }
}
