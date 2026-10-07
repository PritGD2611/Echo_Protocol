using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class JournalManager : MonoBehaviour
{
    [Header("Journal Settings")]
    public KeyCode toggleKey = KeyCode.J;
    public List<JournalPageData> pages = new List<JournalPageData>();

    [Header("UI References")]
    public GameObject journalPanel;
    public Image pageImage;
    public TextMeshProUGUI pageTitleText;
    public TextMeshProUGUI pageBodyText;
    public TextMeshProUGUI pageNumberText;

    [Header("Optional")]
    public AudioSource pageTurnSound;

    private int currentPageIndex = 0;
    private bool isOpen = false;

    void Start()
    {
        journalPanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            ToggleJournal();
        }

        if (isOpen)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow)) NextPage();
            if (Input.GetKeyDown(KeyCode.LeftArrow)) PreviousPage();
            if (Input.GetKeyDown(KeyCode.Escape)) CloseJournal();
        }
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
        currentPageIndex = 0;
        UpdatePageDisplay();

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

    public void NextPage()
    {
        if (currentPageIndex < pages.Count - 1)
        {
            currentPageIndex++;
            UpdatePageDisplay();
            PlayPageSound();
        }
    }

    public void PreviousPage()
    {
        if (currentPageIndex > 0)
        {
            currentPageIndex--;
            UpdatePageDisplay();
            PlayPageSound();
        }
    }

    void UpdatePageDisplay()
    {
        if (pages.Count == 0) return;

        JournalPageData currentPage = pages[currentPageIndex];

        pageTitleText.text = currentPage.pageTitle;
        pageBodyText.text = currentPage.pageText;

        if (currentPage.pageImage != null)
        {
            pageImage.sprite = currentPage.pageImage;
            pageImage.enabled = true;
        }
        else
        {
            pageImage.enabled = false;
        }

        pageNumberText.text = (currentPageIndex + 1) + " / " + pages.Count;
    }

    void PlayPageSound()
    {
        if (pageTurnSound != null)
        {
            pageTurnSound.Play();
        }
    }
}