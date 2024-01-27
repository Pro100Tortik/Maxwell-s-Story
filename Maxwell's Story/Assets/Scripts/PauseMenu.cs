using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject settingsMenu;

    private bool _isPaused = false;
    private bool _isInSettings = false;

    private void Awake()
    {
        _isPaused = false;
        _isInSettings = false;

        pauseMenu.SetActive(false);
        settingsMenu.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }
    }

    public void TogglePauseMenu()
    {
        EnableCursor();
        if (!_isPaused && !_isInSettings)
        {
            Time.timeScale = 0.0f;
            _isPaused = true;
        }
        else if (_isPaused && !_isInSettings)
        {
            Time.timeScale = 1.0f;
            DisableCursor();
            _isPaused = false;
        }
        else if (_isInSettings)
        {
            _isInSettings = false;
        }
        pauseMenu.SetActive(_isPaused);
        settingsMenu.SetActive(_isInSettings);
    }

    public void GoToSettings()
    {
        if (!_isInSettings)
        {
            _isInSettings = true;
            settingsMenu.SetActive(true);
            pauseMenu.SetActive(false);
        }
    }

    public void GoToMainMenu() => SceneChangerScript.ChangeScene(0);

    private void EnableCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void DisableCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
