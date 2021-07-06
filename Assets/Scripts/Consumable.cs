using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Consumable
{
    public string name;
    public int quantity;
    public int effect;
    public Sprite sprite;
    public Consumable(string name, Sprite sprite, int effect = 1) {
        this.name = name;
        quantity = 0;
        this.effect = effect;
        this.sprite = sprite;
    }
}
