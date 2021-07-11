using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AdventureStats : MonoBehaviour
{
    public void OpenMenu(Adventure adventure, int currentWave, int minionWaveCount, int restWaveCount, int eliteWaveCount, int bossWaveCount) {
        transform.Find("Border/Title").GetChild(0).GetComponent<Text>().text = adventure.name;
        int minionPoint = 50 * minionWaveCount;
        int restPoint = 25 * restWaveCount;
        int elitePoint = 150 * eliteWaveCount;
        int bossPoint = 250 * bossWaveCount;
        int subtotalPoint = minionPoint + restPoint + elitePoint + bossPoint;
        int victoryBonus = 0;
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
        string.Format("Minion Victories: 50 x {0} = {1}\n\nRest Visited: 25 x {2} = {3}\n\nElite Victories: 150 x {4} = {5}\n\nBoss Victories: 250 x {6} = {7}\n\nSubtotal Point: {8}\n\nVictory Bonus: {9}%\n\nTotal Point: {10}",
        minionWaveCount, minionPoint, restWaveCount, restPoint, eliteWaveCount, elitePoint, bossWaveCount, bossPoint, subtotalPoint, victoryBonus, totalScore);

        adventure.currentPoint += totalScore;
        transform.Find("Border/Background/Menu/Point").GetChild(0).GetComponent<Text>().text = "Current Point: " + adventure.currentPoint;
        transform.Find("Border/Background/Get Reward").gameObject.SetActive(false);
        Text rewardText = transform.Find("Border/Background/Get Reward").GetChild(1).GetComponent<Text>();
        for (int i = 0; i < adventure.receivedRewards.Length; i++) {
            if (adventure.currentPoint >= Adventure.pointTable[i]) {
                if (!adventure.receivedRewards[i]) {
                    adventure.rewards[i].GetReward();
                    rewardText.text = "You gained reward for " + Adventure.pointTable[i] + " point:\n" + adventure.rewards[i].description;
                    transform.Find("Border/Background/Get Reward").gameObject.SetActive(true);
                }
            } else {
                break;
            }
        }
    }

    void OnDisable() {
        GameObject.Find("Canvas/FormationScreen/FormationBackground/Traveling Merchant").SetActive(true);
    }
}
