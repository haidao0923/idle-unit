using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TravelingMerchant : MonoBehaviour
{
    int[] merchantUnlock = {3, 6, 9, 12, 15};
    Player player;
    Transform merchant, closedShopTransform; Text merchantDisplay, coinDisplay, storeCreditDisplay;
    Transform buttons, youLose, youGain;
    int merchantIndex, points;
    int negotiatedCount, negotiationCost;

    List<int> lossUnitIndex = new List<int>();
    List<ConsumableToGain> consumableToGain = new List<ConsumableToGain>();

    // Cap, EXP, Stone, Item, Random but have 20% bonus;
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("GameController").GetComponent<Player>();
        merchant = transform.Find("Border/Background/Merchant");
        closedShopTransform = merchant.GetChild(4);
        merchantDisplay = merchant.GetChild(0).GetChild(0).GetComponent<Text>();
        coinDisplay = merchant.GetChild(0).Find("Coin/Text").GetComponent<Text>();
        storeCreditDisplay = merchant.GetChild(0).Find("Credit/Text").GetComponent<Text>();
        buttons = transform.Find("Border/Background/Merchant/Buttons");
        youLose = transform.Find("Border/Background/Merchant/You Lose");
        youGain = transform.Find("Border/Background/Merchant/You Gain");
    }

    void OnEnable() {
        OpenShop();
    }
    void OpenShop() {
        player.UpdateFormationDisplay();
        merchantIndex = -1;
        negotiatedCount = 0;
        NextMerchantButton();
    }
    void NextMerchantButton() {
        merchantIndex++;
        if (merchantIndex >= merchantUnlock.Length) {
            closedShopTransform.gameObject.SetActive(true);
            closedShopTransform.GetChild(1).GetComponent<Text>().text = "You have met all merchants for this adventure run.";
            return;
        } else if (merchantUnlock[merchantIndex] > Adventure.clearedAdventures) {
            closedShopTransform.gameObject.SetActive(true);
            closedShopTransform.GetChild(1).GetComponent<Text>().text = "Complete " + merchantUnlock[merchantIndex] + " different adventures to unlock the next merchant.";
            return;
        } else if (player.inventory.Count <= 10) {
            closedShopTransform.gameObject.SetActive(true);
            closedShopTransform.GetChild(1).GetComponent<Text>().text = "You must have more than 10 units in your inventory for merchants to be interested in you.";
            return;
        } else {
            closedShopTransform.gameObject.SetActive(false);
        }
        merchantDisplay.text = "Merchant " + (merchantIndex + 1) + "/" + merchantUnlock.Length;
        negotiatedCount = 0;
        UpdateDisplay();
    }
    void AcceptOfferButton(bool withStoreCredit = false) {
        if (!withStoreCredit) {
            lossUnitIndex.Sort();
            lossUnitIndex.Reverse();
            for (int i = 0; i < lossUnitIndex.Count; i++) {
                player.RemoveFromInventory(lossUnitIndex[i]);
            }
        }
        // Gain item
        for (int i = 0; i < consumableToGain.Count; i++) {
            ConsumableDatabase.consumables[consumableToGain[i].key][consumableToGain[i].index].quantity += consumableToGain[i].quantityToGain;
        }
        youLose.GetChild(6).gameObject.SetActive(true);
        youLose.GetChild(6).GetChild(0).gameObject.SetActive(true);
        for (int i = 1; i < 4; i++) {
            buttons.GetChild(i).GetComponent<Button>().interactable = false;
        }
    }
    void NegotiateButton() {
        ConsumableDatabase.consumables["Misc"][0].quantity -= negotiationCost;
        negotiatedCount += 1;
        UpdateDisplay();
    }
    void UseStoreCreditButton() {
        ConsumableDatabase.consumables["Misc"][1].quantity -= 1;
        AcceptOfferButton(withStoreCredit: true);
    }


    void UpdateDisplay() {
        coinDisplay.text = ConsumableDatabase.consumables["Misc"][0].quantity.ToString();
        storeCreditDisplay.text = ConsumableDatabase.consumables["Misc"][1].quantity.ToString();
        buttons.GetChild(0).GetComponent<Button>().onClick.RemoveAllListeners();
        buttons.GetChild(0).GetComponent<Button>().onClick.AddListener(() => NextMerchantButton());
        buttons.GetChild(1).GetComponent<Button>().interactable = true;
        buttons.GetChild(1).GetComponent<Button>().onClick.RemoveAllListeners();
        buttons.GetChild(1).GetComponent<Button>().onClick.AddListener(() => AcceptOfferButton());
        negotiationCost = negotiatedCount * 1000 + 1000;
        buttons.GetChild(2).GetChild(0).GetComponent<Text>().text = "Negotiate\nCost: " + negotiationCost;
        if (ConsumableDatabase.consumables["Misc"][0].quantity >= negotiationCost) {
            buttons.GetChild(2).GetComponent<Button>().interactable = true;
            buttons.GetChild(2).GetComponent<Button>().onClick.RemoveAllListeners();
            buttons.GetChild(2).GetComponent<Button>().onClick.AddListener(() => NegotiateButton());
        } else {
            buttons.GetChild(2).GetComponent<Button>().interactable = false;
        }
        if (ConsumableDatabase.consumables["Misc"][1].quantity > 0) {
            buttons.GetChild(3).GetComponent<Button>().interactable = true;
            buttons.GetChild(3).GetComponent<Button>().onClick.RemoveAllListeners();
            buttons.GetChild(3).GetComponent<Button>().onClick.AddListener(() => UseStoreCreditButton());
        } else {
            buttons.GetChild(3).GetComponent<Button>().interactable = false;
        }
        // You Lose Slot Display
        UpdateYouLoseDisplay();
        // You Win
        UpdateYouGainDisplay();
    }

    void UpdateYouLoseDisplay() {
        lossUnitIndex = new List<int>();
        points = 0;
        int i = 0;
        for ( ; i <= merchantIndex; i++) {
            Transform currentSlot = youLose.GetChild(i);
            int unitIndex;
            do {
                unitIndex = Random.Range(0, player.inventory.Count);
            } while (lossUnitIndex.Contains(unitIndex));
            lossUnitIndex.Add(unitIndex);
            Unit currentUnit = player.inventory[lossUnitIndex[i]]; //Random Unit
            points += ConvertRarityToPoint(currentUnit);
            UpdateUnitSlot(currentSlot, currentUnit, unitIndex);
        }
        for ( ; i < youLose.childCount; i++) {
            Transform currentSlot = youLose.GetChild(i);
            for (int j = 0; j < currentSlot.childCount; j++) {
                currentSlot.GetChild(j).gameObject.SetActive(false);
            }
        }
        youLose.GetChild(6).gameObject.SetActive(false);
    }
    void UpdateUnitSlot(Transform slot, Unit unit, int unitIndex) {
        for (int i = 0; i < slot.childCount; i++) {
            slot.GetChild(i).gameObject.SetActive(true);
        }
        slot.GetChild(0).GetComponent<Image>().sprite = unit.sprite;
        slot.GetChild(1).GetChild(0).GetComponent<Text>().text = "Lv " + unit.level;
        slot.GetChild(2).GetComponent<Image>().color = Unit.GetRarityColor(unit.rarity);
        slot.GetChild(2).GetChild(0).GetComponent<Text>().text = Unit.GetRarityAcronym(unit.rarity);
        slot.GetChild(3).GetChild(0).GetComponent<Image>().sprite = unit.GetElementSprite();
        for (int i = 0, j = 0; i < player.formation.Length; i++) {
            if (unitIndex == player.formation[i]) {
                slot.GetChild(4).gameObject.SetActive(true);
                j++;
                break;
            } else {
                slot.GetChild(4).gameObject.SetActive(false);
            }
        }
    }

    void UpdateYouGainDisplay() {
        UpdateYouGainList();
        int i = 0;
        for ( ; i < consumableToGain.Count; i++) {
            Transform currentSlot = youGain.GetChild(i);
            UpdateItemSlot(currentSlot, consumableToGain[i].GetConsumableFromDatabase().sprite, consumableToGain[i].quantityToGain);
        }
        for ( ; i < youGain.childCount; i++) {
            Transform currentSlot = youGain.GetChild(i);
            for (int j = 0; j < currentSlot.childCount; j++) {
                currentSlot.GetChild(j).gameObject.SetActive(false);
            }
        }
    }
    void UpdateYouGainList() {
        consumableToGain = new List<ConsumableToGain>();
        switch (merchantIndex) {
            case 0:
                MerchantOne();
                break;
            case 1:
                MerchantTwo();
                break;
            case 2:
                MerchantThree();
                break;
            case 3:
                MerchantFour();
                break;
            case 4:
                MerchantFive(Random.Range(0, 4));
                break;
        }
    }
    void UpdateItemSlot(Transform slot, Sprite sprite, int quantity) {
        for (int i = 0; i < slot.childCount; i++) {
            slot.GetChild(i).gameObject.SetActive(true);
        }
        slot.GetChild(0).GetComponent<Image>().sprite = sprite;
        slot.GetChild(1).GetChild(0).GetComponent<Text>().text = quantity.ToString();
    }
    void MerchantOne() {
        int capDatabaseIndex = 0;
        switch (player.inventory[lossUnitIndex[0]].rarity) {
            case Rarity.COMMON:
                capDatabaseIndex = 0;
                break;
            case Rarity.RARE:
                capDatabaseIndex = 1;
                break;
            case Rarity.EPIC:
                capDatabaseIndex = 2;
                break;
            case Rarity.LEGENDARY:
                capDatabaseIndex = 3;
                break;
        }
        int quantityToGain = player.inventory[lossUnitIndex[0]].capsAbsorbed + 1;
        consumableToGain.Add(new ConsumableToGain("Cap", capDatabaseIndex, quantityToGain));
    }
    void MerchantTwo() {
        int quantityGained;
        if ((quantityGained = GainedConsumable(6000)) > 0) {
            consumableToGain.Add(new ConsumableToGain("Potion", 2, quantityGained));
        }
        if ((quantityGained = GainedConsumable(1000)) > 0) {
            consumableToGain.Add(new ConsumableToGain("Potion", 1, quantityGained));
        }
        if ((quantityGained = GainedConsumable(200, true)) > 0) {
            consumableToGain.Add(new ConsumableToGain("Potion", 0, quantityGained));
        }
    }
    void MerchantThree() {
        int quantityGained;
        if ((quantityGained = GainedConsumable(1250)) > 0) {
            consumableToGain.Add(new ConsumableToGain("Stone", 2, quantityGained));
        }
        if ((quantityGained = GainedConsumable(450)) > 0) {
            consumableToGain.Add(new ConsumableToGain("Stone", 1, quantityGained));
        }
        if ((quantityGained = GainedConsumable(300, true)) > 0) {
            consumableToGain.Add(new ConsumableToGain("Stone", 0, quantityGained));
        }
    }
    void MerchantFour() {
        int quantityGained;
        if ((quantityGained = GainedConsumable(3500, true)) > 0) {
            consumableToGain.Add(new ConsumableToGain("Misc", 1, quantityGained));
        }
        consumableToGain.Add(new ConsumableToGain("Misc", 0, points * 5));
        points = 0;
    }
    void MerchantFive(int merchantIndex) {
        points = (int) (points * 1.2);
        switch (merchantIndex) {
            case 0:
                MerchantOne();
                break;
            case 1:
                MerchantTwo();
                break;
            case 2:
                MerchantThree();
                break;
            case 3:
                MerchantFour();
                break;
        }
    }
    int GainedConsumable(int price, bool exhaustive = false) {
        if (price == 0) {
            return 0;
        }
        int quantityToGain;
        if (exhaustive) {
            quantityToGain = points / price;
        } else {
            quantityToGain = Random.Range(-1, points / price) + 1;
        }
        if (quantityToGain > 0) {
            points -= price * quantityToGain;
            return quantityToGain;
        }
        return 0;
    }
    int ConvertRarityToPoint(Unit unit) {
        switch (unit.rarity) {
            case Rarity.COMMON:
                return 100 + unit.level * 10;
            case Rarity.RARE:
                return 250 + unit.level * 15;
            case Rarity.EPIC:
                return 1500 + unit.level * 20;
            case Rarity.LEGENDARY:
                return 10000 + unit.level * 25;
        }
        return -1;
    }

    private class ConsumableToGain {
        public string key;
        public int index;
        public int quantityToGain;

        public ConsumableToGain(string key, int index, int quantityToGain) {
            this.key = key;
            this.index = index;
            this.quantityToGain = quantityToGain;
        }
        public Consumable GetConsumableFromDatabase() {
            return ConsumableDatabase.consumables[key][index];
        }
    }
}
