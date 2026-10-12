using System;
using System.Collections.Generic;
using UnityEngine;

public class EvidenceManager : MonoBehaviour
{
    public static EvidenceManager Instance;

    private HashSet<EvidenceType> checkedEvidence = new HashSet<EvidenceType>();

    public event Action OnEvidenceChanged;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public bool IsChecked(EvidenceType type) => checkedEvidence.Contains(type);

    public void ToggleEvidence(EvidenceType type)
    {
        if (checkedEvidence.Contains(type))
            checkedEvidence.Remove(type);
        else
            checkedEvidence.Add(type);

        OnEvidenceChanged?.Invoke();
    }

    public HashSet<EvidenceType> GetCheckedEvidence() => checkedEvidence;

    public void ResetEvidence()
    {
        checkedEvidence.Clear();
        OnEvidenceChanged?.Invoke();
    }
}