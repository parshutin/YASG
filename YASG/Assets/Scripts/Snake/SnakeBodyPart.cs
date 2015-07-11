using System.Collections;
using Assets.Scripts.Helpers;
using Assets.Scripts.Snake;
using UnityEngine;

public class SnakeBodyPart : MonoBehaviour
{
    [SerializeField]
    private SnakeSpritesHelper _spritesHelper;

    public SnakePart PartType;

    public SpriteRenderer SpriteRenderer;

    // Use this for initialization
    private void Update()
    {
        //yield return new WaitForSeconds(0.5f);
        if (SpriteRenderer.sprite == null)
        {
            SpriteRenderer.sprite = _spritesHelper.GetSprite(PartType);
        }
    }
}
