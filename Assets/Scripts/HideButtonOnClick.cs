using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HideButtonOnClick : MonoBehaviour
{
    public void HideButton()
    {
        gameObject.SetActive(false);
    }
}
