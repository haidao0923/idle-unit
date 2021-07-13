using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Consumable
{
    public string name;
    private int _quantity;
    public int quantity {
        get => _quantity;
        set
        {
            _quantity = value;
            if (!SaveAndLoad.data.isLoading) {
                SaveAndLoad.data.SaveConsumable();
            }
        }
    }
    public int effect;
    public Sprite sprite;
    public Consumable(string name, Sprite sprite, int effect = 1) {
        this.name = name;
        _quantity = 0;
        this.effect = effect;
        this.sprite = sprite;
    }
}
