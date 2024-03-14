using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

//start() 물풍선의 위치값 = Center
//터지는 범위 = center + 물풍선의 길이값 변수 + 끝부분
//배열에 물풍선의 길이만큼 for문 돌리기
//order 계시



public class Bubble : MonoBehaviour
{
    //private int[] Up_Explosion;
    //private int[] Down_Explosion;
    //private int[] Left_Explosion;
    //private int[] Right_Explosion;

    [SerializeField]
    private Player_Controller player;


    private BoxCollider2D collider;


    //물풍선줄기 프리팹=====
    [SerializeField]
    private GameObject Explosion_center;

    [SerializeField]
    private List<GameObject> Explosion_middle;

    [SerializeField]
    private GameObject Explosion_upedge;
    [SerializeField]
    private GameObject Explosion_downedge;
    [SerializeField]
    private GameObject Explosion_leftedge;
    [SerializeField]
    private GameObject Explosion_rightedge;



    GameObject center_prefab;
    GameObject up_prefab;
    GameObject down_prefab;
    GameObject left_prefab;
    GameObject right_prefab;

 


   

    //타일맵 및 위치 관련===============
    [SerializeField]
    private Tilemap tilemap;

    private Vector3 Current_position;       //시작 위치 변수

    private int length; //물풍선 길이(중간값)


    private float Boom_time = 0.5f;
    private float Current_time;


    private Queue<Vector3Int> boom_positions;

    //물풍선 터지는 애니메이션
    //[SerializeField]
    //private Animation center_animation;
    private bool isAnimEnded = false;

    private void Awake()
    {
        collider = GetComponent<BoxCollider2D>();
        player = GameObject.FindObjectOfType<Player_Controller>();
        tilemap = GameObject.FindObjectOfType<Tilemap>();
        Current_position = transform.position;
        Current_time = Boom_time;
        length = player.WaterLength;
        // middle_animator.SetBool("isAnimEnded", false);
        //center_animation = center_prefab.GetComponent<Animation>();
    }

    private void Start()
    {
        collider.enabled = false;
        StartCoroutine("Timer");
       
    }
    private void Update()
    {
      
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Water"))
        {
            Boom();
        }

    }



    public IEnumerator Timer()
    {
        yield return new WaitForSeconds(3f);
        Boom();
    }

    private void Boom()
    {

        player.PlayAudio(1);
        //Vector3Int boomPos = player.boom_positions.Dequeue();
        //Vector3Int boomPos = player.tilemapPos;
        Vector3Int boomPos = tilemap.WorldToCell(transform.position);

        //중앙
        center_prefab = Instantiate(Explosion_center, tilemap.GetCellCenterLocal(boomPos), Quaternion.identity);
        

        //미들
        Explosion_middle = new List<GameObject>();
        for (int i = 0; i <=     player.WaterLength; i++) 
        {
            if( i != 0)
            {
                Vector3Int up_location = new Vector3Int((int)boomPos.x, (int)boomPos.y + i, 0);
                Vector3Int down_location = new Vector3Int((int)boomPos.x, (int)boomPos.y - (i), 0);
                Vector3Int right_location = new Vector3Int((int)boomPos.x + (i), (int)boomPos.y, 0);
                Vector3Int left_location = new Vector3Int((int)boomPos.x - (i), (int)boomPos.y, 0);

                Explosion_middle.Add(Instantiate(Explosion_center, tilemap.GetCellCenterLocal(up_location), Quaternion.identity));
                Explosion_middle.Add(Instantiate(Explosion_center, tilemap.GetCellCenterLocal(down_location), Quaternion.identity));
                Explosion_middle.Add(Instantiate(Explosion_center, tilemap.GetCellCenterLocal(left_location), Quaternion.identity));
                Explosion_middle.Add(Instantiate(Explosion_center, tilemap.GetCellCenterLocal(right_location), Quaternion.identity));
            }
         
            
        }

        //끄트머리
        Vector3Int up_edge_location = new Vector3Int((int)boomPos.x, (int)boomPos.y + (player.WaterLength + 1), 0);
        Vector3Int down_edge_location = new Vector3Int((int)boomPos.x, (int)boomPos.y - (player.WaterLength + 1), 0);
        Vector3Int left_edge_location = new Vector3Int((int)boomPos.x - (player.WaterLength + 1), (int)boomPos.y, 0);
        Vector3Int right_edge_location = new Vector3Int((int)boomPos.x + (player.WaterLength + 1), (int)boomPos.y, 0);

        up_prefab = Instantiate(Explosion_upedge, tilemap.GetCellCenterWorld(up_edge_location), Quaternion.identity);
        down_prefab = Instantiate(Explosion_downedge, tilemap.GetCellCenterWorld(down_edge_location), Quaternion.identity);
        left_prefab = Instantiate(Explosion_leftedge, tilemap.GetCellCenterWorld(left_edge_location), Quaternion.identity);
        right_prefab = Instantiate(Explosion_rightedge, tilemap.GetCellCenterWorld(right_edge_location), Quaternion.identity);


        ////물풍선을 부모로 상속
        //up_prefab.transform.SetParent(gameObject.transform);
        //down_prefab.transform.SetParent(gameObject.transform);
        //left_prefab.transform.SetParent(gameObject.transform);
        //right_prefab.transform.SetParent(gameObject.transform);
        //for (int i = 0; i < Explosion_middle.Count; i++)
        //{
        //    Explosion_middle[i].transform.SetParent(gameObject.transform);
        //}


        player.BallonCount -= 1;
        Destroy(gameObject);




        Destroy(center_prefab,0.5f);
        Destroy(up_prefab, 0.5f);
        Destroy(down_prefab, 0.5f);
        Destroy(left_prefab, 0.5f);
        Destroy(right_prefab, 0.5f);

        for (int i = 0; i < Explosion_middle.Count; i++)
        {
            Destroy(Explosion_middle[i], 0.5f);
        }






    }





}
