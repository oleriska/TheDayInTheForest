using UnityEngine;

public class Dialog
{
    public string Name { get; set; }
    public string Text { get; set; }
    public Sprite Image { get => GetSprite(); }

    public Dialog(string name, string text)
    {
        Name = name;
        Text = text;
    }
    private Sprite GetSprite()
    {
        return Resources.Load<Sprite>(Name);
    }
}
