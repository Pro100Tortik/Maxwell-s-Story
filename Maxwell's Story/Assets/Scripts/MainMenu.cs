using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject settingsMenu;
    private bool _isSettingsEnabled = false;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Time.timeScale = 1f;

        mainMenu.SetActive(true);
        settingsMenu.SetActive(false);
        _isSettingsEnabled = false;
    }

    public void ToggleSettings()
    {
        _isSettingsEnabled = !_isSettingsEnabled;
        mainMenu.SetActive(!_isSettingsEnabled);
        settingsMenu.SetActive(_isSettingsEnabled);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif

        Application.Quit();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_isSettingsEnabled)
            {
                ToggleSettings();
            }
        }
    }
}
