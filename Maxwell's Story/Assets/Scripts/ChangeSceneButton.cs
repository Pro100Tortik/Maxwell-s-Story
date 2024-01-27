using UnityEngine;

public class ChangeSceneButton : MonoBehaviour
{
    public void ChangeScene(string sceneName)
    {
        Fade(SceneChangerScript.ChangeScene(sceneName));
    }

    public void ChangeScene(int sceneID)
    {
        Fade(SceneChangerScript.ChangeScene(sceneID));
    }

    private void Fade(AsyncOperation operation)
    {
        Debug.Log("Hello, how are you, I am under de wotor, Help me");
    }
}
