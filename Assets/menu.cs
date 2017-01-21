using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class menu : MonoBehaviour
{
    public GameObject start;
    public GameObject controls;

    private void Awake()
    {
        start.SetActive(true);
        controls.SetActive(false);
    }

    public void StartGame()
    {
        Application.LoadLevel(1);
    }

    public void DisplayStartScreen()
    {
        start.SetActive(true);
        controls.SetActive(false);
    }

    public void DisplayControls()
    {
        start.SetActive(false);
        controls.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
