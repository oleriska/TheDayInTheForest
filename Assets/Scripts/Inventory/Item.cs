using System;
using UnityEngine;

[Serializable]
public class Item
{
    public string Name;
    public string Description;
    public bool Equipable;
    public Sprite Image { get => GetSprite(); }

    public Item(string name, string description)
    {
        Name = name;
        Description = description;
    }

    public Item(string name, string description, bool equipable)
    {
        Name = name;
        Description = description;
        Equipable = equipable;
    }

    private Sprite GetSprite()
    {
        return Resources.Load<Sprite>(Name);
    }
}
