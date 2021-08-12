using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ChooseAdventure : MonoBehaviour
{
    [SerializeField] GameObject adventureDetail;
    [SerializeField] Transform[] slots, menu;
    [SerializeField] Image[] images;
    [SerializeField] Text[] nameTexts, pointTexts;
    [SerializeField] GameObject[] lockPanels;
    [SerializeField] Button[] selectAdventureButtons, helpButtons;
    public int currentPage, maxPage;

    void OnEnable() {
        currentPage = 1;
        UpdateDisplay();
    }

    void UpdateDisplay() {
        UpdateNavigation();
        foreach (Button button in helpButtons) {
            button.onClick.RemoveAllListeners();
        }
        UpdateSlot(AdventureMode.NORMAL);
        UpdateSlot(AdventureMode.CHALLENGE);
        UpdateSlot(AdventureMode.ASCENDED);
    }
    void UpdateSlot(AdventureMode adventureMode) {
        int i = 0;
        int adventureIndex = (currentPage - 1) * 5;
        switch (adventureMode) {
            case AdventureMode.NORMAL:
                i = 0;
                break;
            case AdventureMode.CHALLENGE:
                i = 5;
                break;
            case AdventureMode.ASCENDED:
                i = 10;
                break;
        }
        int j = 0;
        for ( ; j < 5 && AdventureDatabase.adventures[adventureIndex] != null; i++, adventureIndex++, j++) {
            foreach (Transform child in slots[i]) {
                child.gameObject.SetActive(true);
            }
            Adventure adventure = AdventureDatabase.adventures[adventureIndex];
            images[i].sprite = adventure.sprite;
            nameTexts[i].text = adventure.name;
            pointTexts[i].text = adventure.currentPoint.ToString();
            int tempAdventureIndex = adventureIndex;
            helpButtons[i].onClick.AddListener(() => OnHelpClick(adventureMode, tempAdventureIndex));
            if ((adventureMode == AdventureMode.NORMAL && Adventure.clearedAdventures >= adventureIndex)
                || (adventureMode == AdventureMode.CHALLENGE && Adventure.clearedAdventures > adventureIndex)
                || (adventureMode == AdventureMode.ASCENDED && Adventure.clearedAdventures > adventureIndex)) {
                lockPanels[i].SetActive(false);
                selectAdventureButtons[i].onClick.RemoveAllListeners();
                selectAdventureButtons[i].onClick.AddListener(() => OnSlotClick(tempAdventureIndex, adventureMode));
            } else {
                lockPanels[i].SetActive(true);
            }
        }
        for ( ; j < 5; i++, j++) {
            foreach (Transform child in slots[i]) {
                child.gameObject.SetActive(false);
            }
        }
    }

    void UpdateNavigation() {
        maxPage = 8;
        menu[0].GetComponent<Text>().text = currentPage + "/" + maxPage;
        menu[1].GetComponent<Button>().onClick.RemoveAllListeners();
        menu[2].GetComponent<Button>().onClick.RemoveAllListeners();
        menu[1].GetComponent<Button>().onClick.AddListener(() => OnArrowClick(0));
        menu[2].GetComponent<Button>().onClick.AddListener(() => OnArrowClick(1));
    }

    void OnSlotClick(int adventureIndex, AdventureMode adventureMode) {
        Adventure tempAdventure = null;
        switch (adventureMode) {
            case AdventureMode.NORMAL:
                tempAdventure = AdventureDatabase.adventures[adventureIndex];
                break;
            case AdventureMode.CHALLENGE:
                tempAdventure = AdventureDatabase.challengeAdventures[adventureIndex];
                break;
            case AdventureMode.ASCENDED:
                tempAdventure = AdventureDatabase.ascendedAdventures[adventureIndex];
                break;
        }
        OpenAdventureDetail(tempAdventure);
    }
    void OnHelpClick(AdventureMode adventureMode, int adventureIndex) {
        OnSlotClick(adventureIndex, adventureMode);
        switch (adventureMode) {
            case AdventureMode.NORMAL:
                OnShowLoreClick(adventureIndex);
                break;
            case AdventureMode.CHALLENGE:
                OnViewChallengeClick(adventureIndex);
                break;
            case AdventureMode.ASCENDED:
                OnViewAscendedDetailClick(adventureIndex);
                break;
        }
    }
    void OnViewChallengeClick(int adventureIndex) {
        adventureDetail.GetComponent<AdventureDetails>().DisplayRequirement();
    }
    void OnShowLoreClick(int adventureIndex) {
        adventureDetail.GetComponent<AdventureDetails>().DisplayLore();
    }
    void OnViewAscendedDetailClick(int adventureIndex) {

    }
    public void OnArrowClick(int direction) {
        if (direction == 0 && currentPage > 1) { // 0 = Up
            currentPage -= 1;
        } else if (direction == 1 && currentPage < maxPage) { // 1 = Down
            currentPage += 1;
        } else {
            return;
        }
        UpdateDisplay();
    }
    public void OpenAdventureDetail(Adventure adventure) {
        adventureDetail.GetComponent<AdventureDetails>().adventure = adventure;
        adventureDetail.SetActive(true);
    }
    enum AdventureMode {
        NORMAL, CHALLENGE, ASCENDED
    }
}