using UnityEngine;
using System.Collections.Generic;

public class GhostListController : MonoBehaviour
{
    public enum ListMode { DeductionList, SelectionList }

    [Header("Settings")]
    public ListMode mode;
    public List<GhostData> allGhosts = new List<GhostData>();
    public Transform listContainer;
    public GameObject ghostEntryPrefab;

    [Header("Selection Mode Only")]
    public GhostDetailController ghostDetailController;

    private List<GhostEntryUI> spawnedEntries = new List<GhostEntryUI>();
    private GhostData selectedGhost;

    void OnEnable()
    {
        PopulateList();

        if (mode == ListMode.DeductionList && EvidenceManager.Instance != null)
        {
            EvidenceManager.Instance.OnEvidenceChanged += RefreshDeductionVisuals;
            RefreshDeductionVisuals();
        }
    }

    void OnDisable()
    {
        if (EvidenceManager.Instance != null)
            EvidenceManager.Instance.OnEvidenceChanged -= RefreshDeductionVisuals;
    }

    void PopulateList()
    {
        foreach (Transform child in listContainer) Destroy(child.gameObject);
        spawnedEntries.Clear();

        foreach (GhostData ghost in allGhosts)
        {
            GameObject entryObj = Instantiate(ghostEntryPrefab, listContainer);
            GhostEntryUI entry = entryObj.GetComponent<GhostEntryUI>();

            if (mode == ListMode.DeductionList)
                entry.Setup(ghost, OnGhostClicked_Deduction);
            else
                entry.Setup(ghost, OnGhostClicked_Selection);

            spawnedEntries.Add(entry);
        }
    }

    void RefreshDeductionVisuals()
    {
        HashSet<EvidenceType> checkedEvidence = EvidenceManager.Instance.GetCheckedEvidence();

        foreach (GhostEntryUI entry in spawnedEntries)
        {
            GhostData ghost = entry.GetGhostData();
            bool matches = GhostMatchesEvidence(ghost, checkedEvidence);
            entry.SetCrossedOut(!matches && checkedEvidence.Count > 0);
        }
    }

    bool GhostMatchesEvidence(GhostData ghost, HashSet<EvidenceType> checkedEvidence)
    {
        foreach (EvidenceType evidence in checkedEvidence)
        {
            if (!ghost.requiredEvidence.Contains(evidence)) return false;
        }
        return true;
    }

    void OnGhostClicked_Deduction(GhostData ghost)
    {
        selectedGhost = (selectedGhost == ghost) ? null : ghost;

        foreach (GhostEntryUI entry in spawnedEntries)
            entry.SetSelected(entry.GetGhostData() == selectedGhost);
    }

    void OnGhostClicked_Selection(GhostData ghost)
    {
        if (ghostDetailController != null)
            ghostDetailController.ShowGhost(ghost);
    }
}