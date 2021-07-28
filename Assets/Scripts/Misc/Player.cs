using System;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject formationObject;
    public GameObject selectHero;
    public List<Unit> inventory = new List<Unit>();
    public int[] formation = {-1, -1, -1, -1, -1, -1, -1, -1, -1, -1};
    void Awake()
    {
        if (inventory.Count == 0) {
            selectHero.SetActive(true);
            ConsumableDatabase.consumables["Stone"][0].quantity = 10;
            ConsumableDatabase.consumables["Stone"][1].quantity = 10;
            ConsumableDatabase.consumables["Stone"][2].quantity = 10;

            ConsumableDatabase.consumables["Cap"][0].quantity = 10;
            ConsumableDatabase.consumables["Potion"][0].quantity = 10;
        }
        ConsumableDatabase.consumables["Stone"][0].quantity = 10;
        ConsumableDatabase.consumables["Stone"][1].quantity = 10;
        ConsumableDatabase.consumables["Stone"][2].quantity = 10;
        for (int i = 0; i < formation.Length; i++) {
            Debug.Log(formation[i]);
        }
        UpdateFormationDisplay();
    }

    void Update()
    {

    }

    public void AddToInventory(params int[] units) {
        for (int i = 0; i < units.Length; i++) {
            if (units[i] < UnitDatabase.units.Length) {
                inventory.Add(new Unit(UnitDatabase.units[units[i]]));
            }
        }
        SaveAndLoad.data.SaveInventory();
    }
    public void AddToInventory(List<int> list) {
        foreach (int element in list) {
            AddToInventory(element);
        }
    }
    public void RemoveFromInventory(params int[] indices) {
        Array.Sort(indices);
        Array.Reverse(indices);
        for (int i = 0; i < indices.Length; i++) {
            if (indices[i] < inventory.Count) {
                inventory.RemoveAt(indices[i]);
                if (IndexInFormation(indices[i], true) != -1) {
                    RemoveFromFormation(IndexInFormation(indices[i]));
                }
            }
        }
        SaveAndLoad.data.SaveInventory();
    }
    public void AddToFormation(int formationSlot, int inventoryId) {
        if (formationSlot >= 0 && formationSlot < 10 && inventoryId >= 0 && inventoryId < inventory.Count) {
            formation[formationSlot] = inventoryId;
            UpdateFormationDisplay(formationSlot);
            SaveAndLoad.data.SaveFormation();
        }
    }
    public void RemoveFromFormation(int formationSlot) {
        if (formationSlot >= 0 && formationSlot < 10) {
            formation[formationSlot] = -1;
            UpdateFormationDisplay(formationSlot);
            SaveAndLoad.data.SaveFormation();
        }
    }

    public void UpdateFormationDisplay(int formationSlot) {
        if (formationSlot < 5) {
            formationObject.transform.Find("Formation").GetChild(formationSlot).GetComponent<FormationCircle>().needUpdate = true;
        } else {
            formationObject.transform.Find("Reserve").GetChild(formationSlot % 5).GetComponent<FormationCircle>().needUpdate = true;
        }
    }
    public void UpdateFormationDisplay() {
        for (int i = 0; i < 5; i++) {
            formationObject.transform.Find("Formation").GetChild(i).GetComponent<FormationCircle>().needUpdate = true;
            formationObject.transform.Find("Reserve").GetChild(i).GetComponent<FormationCircle>().needUpdate = true;
        }
    }
    public int IndexInFormation(int index, bool toRemove = false) {
        int returnedValue = -1;
        for (int i = 0; i < formation.Length; i++) {
            if (formation[i] == index) {
                returnedValue = i;
                if (!toRemove) {
                    return returnedValue;
                }
            } else if (toRemove && formation[i] > index ) {
                formation[i] -= 1;
            }
        }
        return returnedValue;
    }

    public List<int> GetInventoryIndexOfUnitsWithRarity(Rarity rarity) {
        List<int> tempList = new List<int>();
        for (int i = 0; i < inventory.Count; i++) {
            if (inventory[i].rarity == rarity) {
                tempList.Add(i);
            }
        }
        return tempList;
    }
    public List<int> GetInventoryIndexOfUnitsWithElement(Element element) {
        List<int> tempList = new List<int>();
        for (int i = 0; i < inventory.Count; i++) {
            if (inventory[i].element == element) {
                tempList.Add(i);
            }
        }
        return tempList;
    }
}
