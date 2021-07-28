using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SummonScreen : MonoBehaviour
{
    // Start is called before the first frame update
    Player player; Transform summonDisplay, slots;
    int[] commonUnit = {1,2,3,4,5,6,7,8,9,10,11,12,13,14};
    int[] rareUnit = {15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30,31,32,33,34,35,36,37,38,39,40,41,42,43};
    int[] epicUnit = {43,44,45,46,47,48,49,50,51,52,53,54,55,56,57,58,59,60,61,62,63,64,65,66,67,68,69,70,71,
                      72,73,74,75,76,77,78,79,80,81,82,83,84,85,86,87,88,89};
    int[] legendaryUnit = {90,91,92,93,94,95,96,97,98,99,100,101,102,103,104,105,106,107,108,109,110,111};

    void Awake() {
        player = GameObject.FindGameObjectWithTag("GameController").GetComponent<Player>();
        slots = transform.Find("Border/Background/Slots");
        summonDisplay = transform.Find("Border/Background/Slots/Show Summon");
    }

    void OnEnable()
    {
        summonDisplay.gameObject.SetActive(false);
        CreateListener();
        UpdateDisplay();
    }

    void SummonUnit(SummonType summonType)
    {
        int random = 0;
        switch(summonType) {
            case SummonType.BasicStone:
                ConsumableDatabase.consumables["Stone"][0].quantity -= 1;
                random = Random.Range(0, 100);
                if (random >= 95) {
                    GetRandomUnit(Rarity.RARE);
                } else {
                    GetRandomUnit(Rarity.COMMON);
                }
                break;
            case SummonType.AdvanceStone:
                ConsumableDatabase.consumables["Stone"][1].quantity -= 1;
                random = Random.Range(0, 100);
                if (random >= 95) {
                    GetRandomUnit(Rarity.EPIC);
                } else if (random >= 80) {
                    GetRandomUnit(Rarity.RARE);
                } else {
                    GetRandomUnit(Rarity.COMMON);
                }
                break;
            case SummonType.UltimateStone:
                ConsumableDatabase.consumables["Stone"][2].quantity -= 1;
                random = Random.Range(0, 100);
                if (random >= 97) {
                    GetRandomUnit(Rarity.LEGENDARY);
                } else if (random >= 82) {
                    GetRandomUnit(Rarity.EPIC);
                } else {
                    GetRandomUnit(Rarity.RARE);
                }
                break;
        }
    }

    void GetRandomUnit(Rarity rarity) {
        int summonedUnitIndex = 0;
        switch (rarity) {
            case Rarity.COMMON:
                summonedUnitIndex = commonUnit[Random.Range(0, commonUnit.Length)];
                break;
            case Rarity.RARE:
                summonedUnitIndex = rareUnit[Random.Range(0, rareUnit.Length)];
                break;
            case Rarity.EPIC:
                summonedUnitIndex = epicUnit[Random.Range(0, epicUnit.Length)];
                break;
            case Rarity.LEGENDARY:
                summonedUnitIndex = legendaryUnit[Random.Range(0, legendaryUnit.Length)];
                break;
        }
        player.AddToInventory(summonedUnitIndex);
        Unit summonedUnit = new Unit(UnitDatabase.units[summonedUnitIndex]);
        summonDisplay.GetComponent<Image>().color = Unit.GetRarityColor(summonedUnit.rarity);
        summonDisplay.GetChild(0).GetComponent<Image>().sprite = summonedUnit.sprite;
        slots.GetComponent<Animation>().Play();
        UpdateDisplay();
    }

    void CreateListener() {
        slots.GetChild(0).Find("Button").GetComponent<Button>().onClick.RemoveAllListeners();
        slots.GetChild(1).Find("Button").GetComponent<Button>().onClick.RemoveAllListeners();
        slots.GetChild(2).Find("Button").GetComponent<Button>().onClick.RemoveAllListeners();
        slots.GetChild(0).Find("Button").GetComponent<Button>().onClick.AddListener(() => SummonUnit(SummonType.BasicStone));
        slots.GetChild(1).Find("Button").GetComponent<Button>().onClick.AddListener(() => SummonUnit(SummonType.AdvanceStone));
        slots.GetChild(2).Find("Button").GetComponent<Button>().onClick.AddListener(() => SummonUnit(SummonType.UltimateStone));
    }
    void UpdateDisplay() {
        for (int i = 0; i < 3; i++) {
            slots.GetChild(i).Find("Count").GetChild(1).GetComponent<Text>().text = "x" + ConsumableDatabase.consumables["Stone"][i].quantity;
            if (ConsumableDatabase.consumables["Stone"][i].quantity > 0) {
                slots.GetChild(i).Find("Button").GetComponent<Button>().interactable = true;
            } else {
                slots.GetChild(i).Find("Button").GetComponent<Button>().onClick.RemoveAllListeners();
                slots.GetChild(i).Find("Button").GetComponent<Button>().interactable = false;
            }
        }
    }

    enum SummonType {
        BasicStone, AdvanceStone, UltimateStone
    }
}
