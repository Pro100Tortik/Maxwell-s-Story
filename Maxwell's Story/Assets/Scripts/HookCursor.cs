using UnityEngine;

public class HookCursor : MonoBehaviour
{
    [SerializeField] private GrapplingHook hook;
    [SerializeField] private GameObject hookCursor;

    private void OnEnable() => hook.CanHook += UpdateHook;

    private void OnDisable() => hook.CanHook -= UpdateHook;

    private void UpdateHook() => hookCursor.SetActive(true);

    private void FixedUpdate() => hookCursor.SetActive(false);
}
