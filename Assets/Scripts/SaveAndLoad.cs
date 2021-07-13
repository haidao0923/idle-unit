using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

public class SaveAndLoad : MonoBehaviour
{
    public static SaveAndLoad data;
    public bool isLoading;
    SavedData savedData;
    public Player player;

    void Awake()
    {
        if (data == null)
        {
            DontDestroyOnLoad(gameObject);
            data = this;
        }
        else if (data != this)
        {
            Destroy(gameObject);
        }
        savedData = new SavedData();
        Load();
    }
    public void SaveInventory() {
        savedData.inventory.Clear();
        for (int i = 0; i < player.inventory.Count; i++) {
            Unit unit = player.inventory[i];
            savedData.inventory.Add(new SavedUnitData(unit.id, unit.level, unit.capsAbsorbed, unit.exp));
        }
        Save();
    }
    public void SaveFormation() {
        savedData.formation = player.formation;
        Save();
    }
    public void LoadInventoryAndFormation(SavedData loadedData) {
        for (int i = 0; i < loadedData.inventory.Count; i++) {
            SavedUnitData loadedUnit = loadedData.inventory[i];
            Unit unit = UnitDatabase.GetUnitById(loadedUnit.id);
            unit.capsAbsorbed = loadedUnit.capsAbsorbed;
            unit.SetLevel(loadedUnit.level);
            unit.exp = loadedUnit.exp;
            player.inventory.Add(unit);
        }
        player.formation = loadedData.formation;

    }
    public void SaveConsumable() {
        savedData.consumables.Clear();
        foreach (KeyValuePair<string, Consumable[]> element in ConsumableDatabase.consumables) {
            for (int i = 0; i < element.Value.Count(); i++) {
                savedData.consumables.Add(new SavedConsumableData(element.Key, i, element.Value[i].quantity));
            }
        }
    }
    public void LoadConsumable(SavedData loadedData) {
        foreach (SavedConsumableData consumable in loadedData.consumables) {
            ConsumableDatabase.consumables[consumable.key][consumable.index].quantity = consumable.quantity;
        }
    }
    public void Save() {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/V1.dat", FileMode.Create);

        bf.Serialize(file, savedData);
        file.Close();
        Debug.Log("Saved");
    }

    public void Load()
    {
        isLoading = true;
        if (File.Exists(Application.persistentDataPath + "/V1.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/V1.dat", FileMode.Open);
            SavedData loadedData = (SavedData)bf.Deserialize(file);

            LoadInventoryAndFormation(loadedData);
            LoadConsumable(loadedData);


            file.Close();
            //if versionNumber is greater than or equal to current version number, then load data for new content
            Debug.Log("Loaded");
        }
        isLoading = false;
    }
}

[Serializable]
public class SavedUnitData {
    public int id;
    public int level, capsAbsorbed;
    public int exp;

    public SavedUnitData(int id, int level, int capsAbsorbed, int exp) {
        this.id = id;
        this.level = level;
        this.capsAbsorbed = capsAbsorbed;
        this.exp = exp;
    }
}

[Serializable]
public class SavedConsumableData {
    public string key;
    public int index;
    public int quantity;

    public SavedConsumableData(string key, int index, int quantity) {
        this.key = key;
        this.index = index;
        this.quantity = quantity;
    }
}

[Serializable]
public class SavedData {
    public List<SavedUnitData> inventory = new List<SavedUnitData>();
    public int[] formation = {-1, -1, -1, -1, -1, -1, -1, -1, -1, -1};
    public List<SavedConsumableData> consumables = new List<SavedConsumableData>();
}