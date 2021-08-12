using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SelectUnit : MonoBehaviour
{
    [SerializeField] UnitView unitViewScript;
    [SerializeField] Text pageNumberText;
    [SerializeField] Button[] navigationButton;
    [SerializeField] Transform[] slots, stars;
    [SerializeField] Button[] slotButtons;
    [SerializeField] Image[] images, rarityBackgrounds, elementSprite;
    [SerializeField] Text[] levelText, rarityText;
    [SerializeField] GameObject[] equippedGameObject;
    int formationIndex;
    int currentPage, maxPage;
    DisplayType displayType;
    List<Unit> displayedUnit = new List<Unit>();

    public void OpenPanel(int formationIndex) {
        gameObject.SetActive(true);
        this.formationIndex = formationIndex;
        displayType = DisplayType.ALL;
        currentPage = 1;
        UpdateDisplay();
    }
    void UpdateDisplay() {
        UpdateNavigation();
        int i = 0;
        for (int j = (currentPage - 1) * 20; i < slots.Length && j < displayedUnit.Count; i++, j++) {
            Unit currentUnit = displayedUnit[j];
            UpdateSlot(i, currentUnit);
        }
        for ( ; i < slots.Length; i++) {
            for (int j = 0; j < slots[i].childCount; j++) {
                slots[i].GetChild(j).gameObject.SetActive(false);
            }
        }
    }
    void UpdateSlot(int index, Unit unit) {
        slotButtons[index].onClick.RemoveAllListeners();
        slotButtons[index].onClick.AddListener(() => OnSlotClick(index));
        for (int i = 0; i < slots[index].childCount; i++) {
            slots[index].GetChild(i).gameObject.SetActive(true);
        }
        images[index].sprite = unit.sprite;
        levelText[index].text = "Lv " + unit.level;
        rarityBackgrounds[index].color = Unit.GetRarityColor(unit.rarity);
        rarityText[index].text = Unit.GetRarityAcronym(unit.rarity);
        elementSprite[index].sprite = unit.GetElementSprite();
        for (int i = 0; i < 5; i++) {
            if (unit.ascensionLevel > i) {
                stars[index].GetChild(i).gameObject.SetActive(true);
            } else {
                stars[index].GetChild(i).gameObject.SetActive(false);
            }
        }
        if (Player.IndexInFormation((currentPage - 1) * 20 + index) != -1) {
            equippedGameObject[index].SetActive(true);
        } else {
            equippedGameObject[index].SetActive(false);
        }
    }

    void UpdateNavigation() {
        displayedUnit = new List<Unit>();
        switch (displayType) {
            case DisplayType.ALL:
                displayedUnit = Player.inventory;
                break;
            case DisplayType.COMMON:
                for (int i = 0; i < Player.inventory.Count; i++) {
                    if (Player.inventory[i].rarity == Rarity.Common) {
                        displayedUnit.Add(Player.inventory[i]);
                    }
                }
                break;
            case DisplayType.RARE:
                for (int i = 0; i < Player.inventory.Count; i++) {
                    if (Player.inventory[i].rarity == Rarity.Rare) {
                        displayedUnit.Add(Player.inventory[i]);
                    }
                }
                break;
            case DisplayType.EPIC:
                for (int i = 0; i < Player.inventory.Count; i++) {
                    if (Player.inventory[i].rarity == Rarity.Epic) {
                        displayedUnit.Add(Player.inventory[i]);
                    }
                }
                break;
            case DisplayType.LEGENDARY:
                for (int i = 0; i < Player.inventory.Count; i++) {
                    if (Player.inventory[i].rarity == Rarity.Legendary) {
                        displayedUnit.Add(Player.inventory[i]);
                    }
                }
                break;
        }
        maxPage = (int) Mathf.Ceil(displayedUnit.Count / 20f);
        if (maxPage == 0) {
            maxPage = 1;
        }
        pageNumberText.text = currentPage + "/" + maxPage;
        navigationButton[0].onClick.RemoveAllListeners();
        navigationButton[1].onClick.RemoveAllListeners();
        navigationButton[0].onClick.AddListener(() => OnArrowClick(0));
        navigationButton[1].onClick.AddListener(() => OnArrowClick(1));
    }

    public void OnSlotClick(int siblingIndex) {
        int inventoryIndex = (currentPage - 1) * 20 + siblingIndex;
        unitViewScript.OpenPanel(formationIndex, inventoryIndex);
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