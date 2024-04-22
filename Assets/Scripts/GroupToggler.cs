using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GroupToggler : MonoBehaviour
{
    public GameObject[] groupsToToggle;

    public void ToggleGroupVisibility()
    {
        foreach (GameObject group in groupsToToggle)
        {
            group.SetActive(!group.activeSelf);
        }
    }
}
