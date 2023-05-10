using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public GameObject panel;
    public TMP_Text text1;
    public TMP_Text text2;
    public TMP_Text text3;
    public TMP_Text text4;
    public TMP_Text text5;

    private bool isTrigger6Entered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (gameObject.CompareTag("Trigger1"))
            {
                panel.SetActive(true);
                text1.gameObject.SetActive(true);
            }
            else if (gameObject.CompareTag("Trigger2"))
            {
                panel.SetActive(true);
                text2.gameObject.SetActive(true);
            }
            else if (gameObject.CompareTag("Trigger3"))
            {
                panel.SetActive(true);
                text3.gameObject.SetActive(true);
            }
            else if (gameObject.CompareTag("Trigger4"))
            {
                panel.SetActive(true);
                text4.gameObject.SetActive(true);
            }
            else if (gameObject.CompareTag("Trigger5"))
            {
                panel.SetActive(true);
                text5.gameObject.SetActive(true);
            }
            else if (gameObject.CompareTag("Trigger6"))
            {
                DisableAllTextObjects();
                panel.SetActive(false);
                isTrigger6Entered = true;
                Invoke("LoadNextScene", 1f);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (gameObject.CompareTag("Trigger1"))
            {
                text1.gameObject.SetActive(false);
                panel.SetActive(false);
            }
            else if (gameObject.CompareTag("Trigger2"))
            {
                text2.gameObject.SetActive(false);
                panel.SetActive(false);
            }
            else if (gameObject.CompareTag("Trigger3"))
            {
                text3.gameObject.SetActive(false);
                panel.SetActive(false);
            }
            else if (gameObject.CompareTag("Trigger4"))
            {
                text4.gameObject.SetActive(false);
                panel.SetActive(false);
            }
            else if (gameObject.CompareTag("Trigger5"))
            {
                text5.gameObject.SetActive(false);
                panel.SetActive(false);
            }
        }
    }

    private void DisableAllTextObjects()
    {
        text1.enabled = false;
        text2.enabled = false;
        text3.enabled = false;
        text4.enabled = false;
        text5.enabled = false;
    }

    private void LoadNextScene()
    {
        if (isTrigger6Entered)
        {
            SceneManager.LoadScene("MainScene");
        }
    }
}