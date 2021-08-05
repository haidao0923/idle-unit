using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ChallengeAdventure : Adventure
{
    public Challenge[] challenges;
    public ChallengeAdventure(int id, string name, int waveCount, int[] minionsId, int minMinionLevel, int maxMinionLevel,
            int[] eliteFormation, LevelAdvantage eliteLevelAdvantage, int[] bossFormation, LevelAdvantage bossLevelAdvantage,
            Sprite sprite, Reward[] rewards, Challenge[] challenges = null) : base(id, name, waveCount, minionsId, minMinionLevel, maxMinionLevel,
            eliteFormation, eliteLevelAdvantage, bossFormation, bossLevelAdvantage, sprite, rewards) {
        this.challenges = challenges;
    }
    public override bool MeetRequirement(Text[] texts) {
        bool meetRequirement = true;
        int i = 0;
        for ( ; i < challenges.Length; i++) {
            if (!challenges[i].MeetRequirement()) {
                meetRequirement = false;
                texts[i].color = new Color32(137,0,3,255);
            } else {
                texts[i].color = new Color32(0,245,25,255);
            }
            texts[i].text = challenges[i].description;
        }
        for ( ; i < 3; i++) {
            texts[i].text = "";
        }
        return meetRequirement;
    }
}

public class Challenge {
    ChallengeType challengeType;
    int unitAmount;
    Element[] element;
    Rarity[] rarity;
    public string description;

    public Challenge(ChallengeType challengeType, int unitAmount = 10, Element[] element = null, Rarity[] rarity = null) {
        this.challengeType = challengeType;
        this.unitAmount = unitAmount;
        this.element = element;
        this.rarity = rarity;
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
        switch (challengeType) {
            case ChallengeType.UNIT_AMOUNT:
                if (Player.UnitAmountInFormation() <= unitAmount) return true;
                break;
            case ChallengeType.ATLEAST:
                if (element != null) {
                    if (Player.UnitAmountWithElementInFormation(element) >= unitAmount) return true;
                }
                if (rarity != null) {
                    if (Player.UnitAmountWithRarityInFormation(rarity) >= unitAmount) return true;
                }
                break;
            case ChallengeType.ALL:
                if (element != null) {
                    if (Player.UnitAmountWithElementInFormation(element) == Player.UnitAmountInFormation()) return true;
                }
                if (rarity != null) {
                    if (Player.UnitAmountWithRarityInFormation(rarity) == Player.UnitAmountInFormation()) return true;
                }
                break;
        }
        return false;
    }

    public enum ChallengeType {
        ALL, ATLEAST, UNIT_AMOUNT
    }
}