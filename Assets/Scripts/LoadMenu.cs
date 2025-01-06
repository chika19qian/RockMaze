using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMenu : MonoBehaviour
{
    public void MenuButton()
    {
        SceneManager.LoadScene("StartScene");
    }
}