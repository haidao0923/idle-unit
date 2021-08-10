using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AdventureDetails : MonoBehaviour
{
    public Adventure adventure;
    [SerializeField] Text titleText;
    [SerializeField] Transform slots;
    [SerializeField] GameObject chooseAdventure;
    [SerializeField] GameObject battleScreen;
    [SerializeField] Text currentPointText;
    [SerializeField] GameObject requirementPanel;
    [SerializeField] Text[] requirementTexts;
    [SerializeField] Text challengeTicketText;
    [SerializeField] Button requirementStartButton;
    [SerializeField] GameObject lorePanel;
    [SerializeField] Text loreText;

    void OnEnable() {
        titleText.text = adventure.name;
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
        lorePanel.SetActive(false);
    }
    public void CheckRequirement() {
        if (adventure is ChallengeAdventure) {
            DisplayRequirement();
            return;
        }
        StartAdventure();
    }
    public void StartAdventure() {
        chooseAdventure.SetActive(false);
        this.gameObject.SetActive(false);
        requirementPanel.SetActive(false);
        battleScreen.SetActive(true);
        battleScreen.GetComponent<BattleScreen>().StartAdventure(adventure);
    }
    public void DisplayRequirement() {
        requirementPanel.SetActive(true);
        int challengeTicketAmount = ConsumableDatabase.consumables["Misc"][3].quantity;
        challengeTicketText.text = "x" + challengeTicketAmount;
        bool meetRequirement;
        if ((meetRequirement = adventure.MeetRequirement(requirementTexts)) || challengeTicketAmount > 0) {
            requirementStartButton.interactable = true;
            requirementStartButton.onClick.RemoveAllListeners();
            requirementStartButton.onClick.AddListener(() => StartAdventure());
            requirementStartButton.onClick.AddListener(() => {if (!meetRequirement) ConsumableDatabase.consumables["Misc"][3].quantity -= 1;});
        } else {
            requirementStartButton.interactable = false;
        }
    }
    public void DisplayLore() {
        lorePanel.SetActive(true);
        loreText.text = adventure.lore;
    }
}
