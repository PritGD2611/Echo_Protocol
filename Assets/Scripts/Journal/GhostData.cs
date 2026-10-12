using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewGhost", menuName = "Journal/Ghost Data")]
public class GhostData : ScriptableObject
{
    public string ghostName;

    [TextArea(5, 10)]
    public string description;

    public Sprite ghostImage;

    public List<EvidenceType> requiredEvidence = new List<EvidenceType>();

    [Header("Optional Extra Info")]
    [TextArea(2, 5)] public string strengths;
    [TextArea(2, 5)] public string weaknesses;
}