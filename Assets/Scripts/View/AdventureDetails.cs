using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AdventureDetails : MonoBehaviour
{
    public Adventure adventure;
    [SerializeField] GameObject battleScreen;
    [SerializeField] Text currentPointText;
    [SerializeField] GameObject requirementPanel;
    [SerializeField] Text[] requirementTexts;
    [SerializeField] Text challengeTicketText;
    [SerializeField] Button requirementStartButton;

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
        currentPointText.text = "Current Point: " + adventure.currentPoint;
        requirementPanel.SetActive(false);
    }
    public void CheckRequirement() {
        if (!adventure.MeetRequirement(requirementTexts)) {
            DisplayRequirement();
            return;
        }
        StartAdventure();
    }
    public void StartAdventure() {
        GameObject.Find("Canvas/BattleScreen").GetComponent<BattleScreen>().StartAdventure(adventure);
        this.gameObject.SetActive(false);
        requirementPanel.SetActive(false);
        battleScreen.SetActive(true);
    }
    void DisplayRequirement() {
        requirementPanel.SetActive(true);
        int challengeTicketAmount = ConsumableDatabase.consumables["Misc"][3].quantity;
        challengeTicketText.text = "Challenge Ticket: " + challengeTicketAmount;
        if (challengeTicketAmount > 0) {
            requirementStartButton.interactable = true;
        }
    }
}
