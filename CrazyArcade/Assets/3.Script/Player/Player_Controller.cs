using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;


public enum GameMode
{
    Solo = 0,
    Multi,
}


public class Player_Controller : MonoBehaviour
{
    [SerializeField]
    private MapSizeData mapSize;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private Texture2D Mouse_Image;

    [SerializeField]
    private GameObject[] StartPoints;



    private Village_Controller village_Controller;


    private float hori;
    private float verti;

    public float MoveSpeed = 0.01f;
    public float RightMoveSpeed = 5f;
    public float LeftMoveSpeed = 5f;
    public float UpMoveSpeed = 5f;
    public float DownMoveSpeed = 0.01f;

    public float MaxSpeed = 1f;

    public bool isDead;
    public bool isInBush;
    public bool isInBubble = false;

    private bool isKeyDown = false;

   
    private KeyCode PressedKey;


    

  

    private SpriteRenderer spriteRenderer;

    private bool isUp;
    private bool isDown;

    private bool isRight;
    private bool isLeft;



    private BoxCollider2D boxCollider;
    private Rigidbody2D rigidBody;


    //물풍선 관련
    [SerializeField]
    GameObject Bomb_Prefabs;


    [SerializeField]
    private Tilemap tilemap;
    [SerializeField]
    private Grid grid;

   
    public Vector3 CurrentPosition;
    public Vector3Int CurrentCellPosition;
    public Vector3 bomb_location; 
    public Vector3Int tilemapPos;   //Bubble.cs에 넘겨줄 버블 중앙 위치 변수
    public Queue<Vector3Int> boom_positions;

    public int WaterLength; //물줄기 길이

    public int MaxBallonCount;    //한번에 설치할 수 있는 최대 물풍선 갯수
    public int BallonCount;  //현재 사용 가능한 물풍선 갯수

    GameMode current_GameMode;

    private string CharacterName;

    //오디오
    [SerializeField]
    private AudioSource audio;

    public AudioClip Clip_dead;
    public AudioClip Clip_setBomb;
    public AudioClip Clip_getItem;
    public AudioClip Clip_explosion;
    public AudioClip Clip_bush;
    public AudioClip Clip_Stuck;


    void Start()
    {

    


        //게임모드 정하기
        //1p, 2p랜덤 정하기

        //current_GameMode = GameManager.Instance.CurrentGameMode;

        current_GameMode = GameMode.Multi;
        //current_GameMode = GameMode.Solo;

       


        tilemap = FindObjectOfType<Tilemap>();
        PressedKey = KeyCode.None;
        
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        boxCollider = GetComponent<BoxCollider2D>();
        rigidBody = GetComponent<Rigidbody2D>();

        rigidBody.useAutoMass = true;
        rigidBody.gravityScale = 0f;

        boom_positions = new Queue<Vector3Int>();

        Cursor.SetCursor(Mouse_Image, Vector2.zero, CursorMode.Auto);

        village_Controller = FindObjectOfType<Village_Controller>();

        //audio = FindObjectOfType<Camera>().GetComponent<AudioSource>();
        audio = GetComponent<AudioSource>();
        audio.volume = 0.4f;

        Get_StartPoint(Random.Range(1, 4));
       // transform.position = new Vector3(-0.9f, -1.5f, 0);

        //switch (GameManager.Instance.p1_selected_character)
        //{
        //    case "Dizni":
        //        MaxBallonCount = 2;
        //        WaterLength = 0;
        //        break;

        //    case "Bazzi":
        //        MaxBallonCount = 1;
        //        WaterLength = 0;
        //        break;

        //    case "Dao":
        //        MaxBallonCount = 1;
        //        WaterLength = 1;
        //        break;
        //}
         isDead = false;


        MaxBallonCount = 2;
        WaterLength = 1;

        BallonCount = 0;

    

    }

