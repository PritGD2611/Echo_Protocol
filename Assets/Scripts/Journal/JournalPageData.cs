using UnityEngine;

[CreateAssetMenu(fileName = "NewJournalPage", menuName = "Journal/Journal Page")]
public class JournalPageData : ScriptableObject
{
    [Header("Page Content")]
    public string pageTitle;

    [TextArea(10, 20)]
    public string pageText;

    public Sprite pageImage;
}