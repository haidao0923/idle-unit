using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableDatabase : MonoBehaviour
{
    public Sprite[] stoneSprites = new Sprite[3];
    public Sprite[] capSprites = new Sprite[4];
    public Sprite[] expPotionSprites = new Sprite[3];
    public Sprite[] miscSprites = new Sprite[3];
    public static Dictionary<string, Consumable[]> consumables = new Dictionary<string, Consumable[]>();
    public static System.DateTime lastRevivedTime, currentReviveTime;

    void Awake()
    {
        consumables.Add("Stone", new Consumable[] {
            new Consumable("Basic Stone", stoneSprites[0]),
            new Consumable("Advance Stone", stoneSprites[1]),
            new Consumable("Great Stone", stoneSprites[2]),
        });
        consumables.Add("Cap", new Consumable[] {
            new Consumable("Grey Cap", capSprites[0], 5),
            new Consumable("Green Cap", capSprites[1], 10),
            new Consumable("Purple Cap", capSprites[2], 25),
            new Consumable("Orange Cap", capSprites[3], 50),
        });
        consumables.Add("Potion", new Consumable[] {
            new Consumable("Lesser EXP Potion", expPotionSprites[0], 500),
            new Consumable("EXP Potion", expPotionSprites[1], 3000),
            new Consumable("Greater EXP Potion", expPotionSprites[2], 20000),
        });
        consumables.Add("Misc", new Consumable[] {
            new Consumable("Coin", miscSprites[0]),
            new Consumable("Store Credit", miscSprites[1]),
            new Consumable("Phoenix's Feather", miscSprites[2]),
        });
    }

}
