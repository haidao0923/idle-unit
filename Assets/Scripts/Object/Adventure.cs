using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Adventure
{
    public int id;
    public string name;
    public int waveCount;
    public int[] minionsId;
    public int minMinionLevel, maxMinionLevel;
    public int[] eliteFormation;
    public LevelAdvantage eliteLevelAdvantage;
    public int[] bossFormation;
    public LevelAdvantage bossLevelAdvantage;
    public Sprite sprite;
    private int _currentPoint;
    public int currentPoint {
        get => _currentPoint;
        set {
            _currentPoint = value;
            SaveAndLoad.data.SaveAdventure();
        }
    }
    public bool cleared;
    private static int _clearedAdventures;
    public static int clearedAdventures {
        get => _clearedAdventures;
        set {
            _clearedAdventures = value;
            SaveAndLoad.data.SaveAdventure();
        }
    }
    public static int[] pointTable = {1000, 2000, 5000, 8000, 12000, 16000, 20000, 25000, 35000, 50000, 75000, 100000};
    public Reward[] rewards = new Reward[12];

    public Adventure(int id, string name, int waveCount, int[] minionsId, int minMinionLevel, int maxMinionLevel,
                     int[] eliteFormation, LevelAdvantage eliteLevelAdvantage, int[] bossFormation, LevelAdvantage bossLevelAdvantage,
                    Sprite sprite, Reward[] rewards = null) {
        this.id = id;
        this.name = name;
        this.waveCount = waveCount;
        this.minionsId = minionsId;
        this.minMinionLevel = minMinionLevel;
        this.maxMinionLevel = maxMinionLevel;
        this.eliteFormation = eliteFormation;
        this.eliteLevelAdvantage = eliteLevelAdvantage;
        this.bossFormation = bossFormation;
        this.bossLevelAdvantage = bossLevelAdvantage;
        this.sprite = sprite;
        this.rewards = rewards;
    }
    public virtual bool MeetRequirement(Text[] texts) {
        return true;
    }
}

public class Reward {
    RewardType rewardType;
    int rewardAmount;
    int extraInfo;
    public bool received;
    public string description;

    public Reward(RewardType rewardType, int rewardAmount, int extraInfo = -1) {
        this.rewardType = rewardType;
        this.rewardAmount = rewardAmount;
        this.extraInfo = extraInfo;
        switch (rewardType) {
            case RewardType.COIN:
                description = rewardAmount + " Coins";
                break;
            case RewardType.MISC:
                description = ConsumableDatabase.consumables["Misc"][extraInfo].name + " x" + rewardAmount;
                break;
            case RewardType.STONE:
                description = ConsumableDatabase.consumables["Stone"][extraInfo].name + " x" + rewardAmount;
                break;
            case RewardType.CAP:
                description = ConsumableDatabase.consumables["Cap"][extraInfo].name + " x" + rewardAmount;
                break;
            case RewardType.POTION:
                description = ConsumableDatabase.consumables["Potion"][extraInfo].name + " x" + rewardAmount;
                break;
            case RewardType.CARD:
                description = UnitDatabase.units[extraInfo].name + " x" + rewardAmount;
                break;
        }
    }

    public void GetReward() {
        switch (rewardType) {
            case RewardType.COIN:
                ConsumableDatabase.consumables["Misc"][0].quantity += rewardAmount;
                break;
            case RewardType.MISC:
                ConsumableDatabase.consumables["Misc"][extraInfo].quantity += rewardAmount;
                break;
            case RewardType.STONE:
                ConsumableDatabase.consumables["Stone"][extraInfo].quantity += rewardAmount;
                break;
            case RewardType.CAP:
                ConsumableDatabase.consumables["Cap"][extraInfo].quantity += rewardAmount;
                break;
            case RewardType.POTION:
                ConsumableDatabase.consumables["Potion"][extraInfo].quantity += rewardAmount;
                break;
            case RewardType.CARD:
                int[] indexArray = new int[rewardAmount];
                for (int i = 0; i < rewardAmount; i++) {
                    indexArray[i] = extraInfo;
                }
                GameObject.FindGameObjectWithTag("GameController").GetComponent<Player>().AddToInventory(indexArray);
                break;
        }
    }
}

public enum RewardType {
    COIN, MISC, STONE, CAP, POTION, CARD
}

public enum LevelAdvantage {
    ALL, BOSS
}