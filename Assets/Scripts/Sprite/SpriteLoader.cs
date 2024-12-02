using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteLoader : MonoBehaviour
{
    // Start is called before the first frame update
    public static Sprite LoadSpriteFromResources(string imageName)
    {
        // Load the Texture2D from the Resources folder
        Texture2D texture = Resources.Load<Texture2D>(imageName);

        // Check if the texture is loaded successfully
        if (texture == null)
        {
            Debug.LogError("Image not found in Resources: " + imageName);
            return null;
        }

        // Create a new sprite from the texture
        return Sprite.Create(texture,
                                     new Rect(0, 0, texture.width, texture.height),
                                     new Vector2(0.5f, 0.5f));
    }
}