    public void Get_StartPoint(int num)
    {
        switch(num)
        {
            case 1:
                transform.position = new Vector3(-5.544f, 4.228f, 0f);

                break;

            case 2:
                transform.position = new Vector3(2.94f, 3.59f, 0f);
                break;

            case 3:
                transform.position = new Vector3(3.57f, -3.6f, 0f);
                break;

            case 4:
                transform.position = new Vector3(-4.87f, -2.95f, 0f);
                break;
        }
    }



    // Update is called once per frame
    void Update()
    {

        
        
        RigidBody_Move(current_GameMode);
        Bubble(current_GameMode);
        
        //MoveCharacter();

    }


    private void LateUpdate()
    {
        //플레이어가 화면 범위 밖으로 나가지 않도록 설정
        transform.position = new Vector3
            (
            Mathf.Clamp(transform.position.x, mapSize.LimitMin.x, mapSize.LimitMax.x),
            Mathf.Clamp(transform.position.y, mapSize.LimitMin.y, mapSize.LimitMax.y),
            0
            );
    }


    private void FixedUpdate()
    {

        CurrentCellPosition = tilemap.WorldToCell(CurrentPosition);
      
        layer_order();
    }

    private void RigidBody_Move(GameMode currentMode)
    {

        switch (currentMode)
        {
            case GameMode.Solo:

                if (isDead == false)
                {
                    if (Input.GetKey(KeyCode.UpArrow))
                    {
                        animator.SetBool("isUp", true);
                        rigidBody.AddForce(Vector2.up * MoveSpeed, ForceMode2D.Impulse);
                        rigidBody.velocity = new Vector2(rigidBody.velocity.x, Mathf.Min(rigidBody.velocity.y, MaxSpeed));
                    }
                    else if (Input.GetKey(KeyCode.DownArrow))
                    {
                        animator.SetBool("isDown", true);
                        rigidBody.AddForce(Vector2.down * MoveSpeed, ForceMode2D.Impulse);
                        rigidBody.velocity = new Vector2(rigidBody.velocity.x, Mathf.Max(rigidBody.velocity.y, -MaxSpeed));
                    }
                    else if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow))
                    {
                        if (Input.GetKeyUp(KeyCode.UpArrow))
                        {
                            animator.SetBool("isUp", false);
                        }
                        else
                        {
                            animator.SetBool("isDown", false);
                        }
                        rigidBody.velocity = new Vector3(rigidBody.velocity.x, 0f, 0f);
                    }


                    if (Input.GetKey(KeyCode.LeftArrow))
                    {
                        animator.SetBool("isLeft", true);
                        rigidBody.AddForce(Vector2.left * MoveSpeed, ForceMode2D.Impulse);
                        rigidBody.velocity = new Vector2(Mathf.Max(rigidBody.velocity.x, -MaxSpeed), rigidBody.velocity.y);
                        //transform.localScale = new Vector3(-1f, 1f, 1f);
                    }
                    else if (Input.GetKey(KeyCode.RightArrow)) // 오른쪽 화살표를 누르고 있는 경우
                    {
                        animator.SetBool("isRight", true);
                        rigidBody.AddForce(Vector2.right * MoveSpeed, ForceMode2D.Impulse);
                        rigidBody.velocity = new Vector2(Mathf.Min(rigidBody.velocity.x, MaxSpeed), rigidBody.velocity.y);
                        //transform.localScale = new Vector3(1f, 1f, 1f);
                    }
                    else if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
                    {
                        if (Input.GetKeyUp(KeyCode.LeftArrow))
                        {
                            animator.SetBool("isLeft", false);
                        }
                        else
                        {
                            animator.SetBool("isRight", false);
                        }
                        rigidBody.velocity = new Vector3(0f, rigidBody.velocity.y, 0f);
                    }



                    // CurrentPosition = transform.position;
                    CurrentPosition = new Vector3(rigidBody.position.x, rigidBody.position.y - 0.3f, 0f);   //물풍선 쏘는 위치
                }
                break;




