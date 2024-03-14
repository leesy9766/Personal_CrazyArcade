using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Sliding_Box : MonoBehaviour
{

    [SerializeField]
    MapSizeData mapSize;

    private BoxCollider2D collider;
    private Rigidbody2D rigidBody;




    Vector3Int upPosition;
    Vector3Int downPosition;
    Vector3Int rightPosition;
    Vector3Int leftPosition;

    RaycastHit2D upRay;
    RaycastHit2D downRay;
    RaycastHit2D leftRay;
    RaycastHit2D rightRay;




    private Vector3 CurrentPosition;    //���� ���� ������

    private Vector3Int CurrentCellPosition; //���� ������ �� ��ǥ

    private Vector3 box_location;   //������Ʈ�� ������
  
    private Vector3Int tilemapPos;  //������ġ�� �� ��ǥ

    private Vector3 Destination;


    bool isCollision;
    bool isBlocked;


    private Tilemap tilemap;

    private void Awake()
    {
        tilemap = FindObjectOfType<Tilemap>();
        collider = GetComponent<BoxCollider2D>();
        rigidBody = GetComponent<Rigidbody2D>();

    }
    private void Start()
    {
        isBlocked = false;

        isCollision = false;

        CurrentPosition = transform.position;   //���� ������

        tilemapPos = tilemap.WorldToCell(CurrentPosition);



        Destination = transform.position;


        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;

        //tilemapPos = tilemap.WorldToCell(CurrentPosition);
   
    }


    private void Update()
    {
        GameObject up_col_obj = new GameObject();
        GameObject down_col_obj = new GameObject();
        GameObject left_col_obj = new GameObject();
        GameObject right_col_obj = new GameObject();

        upRay = Physics2D.Raycast(transform.position + Vector3.up, Vector3.down, 0.6f);
        downRay = Physics2D.Raycast(transform.position + Vector3.down, Vector3.up, 0.6f);
        leftRay = Physics2D.Raycast(transform.position + Vector3.left, Vector3.right, 0.6f);
        rightRay = Physics2D.Raycast(transform.position + Vector3.right, Vector3.left, 0.6f);

        if (upRay.collider != null) up_col_obj = upRay.collider.GetComponent<GameObject>();
        if (upRay.collider != null) down_col_obj = upRay.collider.GetComponent<GameObject>();
        if (upRay.collider != null) left_col_obj = upRay.collider.GetComponent<GameObject>();
        if (upRay.collider != null) right_col_obj = upRay.collider.GetComponent<GameObject>();


        
        //if(!up_col_obj.CompareTag("Wall"))
        //{
        //    StartCoroutine(MoveTo(transform.position, tilemapPos));
        //}
        //if (!down_col_obj.CompareTag("Wall"))
        //{
        //    StartCoroutine(MoveTo(transform.position, tilemapPos));
        //}
        //if (!down_col_obj.CompareTag("Wall"))
        //{
        //    StartCoroutine(MoveTo(transform.position, tilemapPos));
        //}
        //if (!down_col_obj.CompareTag("Wall"))
        //{
        //    StartCoroutine(MoveTo(transform.position, tilemapPos));
        //}


        //CurrentPosition = transform.position;


        //transform.position = Vector3.MoveTowards(gameObject.transform.position, Destination, 0.001f);


    }

    private void LateUpdate()
    {

        //�÷��̾ ȭ�� ���� ������ ������ �ʵ��� ����
        transform.position = new Vector3
            (
            Mathf.Clamp(transform.position.x, mapSize.LimitMin.x, mapSize.LimitMax.x),
            Mathf.Clamp(transform.position.y, mapSize.LimitMin.y, mapSize.LimitMax.y),
            0
            );


        CurrentCellPosition = tilemap.WorldToCell(transform.position);
        upPosition = CurrentCellPosition + new Vector3Int(CurrentCellPosition.x, CurrentCellPosition.y , 0);
        downPosition = CurrentCellPosition + new Vector3Int(CurrentCellPosition.x, CurrentCellPosition.y , 0);
        leftPosition = CurrentCellPosition + new Vector3Int(CurrentCellPosition.x, CurrentCellPosition.y , 0);
        rightPosition = CurrentCellPosition + new Vector3Int(CurrentCellPosition.x, CurrentCellPosition.y , 0);

    }




    private void OnCollisionEnter2D(Collision2D collision)
    {
        //���� �߾Ӱ����� �����ϱ�

        //if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("SlidingBox") || collision.gameObject.CompareTag("Block"))
        //{
        //    Debug.Log("���̶� �ε���");
        //    //transform.position = mapSize.LimitMax;
        //    //transform.position = tilemapPos;    
        //    rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;

        //}
        if (collision.transform.CompareTag("Player"))
        {
            Debug.Log("�浹");

            //isBlocked = false;

            rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
            rigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
            ContactPoint2D contact = collision.contacts[0]; //�浹�� 0��° ����Ʈ�� ������

            Vector2 pos = contact.point;    //�浹 ����
            Vector2 normal = contact.normal;    //�浹���� ��ֶ���¡



            if (normal.y == 1)
            { //��
                //rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;


                StartCoroutine(Timer());
                tilemapPos += new Vector3Int(0, 1, 0);  //����ǥ�� ����, ������

                box_location = tilemap.GetCellCenterWorld(tilemapPos) + new Vector3(0f, 0.035f, 0f);    //������ ����ǥ�� ����� ��ȯ +  ��¦ ���� ����



                if (upRay.collider == null)
                {
                    StartCoroutine(MoveTo_Horizontal(transform.position, box_location));
                }

                //transform.position = box_location;





            }
            else if (normal.y == -1)
            {
                //��
                tilemapPos += new Vector3Int(0, -1, 0);
                box_location = tilemap.GetCellCenterWorld(tilemapPos) + new Vector3(0f, 0.035f, 0f);    //������ ����ǥ�� ����� ��ȯ +  ��¦ ���� ����



                if (downRay.collider == null)
                {
                    StartCoroutine(MoveTo_Horizontal(transform.position, box_location));
                }

                //transform.position = box_location;

            }
            if (normal.x == -1)
            {
                //��
                tilemapPos += new Vector3Int(-1, 0, 0);
                box_location = tilemap.GetCellCenterWorld(tilemapPos) + new Vector3(0f, 0.035f, 0f);    //������ ����ǥ�� ����� ��ȯ +  ��¦ ���� ����



                if (leftRay.collider == null)
                {
                    StartCoroutine(MoveTo_Vertical(transform.position, box_location));
                }

                //transform.position = box_location;

            }
            else if (normal.x == 1)
            {
                //��   
                tilemapPos += new Vector3Int(1, 0, 0);
                box_location = tilemap.GetCellCenterWorld(tilemapPos) + new Vector3(0f, 0.035f, 0f);    //������ ����ǥ�� ����� ��ȯ +  ��¦ ���� ����



                if (rightRay.collider == null)
                {
                    StartCoroutine(MoveTo_Vertical(transform.position, box_location));
                }

                //transform.position = box_location;
            }


        }





    }
    private void OnCollisionExit2D(Collision2D collision)
    {

        Debug.Log("ȣ���");
           
       
       
       // rigidBody.velocity = new Vector3(rigidBody.velocity.x, 0f, 0f);
      
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Water"))
        {
            Destroy(gameObject);
        }
    }


    bool CheckHitWall_Horizontal(Vector3 movement)
    {
        movement = transform.TransformDirection(movement);

        float scope = 0.1f;

        List<Vector3> rayPositions = new List<Vector3>();

        rayPositions.Add(transform.position + Vector3.up * 0.1f);
        rayPositions.Add(transform.position + Vector3.up * collider.size.y * 0.5f);
        rayPositions.Add(transform.position + Vector3.up * collider.size.y);

        foreach (Vector3 pos in rayPositions)
        {
            Debug.DrawRay(pos, movement * scope, Color.red);
        }

        foreach (Vector3 pos in rayPositions)
        {
            if (Physics.Raycast(pos, movement, out RaycastHit hit, scope))
            {
                if (hit.collider.CompareTag("Wall") || hit.collider.CompareTag("Block"))
                {
                    return true;
                }
            }
        }
        return false;

    }


    bool CheckHitWall_Vertical(Vector3 movement)
    {
        movement = transform.TransformDirection(movement);

        float scope = 0.1f;

        List<Vector3> rayPositions = new List<Vector3>();

        rayPositions.Add(transform.position + Vector3.right * 0.1f);
        rayPositions.Add(transform.position + Vector3.right * collider.size.x * 0.5f);
        rayPositions.Add(transform.position + Vector3.right * collider.size.x);

        foreach (Vector3 pos in rayPositions)
        {
            Debug.DrawRay(pos, movement * scope, Color.red);
        }

        foreach (Vector3 pos in rayPositions)
        {
            if (Physics.Raycast(pos, movement, out RaycastHit hit, scope))
            {
                if (hit.collider.CompareTag("Wall") || hit.collider.CompareTag("Block"))
                {
                    return true;
                }
            }
        }
        return false;

    }



    private IEnumerator MoveTo_Vertical(Vector3 Current, Vector3 Direction)
    {

        float distance = 0.5f;
        float cellsize = 0.65f;
        Vector3Int Position;
        while (transform.position.x <= Current.x + cellsize)
        {
            if (transform.position.x >= Current.x + cellsize)
            {

                Position = tilemap.WorldToCell(transform.position);
                transform.position = tilemap.GetCellCenterWorld(Position) + new Vector3(0f, 0.035f, 0f); ;
                yield break;
            }
            distance += 0.5f * Time.deltaTime;
            transform.position = Vector3.Lerp(Current, Destination, distance);
            //transform.position += new Vector3(0f, 0.01f, 0f);
            yield return null;

        }


        distance = 0.5f;


    }

    private IEnumerator MoveTo_Horizontal(Vector3 Current, Vector3 Destination)
    {
        float distance = 0.5f;
        float cellsize = 0.65f;
        Vector3Int Position;
        while (transform.position.y <= Current.y + cellsize)
        {
            if(transform.position.y >= Current.y+cellsize)
            {

                Position = tilemap.WorldToCell(transform.position);
                transform.position = tilemap.GetCellCenterWorld(Position) + new Vector3(0f, 0.035f, 0f); ;
                yield break;
            }
            distance += 0.5f * Time.deltaTime;
            transform.position = Vector3.Lerp(Current, Destination, distance);
            //transform.position += new Vector3(0f, 0.01f, 0f);
            yield return null;
           
        }


        distance = 0.5f;
    }

    private IEnumerator Timer()
    {
       
        //rigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
        yield return new WaitForSeconds(2f);


        //rigidBody.constraints = RigidbodyConstraints2D.None;
        //rigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
        yield break;
    }

}
