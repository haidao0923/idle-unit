using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{

    Player player;
    Transform buttons;
    Button summonButton, shopButton, adventureButton;
    GameObject tooltip;

    void Start()
    {
        buttons = transform.Find("Buttons");
        summonButton = buttons.Find("Summon Button").GetComponent<Button>();
        shopButton = buttons.Find("Shop Button").GetComponent<Button>();
        adventureButton = buttons.Find("Adventure Button").GetComponent<Button>();
        tooltip = transform.Find("Tooltip").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        bool formationIsEmpty = true;
        for (int i = 0; i < Player.formation.Length; i++) {
            if (Player.formation[i] != -1) {
                formationIsEmpty = false;
                break;
            }
        }
        if (formationIsEmpty) {
            tooltip.SetActive(true);
            adventureButton.interactable = false;
            adventureButton.transform.GetChild(0).GetComponent<Text>().text = "Locked";
        } else {
            tooltip.SetActive(false);
            adventureButton.interactable = true;
            adventureButton.transform.GetChild(0).GetComponent<Text>().text = "Adventure";
        }
        if (Adventure.clearedAdventures >= 1) {
            summonButton.interactable = true;
            summonButton.transform.GetChild(0).GetComponent<Text>().text = "Summon";
        } else {
            summonButton.interactable = false;
            summonButton.transform.GetChild(0).GetComponent<Text>().text = "Locked";
        }
        if (Adventure.clearedAdventures >= 2) {
            shopButton.interactable = true;
            shopButton.transform.GetChild(0).GetComponent<Text>().text = "Shop";
        } else {
            shopButton.interactable = false;
            shopButton.transform.GetChild(0).GetComponent<Text>().text = "Locked";
        }
    }
}
