using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AdventureDetails : MonoBehaviour
{
    public Adventure adventure;
    // Start is called before the first frame update

    void OnEnable() {
        transform.Find("Border/Title").GetChild(0).GetComponent<Text>().text = adventure.name;
        Transform slots = transform.Find("Border/Background/Slots");
        for (int i = 0; i < slots.childCount; i++) {
            if (adventure.currentPoint >= Adventure.pointTable[i]) {
                slots.GetChild(i).GetChild(1).GetComponent<Image>().color = new Color32(16,99,13,255);
            } else {
                slots.GetChild(i).GetChild(1).GetComponent<Image>().color = new Color32(77,74,74,255);
            }
            slots.GetChild(i).GetChild(1).GetChild(0).GetComponent<Text>().text = adventure.rewards[i].description;
        }
        transform.Find("Border/Background/Menu/Point").GetChild(0).GetComponent<Text>().text = "Current Point: " + adventure.currentPoint;
        transform.Find("Border/Background/Menu/Start Button").GetComponent<Button>().onClick.RemoveAllListeners();
        transform.Find("Border/Background/Menu/Start Button").GetComponent<Button>().onClick.AddListener(() => StartAdventure());
    }

    void StartAdventure() {
        Debug.Log(1);
        GameObject.Find("Canvas/BattleScreen").GetComponent<BattleScreen>().StartAdventure(adventure);
    }
}
