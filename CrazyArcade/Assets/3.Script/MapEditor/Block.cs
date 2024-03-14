using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum Block_Type
{
    Empty = 0,
    Block,
    Broke,
    Slide,
    Item_Potion = 10,
    Item_Ballon = 11,
    Item_Skate = 12,
}

public class Block : MonoBehaviour
{
    [SerializeField]
    private Sprite[] BlockImage;

    [SerializeField]
    private Sprite[] ItemImage;


    private Block_Type blockType;

    private SpriteRenderer renderer;


    public void Setup(Block_Type type)
    {
        renderer = GetComponent<SpriteRenderer>();
        blockType = type;
    }

    public Block_Type BlockType
    {
        get => blockType;
        set
        {
            blockType = value;
            if((int)blockType<(int)Block_Type.Item_Potion)
            {
                renderer.sprite = BlockImage[(int)blockType];
            }
            else if((int)blockType <= (int)Block_Type.Item_Potion)
            {
                renderer.sprite = ItemImage[(int)blockType];
            }
        }
    }


}
