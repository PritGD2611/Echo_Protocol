using UnityEngine;
using System.Collections.Generic;

public class JournalManager : MonoBehaviour
{
    [Header("Journal Root")]
    public GameObject journalPanel;

    [Header("Tabs (0=Home,1=Items,2=Overview,3=Media,4=Evidence,5=Ghosts)")]
    public List<GameObject> tabPanels = new List<GameObject>();

    private bool isOpen = false;
    private int currentTabIndex = 0;

    void Start()
    {
        journalPanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J)) ToggleJournal();
        if (isOpen && Input.GetKeyDown(KeyCode.Escape)) CloseJournal();
    }

    public void ToggleJournal()
    {
        if (isOpen) CloseJournal();
        else OpenJournal();
    }

    public void OpenJournal()
    {
        isOpen = true;
        journalPanel.SetActive(true);
        OpenTab(0);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void CloseJournal()
    {
        isOpen = false;
        journalPanel.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void OpenTab(int index)
    {
        currentTabIndex = index;
        for (int i = 0; i < tabPanels.Count; i++)
            tabPanels[i].SetActive(i == index);
    }

    public void NextTab()
    {
        OpenTab((currentTabIndex + 1) % tabPanels.Count);
    }

    public void PreviousTab()
    {
        OpenTab((currentTabIndex - 1 + tabPanels.Count) % tabPanels.Count);
    }
}