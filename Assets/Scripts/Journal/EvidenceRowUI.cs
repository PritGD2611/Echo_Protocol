using UnityEngine;
using UnityEngine.UI;

public class EvidenceRowUI : MonoBehaviour
{
    public EvidenceType evidenceType;
    public Toggle toggle;

    void Start()
    {
        toggle.SetIsOnWithoutNotify(EvidenceManager.Instance.IsChecked(evidenceType));
        toggle.onValueChanged.AddListener(OnToggleChanged);
        EvidenceManager.Instance.OnEvidenceChanged += RefreshVisual;
    }

    void OnDestroy()
    {
        if (EvidenceManager.Instance != null)
            EvidenceManager.Instance.OnEvidenceChanged -= RefreshVisual;
    }

    void OnToggleChanged(bool isOn)
    {
        EvidenceManager.Instance.ToggleEvidence(evidenceType);
    }

    void RefreshVisual()
    {
        toggle.SetIsOnWithoutNotify(EvidenceManager.Instance.IsChecked(evidenceType));
    }
}