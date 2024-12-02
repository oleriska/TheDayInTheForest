using System;
using UnityEngine;

public class ObjectiveController : MonoBehaviour
{
    [SerializeField]
    private TMPro.TextMeshProUGUI ObjectivePanel;

    public static ObjectiveController Instance;
    public event Action<string> OnObjectiveChange;

    public void OnObjectiveChangeTrigger(string objective)
    {
        OnObjectiveChange?.Invoke(objective);
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            OnObjectiveChange += ChangeObjective;
        }
        else if (Instance != this)
        {
            Destroy(this);
        }
    }

    private void ChangeObjective(string objective)
    {
        ObjectivePanel.text = objective;
    }
}
