using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FormationCircle : MonoBehaviour
{
    [SerializeField] SelectUnit selectUnitScript;
    [SerializeField] UnitView unitViewScript;
    AuraSprite auraSprite;
    public bool needUpdate;

    [SerializeField] int formationIndex;
    void Start()
    {
        GameObject gameController = GameObject.FindGameObjectWithTag("GameController");
        auraSprite = gameController.GetComponent<AuraSprite>();
        UpdateUnitDisplay();
    }

    void Update() {
        if (needUpdate) {
            UpdateUnitDisplay();
            needUpdate = false;
        }
    }

    public void OnClick() {
        int inventoryIndex = Player.formation[formationIndex];
        if (inventoryIndex == -1) {
            selectUnitScript.OpenPanel(formationIndex);
        } else {
            unitViewScript.OpenPanel(formationIndex, inventoryIndex);
        }
    }

    public void UpdateUnitDisplay() {
        if (Player.formation[formationIndex] != -1) {
            int inventoryIndex = Player.formation[formationIndex];
            Unit unit = Player.inventory[inventoryIndex];
            for (int i = 0; i < transform.childCount - 1; i++) {
                transform.GetChild(i).gameObject.SetActive(true);
            }
            transform.GetChild(0).GetComponent<Image>().sprite = unit.sprite;
            transform.GetChild(1).GetChild(0).GetComponent<Text>().text = "Lv\n" + unit.level;
            transform.GetChild(2).GetComponent<Image>().color = Unit.GetRarityColor(unit.rarity);
            transform.GetChild(2).GetChild(0).GetComponent<Text>().text = Unit.GetRarityAcronym(unit.rarity);
            transform.GetChild(3).GetChild(0).GetComponent<Image>().sprite = unit.GetElementSprite();
        } else {
            for (int i = 0; i < transform.childCount - 1; i++) {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
}
