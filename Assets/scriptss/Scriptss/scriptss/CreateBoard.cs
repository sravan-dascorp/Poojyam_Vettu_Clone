using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateBoard : MonoBehaviour
{
     [SerializeField] List<BoardTemplate> boardTemplates = new List<BoardTemplate>();
    public int  boardTemplateindex=0;

      SpriteRenderer  verticalSprite,horizontalSprite,emptyboxSprite;
      Camera mainCam;

      //

     

    // public int getBoardHeight{ get => boardHeight;}
    // public int getBoardWidth{ get => boardHeight;}
   
public  static Transform[,] vertical_line;
    public  static Transform[,] horizontal_line;
    public  static Transform[,] boxs;



    private void Awake() {
        verticalSprite = boardTemplates[boardTemplateindex].Vertical;
        horizontalSprite = boardTemplates[boardTemplateindex].Horizontal;
        emptyboxSprite = boardTemplates[boardTemplateindex].emptyBox;
        mainCam = Camera.main;

    }

    // Start is called before the first frame update
    void Start()
    {
        int board_Width= UserSettings.board_width;
       int  board_Height = UserSettings.board_height;
        SetUpBoard(board_Width,board_Height);
    }

   
  
    //box and line dimention setUP
     void SetUpBoard(int board_Width, int board_Height)
    {


        float vertical_Line_Width = verticalSprite.bounds.size.x;
        float vertical_Line_Height = verticalSprite.bounds.size.y;

        float horizontal_Line_Width = horizontalSprite.bounds.size.x;
        float horizontal_Line_Height = horizontalSprite.bounds.size.y;

        float box_Width = emptyboxSprite.bounds.size.x;
        float box_Height = emptyboxSprite.bounds.size.y;


        // /////Debug.Log((board_Width * Screen.width / 2; + (board_Width + 1) * Screen.height / 2;) ///// 2);




        vertical_line = new Transform[board_Width + 1, board_Height];
        horizontal_line = new Transform[board_Width, board_Height + 1];
        boxs = new Transform[board_Width, board_Height];
       
       
        float startX = Screen.width / 2f;
        float startY = Screen.height / 2f;
       // Debug.Log((board_Width * pix_horizontal_width + (board_Width + 1) * pix_vertical_width) / 2);


        //initial game object structure

        vertical_line = new Transform[board_Width + 1, board_Height];
        horizontal_line = new Transform[board_Width, board_Height + 1];
        boxs = new Transform[board_Width, board_Height];



        float yCor = mainCam.ScreenToWorldPoint(new Vector3(0, startY, 0f)).y;
        // middle screen
        yCor = yCor + (board_Height * vertical_Line_Height + (board_Height + 1) * horizontal_Line_Height) / 2;
        for (int y = 0; y <= board_Height; y++)
        {
            float xCor = mainCam.ScreenToWorldPoint(new Vector3(startX, 0, 0f)).x;
            //middle screen
            xCor = xCor - (board_Width * horizontal_Line_Width + (board_Width + 1) * vertical_Line_Width) / 2;
           // check.text = "x =  " + xCor + " y = " + yCor;
            for (int x = 0; x <= board_Width; x++)
            {
                Vector3 center_of_Box = new Vector3(xCor, yCor, 0f);
                Vector3 center_of_Vertical = new Vector3(xCor - horizontal_Line_Width / 2 - vertical_Line_Width / 2, yCor, 0f);
                Vector3 center_of_Horizontal = new Vector3(xCor, yCor + horizontal_Line_Height / 2 + vertical_Line_Height / 2, 0f);

               if (x != board_Width && y != board_Height)
                      boxs[x, y] = Instantiate(emptyboxSprite.transform, center_of_Box, Quaternion.identity) as Transform;
               
               if (y != board_Height)
              
                    vertical_line[x, y] = Instantiate(verticalSprite.transform, center_of_Vertical, Quaternion.identity) as Transform;
                    
           
                      
               if (x != board_Width)
                      horizontal_line[x, y] = Instantiate(horizontalSprite.transform, center_of_Horizontal, Quaternion.identity) as Transform;
               
               xCor += box_Width + vertical_Line_Width;
            }
            yCor -= box_Height + horizontal_Line_Height;
        }
    }
       
       
      //  CreatePlayBoard(_boardWidth, _boardHeight, vertical_Line_Width, vertical_Line_Height, horizontal_Line_Width, horizontal_Line_Height, box_Width, box_Height);

    }

    
    
    
    
//     private void CreatePlayBoard(int board_Width, int board_Height, float vertical_Line_Width, float vertical_Line_Height, float horizontal_Line_Width, float horizontal_Line_Height, float box_Width, float box_Height)
//     {
//         float startX = Screen.width / 2;
//         float startY = Screen.height / 2;
//         float yPosition = mainCam.ScreenToWorldPoint(new Vector3(0, startY, 0f)).y;
//         // middle screen
//         yPosition = yPosition + (board_Height * vertical_Line_Height + (board_Height + 1) * horizontal_Line_Height) / 2;
//         for (int y = 0; y <= board_Height; y++)
//         {
//             float xPosition = mainCam.ScreenToWorldPoint(new Vector3(startX, 0, 0f)).x;
//             //middle screen
//             xPosition = xPosition - (board_Width * horizontal_Line_Width + (board_Width + 1) * vertical_Line_Width) / 2;
//             // check.text = "x =  " + xCor + " y = " + yCor;
//             for (int x = 0; x <= board_Width; x++)
//             {
//                 Vector3 center_of_Box = new Vector3(xPosition, yPosition, 0f);
//                 Vector3 center_of_Vertical = new Vector3(xPosition - horizontal_Line_Width / 2 - vertical_Line_Width / 2, xPosition, 0f);
//                 Vector3 center_of_Horizontal = new Vector3(xPosition, yPosition + horizontal_Line_Height / 2 + vertical_Line_Height / 2, 0f);

//                 if (x != board_Width && y != board_Height)
//                     boxs[x, y] = Instantiate(emptyboxSprite.transform, center_of_Box, Quaternion.identity) as Transform;

//                 if (y != board_Height)

//                     vertical_line[x, y] = Instantiate(verticalSprite.transform, center_of_Vertical, Quaternion.identity) as Transform;



//                 if (x != board_Width)
//                     horizontal_line[x, y] = Instantiate(horizontalSprite.transform, center_of_Horizontal, Quaternion.identity) as Transform;

//                 xPosition += box_Width + vertical_Line_Width;
//             }
//             xPosition -= box_Height + horizontal_Line_Height;
//         }
//     }
    
