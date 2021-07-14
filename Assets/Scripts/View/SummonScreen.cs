using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SummonScreen : MonoBehaviour
{
    // Start is called before the first frame update
    Player player; Transform summonDisplay, slots;
    int[] basicStonePool = {0, 1, 2, 3, 4, 5, 6, 7};
    int[] advanceStonePool;
    int[] ultimateStonePool;

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
        Unit summonedUnit = null;
        int random = 0;
        switch(summonType) {
            case SummonType.BasicStone:
                random = Random.Range(0, basicStonePool.Length);
                summonedUnit = new Unit(UnitDatabase.units[basicStonePool[random]]);
                player.AddToInventory(basicStonePool[random]);
                ConsumableDatabase.consumables["Stone"][0].quantity -= 1;
                break;
            case SummonType.AdvanceStone:
                random = Random.Range(0, advanceStonePool.Length);
                summonedUnit = new Unit(UnitDatabase.units[advanceStonePool[random]]);
                player.AddToInventory(advanceStonePool[random]);
                ConsumableDatabase.consumables["Stone"][1].quantity -= 1;
                break;
            case SummonType.UltimateStone:
                random = Random.Range(0, ultimateStonePool.Length);
                summonedUnit = new Unit(UnitDatabase.units[ultimateStonePool[random]]);
                player.AddToInventory(ultimateStonePool[random]);
                ConsumableDatabase.consumables["Stone"][2].quantity -= 1;
                break;
        }
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
