using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileMap2D : MonoBehaviour
{
    [Header("MapEditor")]

    [SerializeField]
    private GameObject BlockPrefabs;

    [SerializeField]
    private InputField input_Width;

    [SerializeField]
    private InputField input_Height;


    //�ʻ����� ����
    public int width { get; private set; } = 0;
    public int height { get; private set; } = 0;

    public List<Block> blockList { get; private set; }

    //�� ������ ����
    private MapData mapdata;

    private void Awake()
    {
        blockList = new List<Block>();
    }

    private void SpawnTile(Block_Type type, Vector3 position)
    {
        GameObject clone = Instantiate(BlockPrefabs, position, Quaternion.identity);
        clone.name = "Block";
        clone.transform.SetParent(transform);
        Block block = clone.GetComponent<Block>();

        block.Setup(type);
        blockList.Add(block);
    }

    public MapData GetMapData()
    {
        for(int i =0; i<blockList.Count; i++)
        {
            mapdata.Mapdata[i] = (int)blockList[i].BlockType;
        }

        return mapdata;
    }


}
