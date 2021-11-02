using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public   class MainMenuOption : MonoBehaviour
{
    public Dropdown input_Rows;
    public Dropdown input_Colums;

    public Dropdown player1_Avatar;
    public Dropdown player2_Avatar;

    public GameObject spr_player1Avatar;
    public GameObject spr_player2Avatar;

    [SerializeField] Transform AvatarListPrefab;




     private void Awake() {
         
        
    }


    // Start is called before the first frame update
    void Start()
    {
        spr_player1Avatar.GetComponent<Image>().sprite = AvatarListPrefab.GetChild(0).GetComponent<SpriteRenderer>().sprite;
        spr_player2Avatar.GetComponent<Image>().sprite = AvatarListPrefab.GetChild(1).GetComponent<SpriteRenderer>().sprite;
       
        input_Colums.onValueChanged.AddListener(delegate {
            SetBoardHeight(input_Colums);});
        input_Rows.onValueChanged.AddListener(delegate {
            SetBoardWidth(input_Rows);});

        player1_Avatar.onValueChanged.AddListener(delegate {
            SelectPlayerAvatar(player1_Avatar,0);});
        player2_Avatar.onValueChanged.AddListener(delegate {
            SelectPlayerAvatar(player2_Avatar,1);});
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetBoardHeight(Dropdown input)
    {
        UserSettings.board_height = input.value;
    }
   void  SetBoardWidth(Dropdown input)
    {
        UserSettings.board_width =int.Parse(input.options[input.value].text); 
        // print(UserSettings._board_width);   
    }
    void  SelectPlayerAvatar(Dropdown input,int playerIndex)
    {
        if(playerIndex ==0){
        UserSettings.P1_AvatarIndex = input.value;
        spr_player1Avatar.GetComponent<Image>().sprite = AvatarListPrefab.GetChild(input.value).GetComponent<SpriteRenderer>().sprite;
        }
        else if(playerIndex ==1)
        {
            UserSettings.P2_AvatarIndex = input.value;
             spr_player2Avatar.GetComponent<Image>().sprite = AvatarListPrefab.GetChild(input.value).GetComponent<SpriteRenderer>().sprite;
        }
        // print(UserSettings._board_width);   
    }
    public void  NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    void StartGame()
    {

    }



//remove listeners
    private void OnDisable() {
        
    }
}
