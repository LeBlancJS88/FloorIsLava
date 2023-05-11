using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VersionDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<TMPro.TextMeshProUGUI>().text = "v" + Application.version;
    }
}
