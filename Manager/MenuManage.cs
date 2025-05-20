using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManage : MonoBehaviour
{
    public static MenuManage instance { get; set; }
    public GameObject uiCanvas;
    public bool isMenuOpen;
    public GameObject saveMenu;
    public GameObject settingsMenu;
    public GameObject menu;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M) && !isMenuOpen)
        {
            uiCanvas.SetActive(true);

            isMenuOpen = true;
    
          
        }
        else if (Input.GetKeyDown(KeyCode.M) && isMenuOpen)
        {
            saveMenu.SetActive(false);
            settingsMenu.SetActive(false);
            menu.SetActive(true);
            uiCanvas.SetActive(false);
          
        }
    }
}
