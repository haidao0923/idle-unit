using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChallengeAdventure : Adventure
{
    public Challenge[] challenges;
    public ChallengeAdventure(int id, string name, int waveCount, int[] minionsId, int minMinionLevel, int maxMinionLevel,
            int[] eliteFormation, LevelAdvantage eliteLevelAdvantage, int[] bossFormation, LevelAdvantage bossLevelAdvantage,
            Sprite sprite, Reward[] rewards = null) : base(id, name, waveCount, minionsId, minMinionLevel, maxMinionLevel,
            eliteFormation, eliteLevelAdvantage, bossFormation, bossLevelAdvantage, sprite, rewards) {
        //
    }
}

public class Challenge {
    ChallengeType challengeType;
    int unitAmount;
    Element[] element;
    Rarity[] rarity;
    string description;

    public Challenge(ChallengeType challengeType, int unitAmount = 10, Element[] element = null, Rarity[] rarity = null) {
        this.challengeType = challengeType;
        this.unitAmount = unitAmount;
        this.element = element;
        this.rarity = rarity;
        //description
        switch (challengeType) {
            case ChallengeType.UNIT_AMOUNT:
                description = "Have at most " + unitAmount + " unit";
                break;
            case ChallengeType.ATLEAST:
                if (element != null) {
                    description = "At least " + unitAmount + " unit must be " + string.Join(" or ", element);
                }
                if (rarity != null) {
                    description = "At least " + unitAmount + " unit must be " + string.Join(" or ", rarity);
                }
                break;
            case ChallengeType.ALL:
                if (element != null) {
                    description = "All unit must be " + string.Join(" or ", element);
                }
                if (rarity != null) {
                    description = "At least " + unitAmount + " unit must be " + string.Join(" or ", rarity);
                }
                break;
        }
    }

    public bool MeetRequirement() {
        return false;
    }

    public enum ChallengeType {
        ALL, ATLEAST, UNIT_AMOUNT
    }
}