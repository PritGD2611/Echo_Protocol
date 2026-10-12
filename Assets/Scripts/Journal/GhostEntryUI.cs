using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class GhostEntryUI : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public GameObject strikethroughLine;
    public GameObject circleHighlight;
    public Button button;

    private GhostData ghostData;
    private Action<GhostData> onClickCallback;

    public void Setup(GhostData data, Action<GhostData> callback)
    {
        ghostData = data;
        onClickCallback = callback;
        nameText.text = data.ghostName;

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => onClickCallback?.Invoke(ghostData));

        SetCrossedOut(false);
        SetSelected(false);
    }

    public void SetCrossedOut(bool isCrossedOut)
    {
        if (strikethroughLine != null) strikethroughLine.SetActive(isCrossedOut);
    }

    public void SetSelected(bool isSelected)
    {
        if (circleHighlight != null) circleHighlight.SetActive(isSelected);
    }

    public GhostData GetGhostData() => ghostData;
}