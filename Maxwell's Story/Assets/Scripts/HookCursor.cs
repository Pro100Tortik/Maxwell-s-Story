using UnityEngine;
using UnityEngine.UI;

public class HookCursor : MonoBehaviour
{
    [SerializeField] private GrapplingHook hook;
    [SerializeField] private GameObject hookCursor;
    [SerializeField] private RawImage cursor;
    [SerializeField] private FPSInteractor interactor;
    private Vector2 _startCursorSize;

    private void Awake()
    {
        _startCursorSize = cursor.rectTransform.sizeDelta;
    }

    private void OnEnable() => hook.CanHook += UpdateHook;

    private void OnDisable() => hook.CanHook -= UpdateHook;

    private void Update()
    {
        if (interactor.CanInteract)
        {
            cursor.rectTransform.sizeDelta = _startCursorSize + new Vector2(20, 20);
        }
        else
        {
            cursor.rectTransform.sizeDelta = _startCursorSize;
        }
    }

    private void UpdateHook() => hookCursor.SetActive(true);

    private void FixedUpdate() => hookCursor.SetActive(false);
}
