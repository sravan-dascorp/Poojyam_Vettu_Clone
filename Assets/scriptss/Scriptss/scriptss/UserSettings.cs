 using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public static  class UserSettings {   //thid class saves  data across scenes

     public static int  board_width = 4;
    public static int  board_height = 4;

    public static Color player1_color = Color.blue;
    public static Color player2_color = Color.red;

    public static int P1_AvatarIndex=0;
    public static int P2_AvatarIndex=1;


    
   
    
   
    // public int max_num = 6;
    


    // // Use this for initialization
    // void Start () {
	
	// }
	
	// // Update is called once per frame
	// void Update () {
	
	// }
    // public void ChangeScene(int scene_id)
    // {
    //     CreateBoard.board_Width = Convert.ToInt32(input_board_width.text);
    //     CreateBoard.board_Height = Convert.ToInt32(input_board_height.text);
    //     if (CreateBoard.board_Width < 1 || CreateBoard.board_Height < 1)
    //         alert.text = ("Invalid number");
    //     else
    //     if (CreateBoard.board_Width > max_num || CreateBoard.board_Height > max_num)
    //         alert.text = ("Maximum number is " + max_num);
    //     else
    //         SceneManager.LoadScene(scene_id);
    // }
}