            case GameMode.Multi:
                if (gameObject == GameManager.Instance.Player1)
                {
                    if (isDead == false)
                    {
                        if (Input.GetKey(KeyCode.UpArrow))
                        {
                            animator.SetBool("isUp", true);
                            rigidBody.AddForce(Vector2.up * MoveSpeed, ForceMode2D.Impulse);
                            rigidBody.velocity = new Vector2(rigidBody.velocity.x, Mathf.Min(rigidBody.velocity.y, MaxSpeed));
                        }
                        else if (Input.GetKey(KeyCode.DownArrow))
                        {
                            animator.SetBool("isDown", true);
                            rigidBody.AddForce(Vector2.down * MoveSpeed, ForceMode2D.Impulse);
                            rigidBody.velocity = new Vector2(rigidBody.velocity.x, Mathf.Max(rigidBody.velocity.y, -MaxSpeed)); 
                        }
                        else if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow))
                        {
                            if (Input.GetKeyUp(KeyCode.UpArrow))
                            {
                                animator.SetBool("isUp", false);
                            }
                            else
                            {
                                animator.SetBool("isDown", false);
                            }
                            rigidBody.velocity = new Vector3(rigidBody.velocity.x, 0f, 0f);
                        }


                        if (Input.GetKey(KeyCode.LeftArrow))
                        {
                            animator.SetBool("isLeft", true);
                            rigidBody.AddForce(Vector2.left * MoveSpeed, ForceMode2D.Impulse);
                            rigidBody.velocity = new Vector2(Mathf.Max(rigidBody.velocity.x, -MaxSpeed), rigidBody.velocity.y);
                            //transform.localScale = new Vector3(-1f, 1f, 1f);
                        }
                        else if (Input.GetKey(KeyCode.RightArrow)) // 오른쪽 화살표를 누르고 있는 경우
                        {
                            animator.SetBool("isRight", true);
                            rigidBody.AddForce(Vector2.right * MoveSpeed, ForceMode2D.Impulse);
                            rigidBody.velocity = new Vector2(Mathf.Min(rigidBody.velocity.x, MaxSpeed), rigidBody.velocity.y);
                            //transform.localScale = new Vector3(1f, 1f, 1f);
                        }
                        else if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
                        {
                            if (Input.GetKeyUp(KeyCode.LeftArrow))
                            {
                                animator.SetBool("isLeft", false);
                            }
                            else
                            {
                                animator.SetBool("isRight", false);
                            }
                            rigidBody.velocity = new Vector3(0f, rigidBody.velocity.y, 0f);
                        }



