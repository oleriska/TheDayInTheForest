using StarterAssets;
using System.Collections;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogSystem : MonoBehaviour
{
    public static DialogSystem Instance;

    [SerializeField]
    private GameObject DialogPrefab;
    [SerializeField]
    private GameObject BlackPanel;
    [SerializeField]
    private PlayerInput PlayerInput;

    private InputAction _skipDialog;
    private GameObject _actObject;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        _skipDialog = PlayerInput.actions.FindAction("SkipDialog");

        if (_skipDialog == null)
            Debug.Log("Couldn't find action");

        _skipDialog.performed += SkipDialog;
    }

    private IEnumerator DisplayDialogCoroutine(Dialog dialog, float duration, GameObject ActivateObject = null)
    {
        _actObject = ActivateObject;
        BlackPanel.SetActive(true);
        var dialogUI = Instantiate(DialogPrefab);
        dialogUI.transform.SetParent(BlackPanel.transform, false);
        dialogUI.GetComponent<DisplayDialog>().SetDialog(dialog);
        yield return new WaitForSeconds(duration);

        if (dialog != null)
            Destroy(dialogUI);

        if (_actObject != null)
            _actObject.SetActive(true);

        BlackPanel.SetActive(false);
    }

    public static void ShowDialog(Dialog dialog, float duration, GameObject ActivateObject = null)
    {

        if (Instance != null)
        {
            Instance.StartCoroutine(Instance.DisplayDialogCoroutine(dialog, duration, ActivateObject));
        }
    }

    public void SkipDialog(InputAction.CallbackContext context)
    {
        if(_actObject != null)
            _actObject.SetActive(true);

        BlackPanel.SetActive(false);
        
    }
}
