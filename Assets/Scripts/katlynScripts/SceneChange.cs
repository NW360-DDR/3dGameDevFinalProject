using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneChange : MonoBehaviour
{
    public string winLevel;
    public GameObject uiText;

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            uiText.SetActive(true);

            if(Input.GetKeyDown(KeyCode.P))
            {
                SceneManager.LoadScene(winLevel);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            uiText.SetActive(false);

        }
    }
}
