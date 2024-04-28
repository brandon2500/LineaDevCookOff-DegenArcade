using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GroupToggler : MonoBehaviour
{
    public GameObject[] groupsToToggle;
    public GameObject mainMenuGroup;
    public GameObject arcadeFrontGroup;
    public GameObject gameGroup;
    public GameObject prizesGroup;


    public void ToggleGroupVisibility()
    {
        foreach (GameObject group in groupsToToggle)
        {
            group.SetActive(!group.activeSelf);
        }
    }

    public void ResetScreen()
    {
        prizesGroup.SetActive(false);
        gameGroup.SetActive(false);
        arcadeFrontGroup.SetActive(false);
        mainMenuGroup.SetActive(true);
    }
}
