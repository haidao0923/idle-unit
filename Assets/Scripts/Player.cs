﻿using System;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    GameObject formationObject;
    public List<Unit> inventory = new List<Unit>();
    private List<int> strippedInventory = new List<int>();
    public int[] formation = new int[10];
    void Awake()
    {
        formationObject = GameObject.Find("Canvas/FormationScreen/FormationBackground");
        for (int i = 0; i < formation.Length; i++) {
            formation[i] = -1;
        }
        AddToInventory(0,2,3,5,12,1,2,2,3,4,5,81,12,12,11,23,2,4,6,8,1,3,6,5,4,12,11,23,2,4,6,8,1,3,6,5,4,2,4);
        AddToFormation(1, 9);
        AddToFormation(0, 15);
        AddToFormation(3, 27);
        AddToFormation(8, 4);
        ConsumableDatabase.consumables["Stone"][0].quantity = 10;
        ConsumableDatabase.consumables["Cap"][0].quantity = 10;
        ConsumableDatabase.consumables["Potion"][0].quantity = 10;
    }

    void Update()
    {

    }

    public void AddToInventory(params int[] units) {
        for (int i = 0; i < units.Length; i++) {
            if (units[i] < UnitDatabase.units.Length) {
                inventory.Add(new Unit(UnitDatabase.units[units[i]]));
                strippedInventory.Add(units[i]);
            }
        }
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
                strippedInventory.RemoveAt(indices[i]);
                if (IndexInFormation(indices[i], true) != -1) {
                    RemoveFromFormation(IndexInFormation(indices[i]));
                }
            }
        }
    }
    public void AddToFormation(int formationSlot, int inventoryId) {
        if (formationSlot >= 0 && formationSlot < 10 && inventoryId >= 0 && inventoryId < inventory.Count) {
            formation[formationSlot] = inventoryId;
            UpdateFormationDisplay(formationSlot);
        }
    }
    public void RemoveFromFormation(int formationSlot) {
        if (formationSlot >= 0 && formationSlot < 10) {
            formation[formationSlot] = -1;
            UpdateFormationDisplay(formationSlot);
        }
    }

    public void UpdateFormationDisplay(int formationSlot) {
        if (formationSlot < 5) {
            formationObject.transform.Find("Formation").GetChild(formationSlot).GetComponent<FormationCircle>().needUpdate = true;
        } else {
            formationObject.transform.Find("Reserve").GetChild(formationSlot % 5).GetComponent<FormationCircle>().needUpdate = true;
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
