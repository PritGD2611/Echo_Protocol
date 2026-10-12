using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text;

public class GhostDetailController : MonoBehaviour
{
    public GameObject listViewPanel;
    public GameObject detailViewPanel;

    public TextMeshProUGUI ghostNameText;
    public Image ghostImage;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI requiredEvidenceText;
    public TextMeshProUGUI strengthsText;
    public TextMeshProUGUI weaknessesText;

    public void ShowGhost(GhostData ghost)
    {
        listViewPanel.SetActive(false);
        detailViewPanel.SetActive(true);

        ghostNameText.text = ghost.ghostName;
        descriptionText.text = ghost.description;

        if (ghost.ghostImage != null)
        {
            ghostImage.sprite = ghost.ghostImage;
            ghostImage.enabled = true;
        }
        else ghostImage.enabled = false;

        StringBuilder sb = new StringBuilder();
        foreach (var evidence in ghost.requiredEvidence)
            sb.AppendLine("- " + FormatEvidenceName(evidence));

        requiredEvidenceText.text = sb.ToString();
        strengthsText.text = ghost.strengths;
        weaknessesText.text = ghost.weaknesses;
    }

    public void BackToList()
    {
        detailViewPanel.SetActive(false);
        listViewPanel.SetActive(true);
    }

    string FormatEvidenceName(EvidenceType type)
    {
        switch (type)
        {
            case EvidenceType.EMFLevel5: return "EMF Level 5";
            case EvidenceType.DOTSProjector: return "D.O.T.S Projector";
            case EvidenceType.UltravioletLight: return "Ultraviolet";
            case EvidenceType.FreezingTemperatures: return "Freezing Temperatures";
            case EvidenceType.GhostOrb: return "Ghost Orb";
            case EvidenceType.GhostWriting: return "Ghost Writing";
            case EvidenceType.SpiritBox: return "Spirit Box";
            default: return type.ToString();
        }
    }
}