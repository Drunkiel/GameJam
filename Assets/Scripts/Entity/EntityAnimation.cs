using UnityEngine;
using UnityEngine.Rendering.Universal;

[System.Serializable]
public class EntityAnimation
{
    public Sprite[] sprites;
    public Light2D light;
    public SpriteRenderer playerTexture;

    public void ChangeTexure(int index)
    {
        if (index >= sprites.Length)
            return;

        light.lightCookieSprite = sprites[index];
        playerTexture.sprite = sprites[index];
    }
}
