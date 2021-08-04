using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SelectUnit : MonoBehaviour
{
    Player player;
    Transform slots, menu;
    public int formationIndex;
    public int inventoryIndex;
    public int currentPage, maxPage;
    DisplayType displayType;
    List<Unit> displayedUnit = new List<Unit>();
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("GameController").GetComponent<Player>();
        slots = transform.Find("Border/Background/Slots");
        menu = transform.Find("Border/Background/Menu");
    }

    void OnEnable() {
        displayType = DisplayType.ALL;
        currentPage = 1;
        UpdateDisplay();
    }

    void UpdateDisplay() {
        UpdateNavigation();
        int i = 0;
        for (int j = (currentPage - 1) * 20; i < slots.childCount && j < displayedUnit.Count; i++, j++) {
            Transform currentSlot = slots.GetChild(i);
            Unit currentUnit = displayedUnit[j];
            UpdateSlot(currentSlot, currentUnit);
        }
        for ( ; i < slots.childCount; i++) {
            Transform currentSlot = slots.GetChild(i);
            for (int j = 0; j < currentSlot.childCount; j++) {
                currentSlot.GetChild(j).gameObject.SetActive(false);
            }
        }
    }
    void UpdateSlot(Transform slot, Unit unit) {
        slot.GetComponent<Button>().onClick.RemoveAllListeners();
        slot.GetComponent<Button>().onClick.AddListener(() => OnSlotClick(slot.GetSiblingIndex()));
        for (int i = 0; i < slot.childCount; i++) {
            slot.GetChild(i).gameObject.SetActive(true);
        }
        slot.GetChild(0).GetComponent<Image>().sprite = unit.sprite;
        slot.GetChild(1).GetChild(0).GetComponent<Text>().text = "Lv " + unit.level;
        slot.GetChild(2).GetComponent<Image>().color = Unit.GetRarityColor(unit.rarity);
        slot.GetChild(2).GetChild(0).GetComponent<Text>().text = Unit.GetRarityAcronym(unit.rarity);
        slot.GetChild(3).GetChild(0).GetComponent<Image>().sprite = unit.GetElementSprite();
        if (player.IndexInFormation((currentPage - 1) * 20 + slot.GetSiblingIndex()) != -1) {
            slot.GetChild(4).gameObject.SetActive(true);
        } else {
            slot.GetChild(4).gameObject.SetActive(false);
        }
    }

    void UpdateNavigation() {
        displayedUnit = new List<Unit>();
        switch (displayType) {
            case DisplayType.ALL:
                displayedUnit = player.inventory;
                break;
            case DisplayType.COMMON:
                for (int i = 0; i < player.inventory.Count; i++) {
                    if (player.inventory[i].rarity == Rarity.Common) {
                        displayedUnit.Add(player.inventory[i]);
                    }
                }
                break;
            case DisplayType.RARE:
                for (int i = 0; i < player.inventory.Count; i++) {
                    if (player.inventory[i].rarity == Rarity.Rare) {
                        displayedUnit.Add(player.inventory[i]);
                    }
                }
                break;
            case DisplayType.EPIC:
                for (int i = 0; i < player.inventory.Count; i++) {
                    if (player.inventory[i].rarity == Rarity.Epic) {
                        displayedUnit.Add(player.inventory[i]);
                    }
                }
                break;
            case DisplayType.LEGENDARY:
                for (int i = 0; i < player.inventory.Count; i++) {
                    if (player.inventory[i].rarity == Rarity.Legendary) {
                        displayedUnit.Add(player.inventory[i]);
                    }
                }
                break;
        }
        maxPage = (int) Mathf.Ceil(displayedUnit.Count / 20f);
        if (maxPage == 0) {
            maxPage = 1;
        }
        menu.GetChild(0).GetComponent<Text>().text = currentPage + "/" + maxPage;
        menu.GetChild(1).GetComponent<Button>().onClick.RemoveAllListeners();
        menu.GetChild(2).GetComponent<Button>().onClick.RemoveAllListeners();
        menu.GetChild(1).GetComponent<Button>().onClick.AddListener(() => OnArrowClick(0));
        menu.GetChild(2).GetComponent<Button>().onClick.AddListener(() => OnArrowClick(1));
    }

    public void OnSlotClick(int siblingIndex) {
        int inventoryIndex = (currentPage - 1) * 20 + siblingIndex;
        GameObject.FindGameObjectWithTag("GameController").GetComponent<ViewController>().OpenUnitView(formationIndex, inventoryIndex);
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

    enum DisplayType {
        ALL, COMMON, RARE, EPIC, LEGENDARY,
    }
}