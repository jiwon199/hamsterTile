using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneChange : MonoBehaviour
{
   
    public void goToHome()
    {
        SceneManager.LoadScene("homeScene");
    }
    public void goToStage()
    {
        SceneManager.LoadScene("stageScene");
    }
    public void goToPuzzle()
    {            
        SceneManager.LoadScene("puzzleScene");              
    }
    
}
