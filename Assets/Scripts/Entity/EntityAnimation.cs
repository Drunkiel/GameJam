using UnityEngine;

[System.Serializable]
public class EntityAnimation
{
    public Sprite[] sprites;
    public SpriteRenderer playerTexture;

    public void ChangeTexure(int index)
    {
        if (index >= sprites.Length)
            return;

        playerTexture.sprite = sprites[index];
    }
}
