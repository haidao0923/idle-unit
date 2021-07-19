using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Security.Cryptography;
using UnityEngine;

public class SaveAndLoad : MonoBehaviour
{
    public static SaveAndLoad data;
    public bool isLoading;
    public Player player;
    private int key = 369;
    string persistentDataPath;
    public SavedData savedData = new SavedData();


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
        persistentDataPath = Application.dataPath + Path.AltDirectorySeparatorChar + "SaveData.json";
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
        Save();
    }
    public void LoadConsumable(SavedData loadedData) {
        foreach (SavedConsumableData consumable in loadedData.consumables) {
            ConsumableDatabase.consumables[consumable.key][consumable.index].quantity = consumable.quantity;
        }
    }
    public void SaveAdventure() {
        for (int i = 0; i < AdventureDatabase.adventures.Length; i++) {
            if (AdventureDatabase.adventures[i] == null) {
                break;
            }
            if (savedData.adventureList[i] == null) {
                savedData.adventureList[i] = new SavedAdventureData();
            }
            savedData.adventureList[i].currentPoint = AdventureDatabase.adventures[i].currentPoint;
        }
        savedData.clearedAdventures = Adventure.clearedAdventures;
        Save();
    }
    public void LoadAdventure(SavedData loadedData) {
        for (int i = 0; i < loadedData.adventureList.Length; i++) {
            AdventureDatabase.adventures[i].currentPoint = loadedData.adventureList[i].currentPoint;
        }
        Adventure.clearedAdventures = loadedData.clearedAdventures;
    }
    public void SaveCooldownTimer() {
        savedData.lastRevivedTime = ConsumableDatabase.lastRevivedTime;
        Save();
    }
    public void LoadCooldownTimer(SavedData loadedData) {
        ConsumableDatabase.lastRevivedTime = loadedData.lastRevivedTime;
    }
    public void Save() {
        string json = JsonUtility.ToJson(savedData);
        SecureHelper.SaveHash(json);
        Debug.Log(json);

        StreamWriter writer = new StreamWriter(persistentDataPath);
        writer.Write(SecureHelper.EncryptDecrypt(json, key));
        writer.Close();
    }

    public void Load() {
        isLoading = true;
        if (File.Exists(persistentDataPath)) {

            StreamReader reader = new StreamReader(persistentDataPath);
            string json = reader.ReadToEnd();
            json = SecureHelper.EncryptDecrypt(json, key);
            reader.Close();

            SavedData data = new SavedData();
            if (SecureHelper.VerifyHash(json)) {
                data = JsonUtility.FromJson<SavedData>(json);
            }
            savedData = data;
            Debug.Log(data.ToString());

            LoadInventoryAndFormation(data);
            LoadConsumable(data);
            LoadAdventure(data);
            LoadCooldownTimer(data);
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
public class SavedAdventureData {
    public int currentPoint;

    public SavedAdventureData() {
        // Nothing to add for now
    }
}

[Serializable]
public class SavedData {
    public List<SavedUnitData> inventory = new List<SavedUnitData>();
    public int[] formation = {-1, -1, -1, -1, -1, -1, -1, -1, -1, -1};
    public List<SavedConsumableData> consumables = new List<SavedConsumableData>();
    public SavedAdventureData[] adventureList = new SavedAdventureData[2];
    public System.DateTime lastRevivedTime;
    public int clearedAdventures;

}