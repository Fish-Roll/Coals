using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MainMenu
{
    public class NewGame : MonoBehaviour
    {
        public void LoadGame()
        {
            SceneManager.LoadScene("Scenes/SceneWithLevel");
        }
    }
}
