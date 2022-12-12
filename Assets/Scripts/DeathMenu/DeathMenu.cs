using UnityEngine;
using UnityEngine.SceneManagement;

namespace DeathMenu
{
    public class DeathMenu : MonoBehaviour
    {
        public void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void LeaveToMainMenu()
        {
            SceneManager.LoadScene("Scenes/MainMenu");
        }
    }
}