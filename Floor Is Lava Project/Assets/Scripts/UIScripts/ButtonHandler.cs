using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AD1020
{

    public class ButtonHandler : MonoBehaviour
    {
        public GameObject optionsMenu;
        public GameObject generalEffectsPanel;
        public GameObject audioVolumePanel;
        public GameObject graphicSettingsPanel;
        public GameObject keybindingsPanel;
        public void PlayButton()
        {
            SceneManager.LoadScene(1);
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
            Application.Quit();
        }

        public void QuitButton()
        {
            SceneManager.LoadScene(0);
        }
    }


}

