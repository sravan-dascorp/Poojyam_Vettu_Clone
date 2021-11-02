using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour
{

    public bool isOponentAI{get => playerTurn == PlayerTurn.AI;}
    // Player turn


    [SerializeField] Transform AvatarListPrefab;


    [SerializeField] Image player1_Image;
    [SerializeField] Image player2_image;
    Animator player1_imageanimator;
    Animator player2_imageanimator;




    [SerializeField] Text  show_turn;

    [SerializeField] Text show_player1_Score;
    [SerializeField] Text show_player2_Score;
    [SerializeField] Text show_winner;

    private PlayerTurn playerTurn;
    static int player1_score;
    static int player2_score;

    // state of sprite 
    // blue = player 1 , red = player 2

    [SerializeField] Sprite spr_play1_vertical ;
    [SerializeField] Color playerColor;
    [SerializeField] Sprite spr_play1_horizontal,spr_play2_vertical,spr_play2_horizontal,spr_play1_box;

    [SerializeField] Sprite spr_play2_box;
    int number_of_box = UserSettings.board_height * UserSettings.board_width;
    // Player Turn
    public enum PlayerTurn
    {
        PLAYER1,
        PLAYER2,
        AI
    }

    // Use this for initialization
    void Start()
    {
        player1_Image.sprite = AvatarListPrefab.GetChild(UserSettings.P1_AvatarIndex).GetComponent<SpriteRenderer>().sprite;
        player2_image.sprite = AvatarListPrefab.GetChild(UserSettings.P2_AvatarIndex).GetComponent<SpriteRenderer>().sprite;

        player1_imageanimator = player1_Image.GetComponent<Animator>();
        player2_imageanimator = player2_image.GetComponent<Animator>();


        player1_score = 0;
        player2_score = 0;

        show_player1_Score.text = "P 1 Score: " + player1_score;
        show_player2_Score.text = "P 2 Score: " + player2_score;

        
        if (Random.Range(0f, 1f) < 0.5f)
            playerTurn = PlayerTurn.PLAYER1;
        else
            playerTurn = PlayerTurn.PLAYER2;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTurn == PlayerTurn.PLAYER1)
        {
            show_turn.text = "Player 1's turn";
            show_turn.color = UserSettings.player1_color;
            player2_imageanimator.SetBool("isBlink", false);
            player1_imageanimator.SetBool("isBlink", true);


        }
        else if (playerTurn == PlayerTurn.PLAYER2)
        {
            show_turn.text = "Player 2's turn";
            show_turn.color = UserSettings.player2_color;
            player1_imageanimator.SetBool("isBlink", false);
            player2_imageanimator.SetBool("isBlink", true);

        }



        Gamelogic_();
        
        CheckWinner();

    }

    private void CheckWinner()
    {
        if (player1_score + player2_score == number_of_box)
        {
            show_turn.gameObject.SetActive(false);
            player1_imageanimator.enabled = false;
            player2_imageanimator.enabled = false;
            player1_Image.enabled = true;
            player2_image.enabled = true;

            if (player1_score == player2_score)
                show_winner.text = "DRAW GAME";
            else
            if (player1_score < player2_score)
                show_winner.text = "PLAYER 2 WIN";
            else
                show_winner.text = "PLAYER 1 WIN";
        }
    }

    // Player Move
    void Gamelogic_()
    {




        if (Input.GetMouseButtonDown(0))
        {

            float xCor = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
            float yCor = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;
            bool is_empty_vertical = false;
            bool is_empty_horizontal = false;
            bool extra_turn = false;

            
            //
            // float xCor = Input.mousePosition.x;
            // float yCor = Input.mousePosition.y;
            Debug.Log(xCor + "   " + yCor + " ");

            Vector2 origin = new Vector2(xCor, yCor);
            RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.zero, 0);

            if (hit.collider != null)
            {
                is_empty_vertical = hit.transform.gameObject.tag.Equals("empty vertical");
                is_empty_horizontal = hit.transform.gameObject.tag.Equals("empty horizontal");
                //GameSetup a ;
               

                // find index of transform in array

                

                if (is_empty_vertical || is_empty_horizontal)
                {
                    int x_index = 0;
                    int y_index = 0;

                    for (int y = 0; y <= UserSettings.board_height -1; y++)
                        for (int x = 0; x <= UserSettings.board_width -1; x++)
                        {

                            if (y != UserSettings.board_height && hit.transform == CreateBoard.vertical_line[x, y])
                            {
                                x_index = x;
                                y_index = y;
                            }
                            else
                            if (x != UserSettings.board_width && hit.transform == CreateBoard.horizontal_line[x, y])
                            {
                                x_index = x;
                                y_index = y;
                            }

                        }
                    //Debug.Log("hit");

                    //check rule;
                    if (this.playerTurn == PlayerTurn.PLAYER1)
                    {
                        //Debug.Log(x_index + "hit" + y_index);
                        
                        
                        if (is_empty_horizontal)
                        {
                            
                            hit.transform.gameObject.GetComponent<SpriteRenderer>().sprite = spr_play1_horizontal;
                            hit.transform.tag = "is activated";
                            
                            if (y_index != 0)
                            {
                                //Debug.Log(x_index + "hit" + y_index);
                                if (CheckTopBox(x_index,y_index))
                                {
                                    
                                    player1_score++;
                                    show_player1_Score.text = "Player1 Point: " + player1_score;
                                    CreateBoard.boxs[x_index, y_index - 1].GetComponent<SpriteRenderer>().sprite = spr_play1_box;
                                    CreateBoard.boxs[x_index, y_index - 1].tag = "mark box";
                                    extra_turn = true;
                                }
                                Debug.Log(x_index);

                            }
                            
                            if (y_index != UserSettings.board_height)
                            {
                                
                                if (CheckDownBox(x_index, y_index))
                                {
                                    
                                    player1_score++;
                                    show_player1_Score.text = "Player1 Point: " + player1_score;
                                    CreateBoard.boxs[x_index, y_index ].GetComponent<SpriteRenderer>().sprite = spr_play1_box;
                                    CreateBoard.boxs[x_index, y_index ].tag = "mark box";
                                    extra_turn = true;
                                }

                            }
                            
                            
                        }
                        else
                        if (is_empty_vertical)
                        {
                            
                            hit.transform.gameObject.GetComponent<SpriteRenderer>().sprite   = spr_play1_vertical;
                            hit.transform.tag = "is activated";

                            if (x_index != 0)
                            {
                                if (CheckLeftBox(x_index, y_index))
                                {
                                    
                                    player1_score++;
                                    show_player1_Score.text = "Player1 Point: " + player1_score;
                                    CreateBoard.boxs[x_index - 1, y_index].GetComponent<SpriteRenderer>().sprite = spr_play1_box;
                                    CreateBoard.boxs[x_index -1 , y_index].tag = "mark box";
                                    extra_turn = true;
                                }

                            }
                            if (x_index != UserSettings.board_width)
                            {
                                if (CheckRightBox(x_index, y_index))
                                {
                                    
                                    player1_score++;
                                    show_player1_Score.text = "Player1 Point: " + player1_score;
                                    CreateBoard.boxs[x_index, y_index].GetComponent<SpriteRenderer>().sprite = spr_play1_box;
                                    CreateBoard.boxs[x_index, y_index].tag = "mark box";
                                    extra_turn = true;
                                }

                            }
                        }
                       
                        if (!extra_turn)
                            playerTurn = PlayerTurn.PLAYER2;
                      // Debug.Log(playerTurn);

                    }
                    else if (this.playerTurn == PlayerTurn.PLAYER2)
                    {

                        Debug.Log(x_index + "hit" + y_index);
                        if (is_empty_horizontal)
                        {
                            hit.transform.gameObject.GetComponent<SpriteRenderer>().sprite = spr_play2_horizontal;
                            hit.transform.tag = "is activated";
                            if (y_index != 0)
                            {
                                if (CheckTopBox(x_index, y_index))
                                {
                                    
                                    player2_score++;
                                    show_player2_Score.text = "Player2 Point: " + player2_score;
                                    CreateBoard.boxs[x_index, y_index - 1].GetComponent<SpriteRenderer>().sprite = spr_play2_box;
                                    CreateBoard.boxs[x_index, y_index - 1].tag = "mark box";
                                    extra_turn = true;
                                }

                            }
                            if (y_index != UserSettings.board_height)
                            {
                                if (CheckDownBox(x_index, y_index))
                                {
                                    
                                    player2_score++;
                                    show_player2_Score.text = "Player2 Point: " + player2_score;
                                    CreateBoard.boxs[x_index, y_index].GetComponent<SpriteRenderer>().sprite = spr_play2_box;
                                    CreateBoard.boxs[x_index, y_index].tag = "mark box";
                                    extra_turn = true;
                                }

                            }

                        }
                        else
                       if (is_empty_vertical)
                        {
                            hit.transform.gameObject.GetComponent<SpriteRenderer>().sprite = spr_play2_vertical;
                            hit.transform.tag = "is activated";

                            if (x_index != 0)
                            {
                                if (CheckLeftBox(x_index, y_index))
                                {
                                    
                                    player2_score++;
                                    show_player2_Score.text = "Player2 Point: " + player2_score;
                                    CreateBoard.boxs[x_index - 1, y_index].GetComponent<SpriteRenderer>().sprite = spr_play2_box;
                                    CreateBoard.boxs[x_index - 1, y_index].tag = "mark box";
                                    extra_turn = true;
                                }

                            }
                            if (x_index != UserSettings.board_width)
                            {
                                if (CheckRightBox(x_index, y_index))
                                {
                                   
                                    player2_score++;
                                    show_player2_Score.text = "Player2 Point: " + player2_score;
                                    CreateBoard.boxs[x_index, y_index].GetComponent<SpriteRenderer>().sprite = spr_play2_box;
                                    CreateBoard.boxs[x_index, y_index].tag = "mark box";
                                    extra_turn = true;
                                }

                            }
                        }
                        if (!extra_turn)
                            playerTurn = PlayerTurn.PLAYER1;
                    }
                }
            }
        }
    }

    //check make box
    bool CheckRightBox(int ver_x_index, int ver_y_index)
    {
        bool top, down, right;
        bool result = false;
        top = CreateBoard.horizontal_line[ver_x_index,ver_y_index].CompareTag("is activated");
        down = CreateBoard.horizontal_line[ver_x_index, ver_y_index + 1].CompareTag("is activated");
        right = CreateBoard.vertical_line[ver_x_index + 1, ver_y_index].CompareTag("is activated");
        if (top && down && right)
            result = true;
        return (false || result);
    }

    bool CheckLeftBox(int ver_x_index, int ver_y_index)
    {
        bool top, down, left;
        bool result = false;
        top = CreateBoard.horizontal_line[ver_x_index - 1, ver_y_index].CompareTag("is activated");
        down = CreateBoard.horizontal_line[ver_x_index - 1, ver_y_index + 1].CompareTag("is activated");
        left = CreateBoard.vertical_line[ver_x_index - 1, ver_y_index].CompareTag("is activated");
        if (top && down && left)
            result = true;
        return (false || result);
    }

    bool CheckTopBox(int hor_x_index, int hor_y_index)
    {
        bool top, right, left;
        bool result = false;
        top = CreateBoard.horizontal_line[hor_x_index , hor_y_index - 1].CompareTag("is activated");
        left = CreateBoard.vertical_line[hor_x_index , hor_y_index - 1].CompareTag("is activated");
        right = CreateBoard.vertical_line[hor_x_index + 1, hor_y_index -1].CompareTag("is activated");
        if (top && right && left)
            result = true;
        return (false || result);
    }

    bool CheckDownBox(int hor_x_index, int hor_y_index)
    {
        bool down, right, left;
        bool result = false;
        down = CreateBoard.horizontal_line[hor_x_index, hor_y_index + 1].CompareTag("is activated");
        left = CreateBoard.vertical_line[hor_x_index, hor_y_index ].CompareTag("is activated");
        right = CreateBoard.vertical_line[hor_x_index + 1, hor_y_index].CompareTag("is activated");
        if (down && right && left)
            result = true;
        return (false || result);
    }






}

    

   