                        // CurrentPosition = transform.position;
                        CurrentPosition = new Vector3(rigidBody.position.x, rigidBody.position.y - 0.3f, 0f);
                    }
                }
                else
                {
                    if (isDead == false)
                    {
                        if (Input.GetKey(KeyCode.W))
                        {
                            animator.SetBool("isUp", true);
                            rigidBody.AddForce(Vector2.up * MoveSpeed, ForceMode2D.Impulse);
                            rigidBody.velocity = new Vector2(rigidBody.velocity.x, Mathf.Min(rigidBody.velocity.y, MaxSpeed));
                        }
                        else if (Input.GetKey(KeyCode.S))
                        {
                            animator.SetBool("isDown", true);
                            rigidBody.AddForce(Vector2.down * MoveSpeed, ForceMode2D.Impulse);
                            rigidBody.velocity = new Vector2(rigidBody.velocity.x, Mathf.Max(rigidBody.velocity.y, -MaxSpeed));
                        }
                        else if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S))
                        {
                            if (Input.GetKeyUp(KeyCode.W))
                            {
                                animator.SetBool("isUp", false);
                            }
                            else
                            {
                                animator.SetBool("isDown", false);
                            }
                            rigidBody.velocity = new Vector3(rigidBody.velocity.x, 0f, 0f);
                        }


                        if (Input.GetKey(KeyCode.A))
                        {
                            animator.SetBool("isLeft", true);
                            rigidBody.AddForce(Vector2.left * MoveSpeed, ForceMode2D.Impulse);
                            rigidBody.velocity = new Vector2(Mathf.Max(rigidBody.velocity.x, -MaxSpeed), rigidBody.velocity.y);
                            //transform.localScale = new Vector3(-1f, 1f, 1f);
                        }
                        else if (Input.GetKey(KeyCode.D)) // 오른쪽 화살표를 누르고 있는 경우
                        {
                            animator.SetBool("isRight", true);
                            rigidBody.AddForce(Vector2.right * MoveSpeed, ForceMode2D.Impulse);
                            rigidBody.velocity = new Vector2(Mathf.Min(rigidBody.velocity.x, MaxSpeed), rigidBody.velocity.y);
                            //transform.localScale = new Vector3(1f, 1f, 1f);
                        }
                        else if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
                        {
                            if (Input.GetKeyUp(KeyCode.A))
                            {
                                animator.SetBool("isLeft", false);
                            }
                            else
                            {
                                animator.SetBool("isRight", false);
                            }
                            rigidBody.velocity = new Vector3(0f, rigidBody.velocity.y, 0f);
                        }



                        // CurrentPosition = transform.position;
                        CurrentPosition = new Vector3(rigidBody.position.x, rigidBody.position.y - 0.3f, 0f);
                    }
                }
                break;
        }
     
     
    }


    private void Bubble(GameMode currentMode)
    {
        switch(currentMode)
        {
            case GameMode.Solo:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    
                    if (BallonCount < MaxBallonCount && isDead == false)
                    {
                        audio.clip = Clip_setBomb;
                        audio.Play();
                        tilemapPos = tilemap.WorldToCell(CurrentPosition);
                        // boom_positions.Enqueue(tilemapPos);
                        bomb_location = tilemap.GetCellCenterLocal(tilemapPos);
                        Instantiate(Bomb_Prefabs, bomb_location, Quaternion.identity);
                        BallonCount += 1;
                    }

                }
                break;

            case GameMode.Multi:
                if(gameObject == GameManager.Instance.Player1)
                {
                    if (Input.GetKeyDown(KeyCode.RightShift))
                    {

                        if (BallonCount < MaxBallonCount && isDead == false)
                        {
                            audio.clip = Clip_setBomb;
                            audio.Play();
                            tilemapPos = tilemap.WorldToCell(CurrentPosition);
                            // boom_positions.Enqueue(tilemapPos);
                            bomb_location = tilemap.GetCellCenterLocal(tilemapPos);
                            Instantiate(Bomb_Prefabs, bomb_location, Quaternion.identity);
                            BallonCount += 1;
                        }

                    }
                }
                else
                {
                    if(Input.GetKeyDown(KeyCode.LeftShift))
                    {
                        if (BallonCount < MaxBallonCount && isDead == false)
                        {
                            audio.clip = Clip_setBomb;
                            audio.Play();
                            tilemapPos = tilemap.WorldToCell(CurrentPosition);
                            // boom_positions.Enqueue(tilemapPos);
                            bomb_location = tilemap.GetCellCenterLocal(tilemapPos);
                            Instantiate(Bomb_Prefabs, bomb_location, Quaternion.identity);
                            BallonCount += 1;
                        }
                    }
                }
                break;
        }

      
      
    }



    private void layer_order()
    {
        if (!isInBush)
        {
            switch (CurrentCellPosition.y)
            {
                case -6:
                    spriteRenderer.sortingOrder = 13;
                    break;
                case -5:
                    spriteRenderer.sortingOrder = 12;
                    break;
                case -4:
                    spriteRenderer.sortingOrder = 11;
                    break;
                case -3:
                    spriteRenderer.sortingOrder = 10;
                    break;
                case -2:
                    spriteRenderer.sortingOrder = 9;
                    break;
                case -1:
                    spriteRenderer.sortingOrder = 8;
                    break;
                case 0:
                    spriteRenderer.sortingOrder = 7;
                    break;
                case 1:
                    spriteRenderer.sortingOrder = 6;
                    break;
                case 2:
                    spriteRenderer.sortingOrder = 5;
                    break;
                case 3:
                    spriteRenderer.sortingOrder = 4;
                    break;
                case 4:
                    spriteRenderer.sortingOrder = 3;
                    break;
                case 5:
                    spriteRenderer.sortingOrder = 2;
                    break;

                case 6:
                    spriteRenderer.sortingOrder = 1;
                    break;

            }
        }
        else
        {
            spriteRenderer.sortingOrder = 0;
        }



    }


    #region transform을 이용한 이동
    private void Move()
    {
        CurrentPosition = rigidBody.position;
        if (Input.GetKey(KeyCode.UpArrow))
        {
            rigidBody.MovePosition(new Vector2(CurrentPosition.x, CurrentPosition.y + 0.1f)) ;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            rigidBody.MovePosition(new Vector2(CurrentPosition.x, CurrentPosition.y - 0.1f));
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rigidBody.MovePosition(new Vector2(CurrentPosition.x-0.1f, CurrentPosition.y));
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            rigidBody.MovePosition(new Vector2(CurrentPosition.x+ 0.1f, CurrentPosition.y));
        }

    }



    private void MoveCharacter()
    {
        //입력받는 방향키가 있으면
        //그방향으로의 이동은 멈추기




        #region 
        //if (Input.GetKey(KeyCode.UpArrow))
        //{
        //    Debug.Log("pressed key : 상");
        //    //if (PressedKey == KeyCode.None)
        //    //{
        //    //    PressedKey = KeyCode.UpArrow;
        //    //}     
        //    move_Up();

        //}
        //else
        //{
        //    animator.SetBool("isUp", false);
        //    PressedKey = KeyCode.None;
        //    Debug.Log("pressed key : 없음");
        //}


        //if (Input.GetKey(KeyCode.DownArrow))
        //{
        //    Debug.Log("pressed key : 하");
        //    //if (PressedKey == KeyCode.None)
        //    //{
        //    //    PressedKey = KeyCode.DownArrow;
        //    //}
        //    move_Down();


        //}
        //else
        //{
        //    animator.SetBool("isDown", false);
        //    PressedKey = KeyCode.None;
        //    Debug.Log("pressed key : 없음");
        //}



        //if (Input.GetKey(KeyCode.LeftArrow))
        //{
        //    Debug.Log("pressed key : 좌");
        //    PressedKey = KeyCode.LeftArrow;
        //    move_Left();

        //}
        //else
        //{
        //    PressedKey = KeyCode.None;
        //    Debug.Log("pressed key : 없음");

        //    animator.SetBool("isLeft", false);
        //}

        //if (Input.GetKey(KeyCode.RightArrow))
        //{
        //    Debug.Log("pressed key : 우");
        //    PressedKey = KeyCode.RightArrow;
        //    move_Right();


        //}
        //else
        //{
        //    Debug.Log("pressed key : 없음");
        //    PressedKey = KeyCode.None;

        //    animator.SetBool("isRight", false);

        //}

        #endregion

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0f);
        transform.position += movement * MoveSpeed * Time.deltaTime;

        if (CheckHitWall_Horizontal(movement) ||CheckHitWall_Vertical(movement))
        {
            transform.position = Vector3.zero;
        }


        CurrentPosition = transform.position;

    }

    private void move_Up()
    {
        CurrentPosition.y += MoveSpeed * Time.deltaTime;
        transform.position = CurrentPosition;
        animator.SetBool("isUp", true);

        ////먼저 눌린 키가 있으면
        //if (PressedKey != KeyCode.None && PressedKey != KeyCode.UpArrow)
        //{
        //    //걍 이동
        //    animator.SetBool("isUp", true);
        //    CurrentPosition.y += MoveSpeed * Time.deltaTime;
        //    transform.position = CurrentPosition;
        //}
        ////눌린키가 본인이면
        //else
        //{
        //    animator.SetBool("isUp", true);
        //    CurrentPosition.y += MoveSpeed * Time.deltaTime;
        //    transform.position = CurrentPosition;
        //   // 본인이 먼저 눌린 키인데 다른 키 입력이 들어오면
        //    if (Input.GetKey(KeyCode.DownArrow))
        //    {
        //        animator.SetBool("isUp", false);
        //        move_Down();
        //    }
        //}


    }


    private void move_Down()
    {

        CurrentPosition.y -= MoveSpeed * Time.deltaTime;
        transform.position = CurrentPosition;
        animator.SetBool("isDown", true);


        ////먼저 눌린 키가 있으면
        //if (PressedKey != KeyCode.None && PressedKey != KeyCode.DownArrow)
        //{
        //    //걍 이동
        //    animator.SetBool("isDown", true);
        //    CurrentPosition.y -= MoveSpeed * Time.deltaTime;
        //    transform.position = CurrentPosition;
        //}
        ////본인이 먼저 눌린 키인데 다른 키 입력이 들어오면
        //else 
        //{ 
        //    animator.SetBool("isDown", true);
        //    CurrentPosition.y -= MoveSpeed * Time.deltaTime;
        //    transform.position = CurrentPosition;
        //    if (Input.GetKey(KeyCode.UpArrow))
        //    {
        //        animator.SetBool("isDown", false);
        //        move_Up();
        //    }
        //}



    }



    private void move_Left()
    {
        CurrentPosition.x -= MoveSpeed * Time.deltaTime;
        transform.position = CurrentPosition;
        animator.SetBool("isLeft", true);
    }



    private void move_Right()
    {
        CurrentPosition.x += MoveSpeed * Time.deltaTime;
        transform.position = CurrentPosition;
        animator.SetBool("isRight", true);
    }
