using UnityEngine;
using UnityEngine.SceneManagement;

public class UIDriver : MonoBehaviour
{
    public void LoadGameScreen(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}