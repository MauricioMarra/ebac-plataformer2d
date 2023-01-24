using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject mainMenu;

    public bool _isMainMenuVisible = false;
    public float _currentTimeScale = 1.0f;

    private void Start()
    {
        PauseGame(false);
    }

    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void LoadScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    public void ToggleMainMenu()
    {
        this._isMainMenuVisible = !this._isMainMenuVisible;

        mainMenu.SetActive(_isMainMenuVisible);

        PauseGame(_isMainMenuVisible);
    }

    public void ShowMainMenu(bool show)
    {
        mainMenu.SetActive(show);
        _isMainMenuVisible = show;
    }

    public void PauseGame(bool isPaused)
    {
        if (isPaused)
        {
            //this._currentTimeScale = 0.0f;

            ShowMainMenu(true);

            GameManager.instance.SwitchState(States.Paused);
        }
        else
        {
            //this._currentTimeScale = 1.0f;

            ShowMainMenu(false);

            GameManager.instance.SwitchState(States.Running);
        }

        //Time.timeScale = _currentTimeScale;
    }
}
