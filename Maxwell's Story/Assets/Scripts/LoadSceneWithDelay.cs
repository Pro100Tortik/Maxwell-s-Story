using System.Collections;
using UnityEngine;

public class LoadSceneWithDelay : MonoBehaviour
{
    [SerializeField] private float delay = 12.0f;
    [SerializeField] private string sceneName;

    private void Awake()
    {
        StartCoroutine(WaitForIt());
    }

    private IEnumerator WaitForIt()
    {
        yield return new WaitForSeconds(delay);
        SceneChangerScript.ChangeScene(sceneName);
        yield break;
    }
}
