using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ChooseAdventure : MonoBehaviour
{
    Transform slots, menu;
    public int currentPage, maxPage;
    void Awake()
    {
        slots = transform.Find("Border/Background/Slots");
        menu = transform.Find("Border/Background/Menu");
    }

    void OnEnable() {
        currentPage = 1;
        UpdateDisplay();
    }

    void UpdateDisplay() {
        UpdateNavigation();
        int i = 0;
        for (int j = (currentPage - 1) * 5; i < 5 && AdventureDatabase.adventures[j] != null; i++, j++) {
            UpdateSlot(slots.GetChild(i), AdventureDatabase.adventures[j], j);
            UpdateSlot(slots.GetChild(i + 5), AdventureDatabase.challengeAdventures[j], j, AdventureMode.CHALLENGE);
            UpdateSlot(slots.GetChild(i + 10), AdventureDatabase.ascendedAdventures[j], j, AdventureMode.ASCENDED);
        }
        for ( ; i < 5; i++) {
            Transform currentSlot = slots.GetChild(i);
            for (int j = 0; j < currentSlot.childCount; j++) {
                currentSlot.GetChild(j).gameObject.SetActive(false);
            }
            currentSlot = slots.GetChild(i + 5);
            for (int j = 0; j < currentSlot.childCount; j++) {
                currentSlot.GetChild(j).gameObject.SetActive(false);
            }
            currentSlot = slots.GetChild(i + 10);
            for (int j = 0; j < currentSlot.childCount; j++) {
                currentSlot.GetChild(j).gameObject.SetActive(false);
            }
        }
    }
    void UpdateSlot(Transform slot, Adventure adventure, int index, AdventureMode adventureMode = AdventureMode.NORMAL) {
        for (int i = 0; i < slot.childCount; i++) {
            slot.GetChild(i).gameObject.SetActive(true);
        }
        slot.GetChild(0).GetComponent<Image>().sprite = adventure.sprite;
        slot.GetChild(1).GetChild(0).GetComponent<Text>().text = adventure.name;
        slot.GetChild(2).GetChild(0).GetComponent<Text>().text = adventure.currentPoint.ToString();
        if ((adventureMode == AdventureMode.NORMAL && Adventure.clearedAdventures >= index)
            || (adventureMode == AdventureMode.CHALLENGE && Adventure.clearedAdventures > index)
            || (adventureMode == AdventureMode.ASCENDED && Adventure.clearedAdventures > index)) {
            slot.GetChild(3).gameObject.SetActive(false);
            slot.GetComponent<Button>().onClick.RemoveAllListeners();
            slot.GetComponent<Button>().onClick.AddListener(() => OnSlotClick(index, adventureMode));
        } else {
            slot.GetChild(3).gameObject.SetActive(true);
            slot.GetComponent<Button>().onClick.RemoveAllListeners();
        }
    }

    void UpdateNavigation() {
        maxPage = 1;
        menu.GetChild(0).GetComponent<Text>().text = currentPage + "/" + maxPage;
        menu.GetChild(1).GetComponent<Button>().onClick.RemoveAllListeners();
        menu.GetChild(2).GetComponent<Button>().onClick.RemoveAllListeners();
        menu.GetChild(1).GetComponent<Button>().onClick.AddListener(() => OnArrowClick(0));
        menu.GetChild(2).GetComponent<Button>().onClick.AddListener(() => OnArrowClick(1));
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

        GameObject adventureDetails = transform.parent.Find("Adventure Details").gameObject;
        adventureDetails.GetComponent<AdventureDetails>().adventure = tempAdventure;
        adventureDetails.SetActive(true);
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

    enum AdventureMode {
        NORMAL, CHALLENGE, ASCENDED
    }
}