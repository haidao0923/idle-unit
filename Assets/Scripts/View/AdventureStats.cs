using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AdventureStats : MonoBehaviour
{
    public int victoryBonus;
    public void OpenMenu(Adventure adventure, int currentWave, int minionWaveCount, int restWaveCount, int eliteWaveCount, int bossWaveCount) {
        transform.Find("Border/Title").GetChild(0).GetComponent<Text>().text = adventure.name;
        int minionPoint = (int) WaveTypePoint.MINION * minionWaveCount;
        int restPoint = (int) WaveTypePoint.REST * restWaveCount;
        int elitePoint = (int) WaveTypePoint.ELITE * eliteWaveCount;
        int bossPoint = (int) WaveTypePoint.BOSS * bossWaveCount;
        int subtotalPoint = minionPoint + restPoint + elitePoint + bossPoint;
        victoryBonus = 0;
        if (currentWave > adventure.waveCount) {
            transform.Find("Border/Background/End Message").GetChild(0).GetComponent<Text>().text = "You Win";
            transform.Find("Border/Background/End Message").GetChild(1).GetComponent<Text>().text = "After a long adventure, your party gained a large amount of experiences, loots, and points!";
            victoryBonus = 100;
        } else {
            transform.Find("Border/Background/End Message").GetChild(0).GetComponent<Text>().text = "You Lose";
            transform.Find("Border/Background/End Message").GetChild(1).GetComponent<Text>().text = "Even though you were defeated, you still gain some points. Keep trying to beat the boss!";
            victoryBonus = 0;
        }
        int totalScore = (int) (subtotalPoint * (1 + victoryBonus / 100f));
        transform.Find("Border/Background/Point Background").GetChild(0).GetComponent<Text>().text =
        string.Format("Minion Victories: 50 x {0} = {1}\n\nRest Visited: 25 x {2} = {3}\n\nElite Victories: 100 x {4} = {5}\n\nBoss Victories: 150 x {6} = {7}\n\nSubtotal Point: {8}\n\nVictory Bonus: {9}%\n\nTotal Point: {10}",
        minionWaveCount, minionPoint, restWaveCount, restPoint, eliteWaveCount, elitePoint, bossWaveCount, bossPoint, subtotalPoint, victoryBonus, totalScore);

        int pointToGain = (int) (subtotalPoint * victoryBonus / 100f);
        adventure.currentPoint += pointToGain;

        transform.Find("Border/Background/Menu/Point").GetChild(0).GetComponent<Text>().text = "Current Point: " + adventure.currentPoint;
        transform.Find("Border/Background/Get Reward").gameObject.SetActive(false);
        Text rewardText = transform.Find("Border/Background/Get Reward").GetChild(1).GetComponent<Text>();
        for (int i = 0; i < adventure.rewards.Length; i++) {
            if (!adventure.rewards[i].received) {
                if (adventure.currentPoint >= Adventure.pointTable[i]) {
                    adventure.rewards[i].GetReward();
                    adventure.rewards[i].received = true;
                    rewardText.text = "You gained reward for " + Adventure.pointTable[i] + " point:\n" + adventure.rewards[i].description;
                } else {
                    break;
                }
                transform.Find("Border/Background/Get Reward").gameObject.SetActive(true);
            }
        }
    }

    void OnDisable() {
        if (victoryBonus > 0) {
            GameObject.Find("Canvas/FormationScreen/FormationBackground/Traveling Merchant").SetActive(true);
        }
    }
}

public enum WaveTypePoint {
    MINION = 50, REST = 25, ELITE = 100, BOSS = 150
}
public enum WaveTypeExp {
    MINION = 10, REST = 5, ELITE = 20, BOSS = 30
}