using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelOptions : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    
    public void restartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    public void MainMenu(){
        SceneManager.LoadScene(0);
    }

    public void QuitApp()
    {
        Application.Quit();
    }


}
