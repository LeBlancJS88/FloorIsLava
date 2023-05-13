using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

    public class ButtonHandler : MonoBehaviour
{
    public GameObject optionsMenu;
    public GameObject generalEffectsPanel;
    public GameObject audioVolumePanel;
    public GameObject graphicSettingsPanel;
    public GameObject keybindingsPanel;
    public GameObject playPanel;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!optionsMenu.activeInHierarchy)
            {
                optionsMenu.SetActive(true);
            }
            else
            {
                optionsMenu.SetActive(false);
            }
        }
    }

    public void PlayButton()
    {
        playPanel.SetActive(true);
    }

    public void OptionsButton()
    {
        optionsMenu.SetActive(true);
    }

    public void CloseButton()
    {
        optionsMenu.SetActive(false);
    }

    public void GeneralButton()
    {
        if (audioVolumePanel.activeInHierarchy || graphicSettingsPanel.activeInHierarchy || keybindingsPanel.activeInHierarchy)
        {
            if (audioVolumePanel.activeInHierarchy)
            {
                audioVolumePanel.SetActive(false);
            }

            if (graphicSettingsPanel.activeInHierarchy)
            {
                graphicSettingsPanel.SetActive(false);
            }

            if (keybindingsPanel.activeInHierarchy)
            {
                keybindingsPanel.SetActive(false);
            }
        }

        generalEffectsPanel.SetActive(true);
    }

    public void AudioButton()
    {
        if (generalEffectsPanel.activeInHierarchy || graphicSettingsPanel.activeInHierarchy || keybindingsPanel.activeInHierarchy)
        {
            if (generalEffectsPanel.activeInHierarchy)
            {
                generalEffectsPanel.SetActive(false);
            }

            if (graphicSettingsPanel.activeInHierarchy)
            {
                graphicSettingsPanel.SetActive(false);
            }

            if (keybindingsPanel.activeInHierarchy)
            {
                keybindingsPanel.SetActive(false);
            }
        }

        audioVolumePanel.SetActive(true);
    }

    public void GraphicsButton()
    {
        if (generalEffectsPanel.activeInHierarchy || audioVolumePanel.activeInHierarchy || keybindingsPanel.activeInHierarchy)
        {
            if (generalEffectsPanel.activeInHierarchy)
            {
                generalEffectsPanel.SetActive(false);
            }

            if (audioVolumePanel.activeInHierarchy)
            {
                audioVolumePanel.SetActive(false);
            }

            if (keybindingsPanel.activeInHierarchy)
            {
                keybindingsPanel.SetActive(false);
            }
        }

        graphicSettingsPanel.SetActive(true);
    }

    public void KeyBindingsButton()
    {
        if (generalEffectsPanel.activeInHierarchy || audioVolumePanel.activeInHierarchy || graphicSettingsPanel.activeInHierarchy)
        {
            if (generalEffectsPanel.activeInHierarchy)
            {
                generalEffectsPanel.SetActive(false);
            }

            if (audioVolumePanel.activeInHierarchy)
            {
                audioVolumePanel.SetActive(false);
            }

            if (graphicSettingsPanel.activeInHierarchy)
            {
                graphicSettingsPanel.SetActive(false);
            }
        }

        keybindingsPanel.SetActive(true);
    }
    public void ExitButton()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif
        Application.Quit(); ;
    }

    public void QuitButton()
    {
        SceneManager.LoadScene(0);
    }

    public void YesTutorial()
    {
        SceneManager.LoadScene(1);
    }

    public void NoTutorial()
    {
        SceneManager.LoadScene(2);
    }
}