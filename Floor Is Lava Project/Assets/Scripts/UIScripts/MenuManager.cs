using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AD1020
{

public class MenuManager : MonoBehaviour
{
        public GameObject optionsMenu;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Escape Pressed");
            optionsMenu.SetActive(!optionsMenu.gameObject.activeSelf);
        }
    }
}
}
