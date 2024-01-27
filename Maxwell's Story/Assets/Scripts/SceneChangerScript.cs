using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneChangerScript
{
    public static AsyncOperation ChangeScene(string sceneName) => SceneManager.LoadSceneAsync(sceneName);
    public static AsyncOperation ChangeScene(int sceneID) => SceneManager.LoadSceneAsync(sceneID);
    public static string GetCurrentSceneName() => SceneManager.GetActiveScene().name;
    public static int GetCurrentSceneID() => SceneManager.GetActiveScene().buildIndex;
}
