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
        if (isLoading) {
            return;
        }
        savedData.inventory.Clear();
        for (int i = 0; i < Player.inventory.Count; i++) {
            Unit unit = Player.inventory[i];
            savedData.inventory.Add(new SavedUnitData(unit.id, unit.level, unit.capsAbsorbed, unit.exp));
        }
        Save();
    }
    public void SaveFormation() {
        if (isLoading) {
            return;
        }
        savedData.formation = Player.formation;
        Save();
    }
    public void LoadInventoryAndFormation(SavedData loadedData) {
        for (int i = 0; i < loadedData.inventory.Count; i++) {
            SavedUnitData loadedUnit = loadedData.inventory[i];
            Unit unit = UnitDatabase.GetUnitById(loadedUnit.id);
            unit.capsAbsorbed = loadedUnit.capsAbsorbed;
            unit.SetLevel(loadedUnit.level);
            unit.exp = loadedUnit.exp;
            Player.inventory.Add(unit);
        }
        Player.formation = loadedData.formation;

    }
    public void SaveConsumable() {
        if (isLoading) {
            return;
        }
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
        if (isLoading) {
            return;
        }
        SaveAdventureHelper(AdventureDatabase.adventures, savedData.adventureList);
        SaveAdventureHelper(AdventureDatabase.challengeAdventures, savedData.challengeAdventureList);
        SaveAdventureHelper(AdventureDatabase.ascendedAdventures, savedData.ascendedAdventureList);
        savedData.clearedAdventures = Adventure.clearedAdventures;
        Save();
    }
    private void SaveAdventureHelper(Adventure[] adventures, SavedAdventureData[] savedAdventureList) {
        for (int i = 0; i < adventures.Length; i++) {
            if (adventures[i] == null) {
                break;
            }
            if (savedAdventureList[i] == null) {
                savedAdventureList[i] = new SavedAdventureData();
            }
            savedAdventureList[i].currentPoint = adventures[i].currentPoint;
            savedAdventureList[i].cleared = adventures[i].cleared;
            for (int j = 0; j < savedAdventureList[i].receivedReward.Length; j++) {
                savedAdventureList[i].receivedReward[j] = adventures[i].rewards[j].received;
            }
        }
    }
    public void LoadAdventure(SavedData loadedData) {
        LoadAdventureHelper(AdventureDatabase.adventures, loadedData.adventureList);
        LoadAdventureHelper(AdventureDatabase.challengeAdventures, loadedData.challengeAdventureList);
        LoadAdventureHelper(AdventureDatabase.ascendedAdventures, loadedData.ascendedAdventureList);

        Adventure.clearedAdventures = loadedData.clearedAdventures;
    }
    private void LoadAdventureHelper(Adventure[] adventures, SavedAdventureData[] loadedAdventureList) {
        for (int i = 0; i < loadedAdventureList.Length; i++) {
            if (adventures[i] == null) {
                break;
            }
            adventures[i].currentPoint = loadedAdventureList[i].currentPoint;
            adventures[i].cleared = loadedAdventureList[i].cleared;
            for (int j = 0; j < loadedAdventureList[i].receivedReward.Length; j++) {
                adventures[i].rewards[j].received = loadedAdventureList[i].receivedReward[j];
            }
        }
    }
    public void SaveCooldownTimer() {
        if (isLoading) {
            return;
        }
        savedData.lastRevivedTime = ConsumableDatabase.lastRevivedTime.ToBinary();
        Save();
    }
    public void LoadCooldownTimer(SavedData loadedData) {
        ConsumableDatabase.lastRevivedTime = DateTime.FromBinary(loadedData.lastRevivedTime);
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

            Debug.Log(json);
            SavedData data = new SavedData();
            if (SecureHelper.VerifyHash(json)) {
                data = JsonUtility.FromJson<SavedData>(json);
            }
            savedData = data;

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
    public bool cleared;
    public bool[] receivedReward = new bool[12];
    public SavedAdventureData() {
        // Nothing to add for now
    }
}

[Serializable]
public class SavedData {
    public List<SavedUnitData> inventory = new List<SavedUnitData>();
    public int[] formation = {-1, -1, -1, -1, -1, -1, -1, -1, -1, -1};
    public List<SavedConsumableData> consumables = new List<SavedConsumableData>();
    public SavedAdventureData[] adventureList = new SavedAdventureData[40];
    public SavedAdventureData[] challengeAdventureList = new SavedAdventureData[40];
    public SavedAdventureData[] ascendedAdventureList = new SavedAdventureData[40];
    public long lastRevivedTime;
    public int clearedAdventures;

}