#endregion


    //벽 or 물풍선에 막힘 (Raycast 함수)
    bool CheckHitWall_Horizontal(Vector3 movement)
    {
        movement = transform.TransformDirection(movement);

        float scope = 0.1f;

        List<Vector3> rayPositions = new List<Vector3>();

        rayPositions.Add(transform.position + Vector3.up * 0.1f);
        rayPositions.Add(transform.position + Vector3.up * boxCollider.size.y * 0.5f);
        rayPositions.Add(transform.position + Vector3.up * boxCollider.size.y);

        foreach (Vector3 pos in rayPositions)
        {
            Debug.DrawRay(pos, movement * scope, Color.red);
        }

        foreach (Vector3 pos in rayPositions)
        {
            if(Physics.Raycast(pos, movement, out RaycastHit hit, scope))
            {
                if(hit.collider.CompareTag("Wall") || hit.collider.CompareTag("Block"))
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
        rayPositions.Add(transform.position + Vector3.right * boxCollider.size.x * 0.5f);
        rayPositions.Add(transform.position + Vector3.right * boxCollider.size.x);

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



    private void OnCollisionEnter2D(Collision2D collision)
    {
     
      
   

    }

    



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Water"))
        {
            
            Die();
        }

        if(collision.CompareTag("Bush"))
        {
            isInBush = true;
        }
      
        if(isInBubble)
        {
            boxCollider.isTrigger = true;
            if (collision.CompareTag("Player"))
            {
                
                Dead();
            }
        }
        else
        {
            boxCollider.isTrigger = false;
        }
      
    }

 
    private void OnTriggerExit2D(Collider2D collision)
    {
        isInBush = false;
       // boxCollider.isTrigger = true;

        
    }

    private IEnumerator Dead_Timer()
    {
        yield return new WaitForSeconds(5f);

        Dead();
    }


    private void Dead()
    {
        if(GameManager.Instance.Player1)
        {
            GameManager.Instance.is_p1Dead = true;
        }
        else
        {
            GameManager.Instance.is_p2Dead = true;
        }


        animator.SetBool("isTimeEnded", true);
        audio.clip = Clip_dead;
        audio.Play();
    }

    private void Die()
    {
        audio.clip = Clip_Stuck;
        audio.Play();
        isInBubble = true;
        MoveSpeed = 0.1f;
        MaxSpeed = 0.1f;
        isDead = true;
        animator.SetBool("isInBubble", true);
        StartCoroutine("Dead_Timer");
    }




    public void PlayAudio(int num)
    {
        switch(num)
        {
            case 1:
                audio.clip = Clip_explosion;
                audio.Play();
                break;

            case 2:
                audio.clip = Clip_bush;
                audio.Play();
                break;
        }
       
    }



}
