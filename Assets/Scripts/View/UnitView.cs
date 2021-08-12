using UnityEngine;
using UnityEngine.UI;

public class UnitView : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] Transform view;
    [SerializeField] Transform ascensionStar;
    [SerializeField] Text levelText, rarityText, nameText, expText;
    [SerializeField] Image image, rarityBackground, elementSprite;
    [SerializeField] GameObject[] skillGameObjects;
    [SerializeField] Text[] skillNames, skillDescriptions, statTexts;
    [SerializeField] Button[] buttons;
    [SerializeField] Text[] buttonTexts;
    [SerializeField] Button selectHeroButton;
    [SerializeField] GameObject selectUnitPanel, selectHeroPanel, boostPanel, ascendPanel;
    int formationIndex, inventoryIndex, selectHeroIndex;
    Unit unit;
    private Transform slots;
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("GameController").GetComponent<Player>();
        view = transform.Find("Border/Background");
        slots = view.GetChild(9).Find("Border/Background/Slots");
    }
    public void OpenPanel(int formationIndex = -1, int inventoryIndex = -1, int selectHeroIndex = -1) {
        this.formationIndex = formationIndex;
        this.inventoryIndex = inventoryIndex;
        this.selectHeroIndex = selectHeroIndex;
        if (selectHeroIndex > 0) {
            unit = UnitDatabase.GetUnitById(selectHeroIndex);
        } else {
            unit = Player.inventory[inventoryIndex];
        }
        UpdateDisplay();
    }
    void UpdateDisplay()
    {
        image.sprite = unit.sprite;
        levelText.text = "Lv\n" + unit.level + "/" + unit.levelCap;
        rarityBackground.color = Unit.GetRarityColor(unit.rarity);
        rarityText.text = Unit.GetRarityAcronym(unit.rarity);
        elementSprite.sprite = unit.GetElementSprite();
        nameText.text = unit.name;
        expText.text = "XP: " + unit.exp + "/" + unit.maxExp;
        if (unit.firstSkill != null) {
            skillGameObjects[0].SetActive(true);
            skillNames[0].text = unit.firstSkill.name;
            skillDescriptions[0].text = unit.firstSkill.description;
        } else {
            skillGameObjects[0].SetActive(false);
        }
        if (unit.secondSkill != null) {
            skillGameObjects[1].SetActive(true);
            skillNames[1].text = unit.secondSkill.name;
            skillDescriptions[1].text = unit.secondSkill.description;
        } else {
            skillGameObjects[1].SetActive(false);
        }
        if (unit.thirdSkill != null) {
            skillGameObjects[2].SetActive(true);
            skillNames[2].text = unit.thirdSkill.name;
            skillDescriptions[2].text = unit.thirdSkill.description;
        } else {
            skillGameObjects[2].SetActive(false);
        }
        statTexts[0].text = "Health: " + unit.stat.health;
        statTexts[1].text = "Strength: " + unit.stat.strength;
        statTexts[2].text = "Magic: " + unit.stat.magic;
        statTexts[3].text = "Defense: " + unit.stat.defense;
        statTexts[4].text = "Agility: " + unit.stat.agility;

        boostPanel.SetActive(false);

        if (selectHeroIndex > 0) {
            foreach (Button button in buttons) {
                button.gameObject.SetActive(false);
            }
            selectHeroButton.gameObject.SetActive(true);
            selectHeroButton.onClick.RemoveAllListeners();
            selectHeroButton.onClick.AddListener(() => SelectHero(selectHeroIndex));
            return;
        } else {
            foreach (Button button in buttons) {
                button.gameObject.SetActive(true);
            }
            selectHeroButton.gameObject.SetActive(false);
        }

        buttons[0].onClick.RemoveAllListeners();
        if (inventoryIndex == Player.formation[formationIndex]) {
            buttonTexts[0].text = "Remove";
            buttons[0].onClick.AddListener(() => Remove());
        } else {
            buttonTexts[0].text = "Assign";
            buttons[0].onClick.AddListener(() => Assign());
        }
        buttons[1].onClick.RemoveAllListeners();
        buttons[1].onClick.AddListener(() => OpenBoost());
    }
    void SelectHero(int selectHeroIndex) {
        player.AddToInventory(selectHeroIndex, 4, 4, 11, 11);
        selectHeroPanel.SetActive(false);
        gameObject.SetActive(false);
    }

    public void Assign() {
        for (int i = 0; i < Player.formation.Length; i++) {
            if (Player.formation[i] == inventoryIndex) {
                player.RemoveFromFormation(i);
                break;
            }
        }
        player.AddToFormation(formationIndex, inventoryIndex);
        selectUnitPanel.SetActive(false);
        gameObject.SetActive(false);
    }
    public void Remove() {
        player.RemoveFromFormation(formationIndex);
        selectUnitPanel.SetActive(false);
        gameObject.SetActive(false);
    }

    public void OpenBoost() {
        view.GetChild(9).gameObject.SetActive(true);

        int rarityIndex = 0;
        switch (unit.rarity) {
            case Rarity.Common:
                rarityIndex = 0;
                break;
            case Rarity.Rare:
                rarityIndex = 1;
                break;
            case Rarity.Epic:
                rarityIndex = 2;
                break;
            case Rarity.Legendary:
                rarityIndex = 3;
                break;
        }
        slots.GetChild(0).GetChild(0).GetComponent<Button>().onClick.RemoveAllListeners();
        slots.GetChild(0).GetChild(0).GetComponent<Button>().onClick.AddListener(() => UseCap(rarityIndex));
        slots.GetChild(0).GetChild(0).GetComponent<Image>().sprite = ConsumableDatabase.consumables["Cap"][rarityIndex].sprite;
        slots.GetChild(0).GetChild(1).GetChild(0).GetComponent<Text>().text = ConsumableDatabase.consumables["Cap"][rarityIndex].name;
        slots.GetChild(0).GetChild(2).GetChild(0).GetComponent<Text>().text = "+" + ConsumableDatabase.consumables["Cap"][rarityIndex].effect + " Max Level";

        for (int i = 0; i < 3; i++) {
            slots.GetChild(i + 1).GetChild(0).GetComponent<Button>().onClick.RemoveAllListeners();
            int potionIndex = i;
            slots.GetChild(i + 1).GetChild(0).GetComponent<Button>().onClick.AddListener(() => UsePotion(potionIndex));
            slots.GetChild(i + 1).GetChild(0).GetComponent<Image>().sprite = ConsumableDatabase.consumables["Potion"][i].sprite;
            slots.GetChild(i + 1).GetChild(1).GetChild(0).GetComponent<Text>().text = ConsumableDatabase.consumables["Potion"][i].name;
            slots.GetChild(i + 1).GetChild(2).GetChild(0).GetComponent<Text>().text = "+" + ConsumableDatabase.consumables["Potion"][i].effect + " XP";
        }

        UpdateCapDisplay(rarityIndex);
        UpdatePotionDisplay();
    }

    void UpdateCapDisplay(int capIndex) {
        slots.GetChild(0).GetChild(3).GetChild(0).GetComponent<Text>().text = "Quantity: " + ConsumableDatabase.consumables["Cap"][capIndex].quantity;
        if (unit.levelCap >= 100 * unit.ascensionLevel) {
            slots.GetChild(0).GetChild(2).GetChild(0).GetComponent<Text>().text = "Max Reached!";
            slots.GetChild(0).GetChild(0).GetComponent<Button>().interactable = false;
        } else if (ConsumableDatabase.consumables["Cap"][capIndex].quantity <= 0) {
            slots.GetChild(0).GetChild(2).GetChild(0).GetComponent<Text>().text = "Out of Stock";
            slots.GetChild(0).GetChild(0).GetComponent<Button>().interactable = false;
        } else {
            slots.GetChild(0).GetChild(2).GetChild(0).GetComponent<Text>().text = "+" + ConsumableDatabase.consumables["Cap"][capIndex].effect + " Max Level";
            slots.GetChild(0).GetChild(0).GetComponent<Button>().interactable = true;
        }
    }
    void UpdatePotionDisplay() {
        for (int i = 0; i < 3; i++) {
            slots.GetChild(i + 1).GetChild(3).GetChild(0).GetComponent<Text>().text = "Quantity: " + ConsumableDatabase.consumables["Potion"][i].quantity;
            if (unit.level >= unit.levelCap) {
                slots.GetChild(i + 1).GetChild(2).GetChild(0).GetComponent<Text>().text = "Max Level Reached!";
                slots.GetChild(i + 1).GetChild(0).GetComponent<Button>().interactable = false;
            } else if (ConsumableDatabase.consumables["Potion"][i].quantity <= 0) {
                slots.GetChild(i + 1).GetChild(2).GetChild(0).GetComponent<Text>().text = "Out of Stock";
                slots.GetChild(i + 1).GetChild(0).GetComponent<Button>().interactable = false;
            } else {
                slots.GetChild(i + 1).GetChild(2).GetChild(0).GetComponent<Text>().text = "+" + ConsumableDatabase.consumables["Potion"][i].effect + " XP";
                slots.GetChild(i + 1).GetChild(0).GetComponent<Button>().interactable = true;
            }
        }
    }

    void UseCap(int capIndex) {
        unit.capsAbsorbed += 1;
        unit.GainExp(0);
        ConsumableDatabase.consumables["Cap"][capIndex].quantity -= 1;
        UpdateDisplay();
        player.UpdateFormationDisplay(formationIndex);
        SaveAndLoad.data.SaveInventory();
    }

    void UsePotion(int potionIndex) {
        unit.GainExp(ConsumableDatabase.consumables["Potion"][potionIndex].effect);
        ConsumableDatabase.consumables["Potion"][potionIndex].quantity -= 1;
        UpdateDisplay();
        player.UpdateFormationDisplay(formationIndex);
        SaveAndLoad.data.SaveInventory();
    }
}
