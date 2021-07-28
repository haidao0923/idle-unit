using UnityEngine;
using UnityEngine.UI;

public class UnitView : MonoBehaviour
{
    Player player;
    Transform view;
    Transform slots;
    public int formationIndex;
    public int inventoryIndex;
    public int selectHeroIndex = -1;
    Unit unit;
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("GameController").GetComponent<Player>();
        view = transform.Find("Border/Background");
        slots = view.GetChild(9).Find("Border/Background/Slots");
    }

    void OnEnable() {
        if (selectHeroIndex > 0) {
            unit = UnitDatabase.GetUnitById(selectHeroIndex);
        } else {
            unit = player.inventory[inventoryIndex];
        }
        UpdateDisplay();
    }
    void UpdateDisplay()
    {
        view.GetChild(0).GetComponent<Image>().sprite = unit.sprite;
        view.GetChild(1).GetChild(0).GetComponent<Text>().text = "Lv\n" + unit.level + "/" + unit.levelCap;
        view.GetChild(2).GetComponent<Image>().color = Unit.GetRarityColor(unit.rarity);
        view.GetChild(2).GetChild(0).GetComponent<Text>().text = Unit.GetRarityAcronym(unit.rarity);
        view.GetChild(3).GetChild(0).GetComponent<Image>().sprite = unit.GetElementSprite();
        view.GetChild(4).GetComponent<Text>().text = unit.name;
        view.GetChild(5).GetChild(0).GetComponent<Text>().text = "XP: " + unit.exp + "/" + unit.maxExp;
        if (unit.firstSkill != null) {
            view.GetChild(6).GetChild(1).gameObject.SetActive(true);
            view.GetChild(6).GetChild(2).gameObject.SetActive(true);
            view.GetChild(6).GetChild(1).GetChild(0).GetComponent<Text>().text = unit.firstSkill.name;
            view.GetChild(6).GetChild(2).GetChild(0).GetComponent<Text>().text = unit.firstSkill.description;
        } else {
            view.GetChild(6).GetChild(1).gameObject.SetActive(false);
            view.GetChild(6).GetChild(2).gameObject.SetActive(false);
        }
        if (unit.secondSkill != null) {
            view.GetChild(6).GetChild(3).gameObject.SetActive(true);
            view.GetChild(6).GetChild(4).gameObject.SetActive(true);
            view.GetChild(6).GetChild(3).GetChild(0).GetComponent<Text>().text = unit.secondSkill.name;
            view.GetChild(6).GetChild(4).GetChild(0).GetComponent<Text>().text = unit.secondSkill.description;
        } else {
            view.GetChild(6).GetChild(3).gameObject.SetActive(false);
            view.GetChild(6).GetChild(4).gameObject.SetActive(false);
        }
        if (unit.thirdSkill != null) {
            view.GetChild(6).GetChild(5).gameObject.SetActive(true);
            view.GetChild(6).GetChild(6).gameObject.SetActive(true);
            view.GetChild(6).GetChild(5).GetChild(0).GetComponent<Text>().text = unit.thirdSkill.name;
            view.GetChild(6).GetChild(6).GetChild(0).GetComponent<Text>().text = unit.thirdSkill.description;
        } else {
            view.GetChild(6).GetChild(5).gameObject.SetActive(false);
            view.GetChild(6).GetChild(6).gameObject.SetActive(false);
        }
        view.GetChild(7).GetChild(1).GetChild(0).GetComponent<Text>().text = "Health: " + unit.stat.health;
        view.GetChild(7).GetChild(2).GetChild(0).GetComponent<Text>().text = "Strength: " + unit.stat.strength;
        view.GetChild(7).GetChild(3).GetChild(0).GetComponent<Text>().text = "Magic: " + unit.stat.magic;
        view.GetChild(7).GetChild(4).GetChild(0).GetComponent<Text>().text = "Defense: " + unit.stat.defense;
        view.GetChild(7).GetChild(5).GetChild(0).GetComponent<Text>().text = "Agility: " + unit.stat.agility;
        view.GetChild(9).gameObject.SetActive(false);

        if (selectHeroIndex > 0) {
            view.GetChild(8).gameObject.SetActive(false);
            view.GetChild(10).gameObject.SetActive(true);
            view.GetChild(10).GetComponent<Button>().onClick.RemoveAllListeners();
            view.GetChild(10).GetComponent<Button>().onClick.AddListener(() => SelectHero(selectHeroIndex));
            return;
        } else {
            view.GetChild(8).gameObject.SetActive(true);
            view.GetChild(10).gameObject.SetActive(false);
        }

        view.GetChild(8).GetChild(0).GetComponent<Button>().onClick.RemoveAllListeners();
        if (inventoryIndex == player.formation[formationIndex]) {
            view.GetChild(8).GetChild(0).GetChild(0).GetComponent<Text>().text = "Remove";
            view.GetChild(8).GetChild(0).GetComponent<Button>().onClick.AddListener(() => Remove());
        } else {
            view.GetChild(8).GetChild(0).GetChild(0).GetComponent<Text>().text = "Assign";
            view.GetChild(8).GetChild(0).GetComponent<Button>().onClick.AddListener(() => Assign());
        }
        view.GetChild(8).GetChild(1).GetComponent<Button>().onClick.RemoveAllListeners();
        view.GetChild(8).GetChild(1).GetComponent<Button>().onClick.AddListener(() => OpenBoost());
    }
    void SelectHero(int selectHeroIndex) {
        player.AddToInventory(selectHeroIndex, 4, 4, 11, 11);
        transform.parent.Find("Select Hero").gameObject.SetActive(false);
        gameObject.SetActive(false);
    }

    public void Assign() {
        for (int i = 0; i < player.formation.Length; i++) {
            if (player.formation[i] == inventoryIndex) {
                player.RemoveFromFormation(i);
                break;
            }
        }
        player.AddToFormation(formationIndex, inventoryIndex);
        transform.parent.Find("Select Unit").gameObject.SetActive(false);
        gameObject.SetActive(false);
    }
    public void Remove() {
        player.RemoveFromFormation(formationIndex);
        transform.parent.Find("Select Unit").gameObject.SetActive(false);
        gameObject.SetActive(false);
    }

    public void OpenBoost() {
        view.GetChild(9).gameObject.SetActive(true);

        int rarityIndex = 0;
        switch (unit.rarity) {
            case Rarity.COMMON:
                rarityIndex = 0;
                break;
            case Rarity.RARE:
                rarityIndex = 1;
                break;
            case Rarity.EPIC:
                rarityIndex = 2;
                break;
            case Rarity.LEGENDARY:
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
        if (unit.levelCap >= 100) {
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
