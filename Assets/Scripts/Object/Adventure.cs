using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Adventure
{
    public int id;
    public string name;
    public int waveCount;
    public int[] minionsId;
    public int minMinionLevel, maxMinionLevel;
    public int[] eliteFormation, eliteFormationLevel;
    public int[] bossFormation, bossFormationLevel;
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
    public static int[] pointTable = {1000, 3000, 6000, 9000, 12000, 16000, 22000, 28000, 35000, 50000, 75000, 100000};
    public Reward[] rewards = new Reward[12];

    public Adventure(int id, string name, int waveCount, int[] minionsId, int minMinionLevel, int maxMinionLevel,
                     int[] eliteFormation, int[] eliteFormationLevel, int[] bossFormation, int[] bossFormationLevel,
                    Sprite sprite, Reward[] rewards = null) {
        this.id = id;
        this.name = name;
        this.waveCount = waveCount;
        this.minionsId = minionsId;
        this.minMinionLevel = minMinionLevel;
        this.maxMinionLevel = maxMinionLevel;
        this.eliteFormation = eliteFormation;
        this.eliteFormationLevel = eliteFormationLevel;
        this.bossFormation = bossFormation;
        this.bossFormationLevel = bossFormationLevel;
        this.sprite = sprite;
        this.rewards = rewards;
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
        }
    }

    public void GetReward() {
        switch (rewardType) {
            case RewardType.COIN:
                ConsumableDatabase.consumables["Misc"][0].quantity += rewardAmount;
                break;
        }
    }
}

public enum RewardType {
    COIN,
}