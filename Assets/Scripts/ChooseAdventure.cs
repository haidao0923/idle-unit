using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ChooseAdventure : MonoBehaviour
{
    Adventure[] adventures;
    Transform slots, menu;
    public int currentPage, maxPage;
    void Awake()
    {
        adventures = GameObject.FindGameObjectWithTag("GameController").GetComponent<AdventureDatabase>().adventures;
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
        for (int j = (currentPage - 1) * 15; i < slots.childCount && adventures[j] != null; i++, j++) {
            Transform currentSlot = slots.GetChild(i);
            Adventure currentAdventure = adventures[j];
            UpdateSlot(currentSlot, currentAdventure, j);
        }
        for ( ; i < slots.childCount; i++) {
            Transform currentSlot = slots.GetChild(i);
            for (int j = 0; j < currentSlot.childCount; j++) {
                currentSlot.GetChild(j).gameObject.SetActive(false);
            }
        }
    }
    void UpdateSlot(Transform slot, Adventure adventure, int index) {
        for (int i = 0; i < slot.childCount; i++) {
            slot.GetChild(i).gameObject.SetActive(true);
        }
        slot.GetChild(0).GetComponent<Image>().sprite = adventure.sprite;
        slot.GetChild(1).GetChild(0).GetComponent<Text>().text = adventure.name;
        slot.GetChild(2).GetChild(0).GetComponent<Text>().text = adventure.currentPoint.ToString();
        if (index == 0 || adventures[index - 1].currentPoint >= 1000) {
            slot.GetChild(3).gameObject.SetActive(false);
            slot.GetComponent<Button>().onClick.RemoveAllListeners();
            slot.GetComponent<Button>().onClick.AddListener(() => OnSlotClick(slot.GetSiblingIndex()));
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

    public void OnSlotClick(int siblingIndex) {
        int adventureIndex = (currentPage - 1) * 15 + siblingIndex;
        GameObject adventureDetails = transform.parent.Find("Adventure Details").gameObject;
        adventureDetails.GetComponent<AdventureDetails>().adventure = adventures[adventureIndex];
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
}