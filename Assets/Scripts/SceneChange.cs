using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneChange : MonoBehaviour
{
    
   // int lastStage = 24;
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

    public void goToStart()
    {
        SceneManager.LoadScene("startScene");
    }
    public void goToNextPuzzle()
    {
        int lastStage;
        if (GameManager.instance.localWorldInfo == 0) lastStage = 24;
        else lastStage = 40;
        GameObject.Find("stagenum");
        if(stagenum.stageNum<lastStage) stagenum.stageNum++;

        
         SceneManager.LoadScene("puzzleScene");

    }
    
    public void goToStory() 
    {
        SceneManager.LoadScene("story");
    }
    public void goToStory2()
    {
        SceneManager.LoadScene("story2");
    }
    public void goToEnding()
    {
        SceneManager.LoadScene("endingScene");
    }

    public void EndGame()
    {
        Application.Quit();
       // UnityEditor.EditorApplication.isPlaying = false;
    }
    //테스트플레이용-모든레벨을 클리어처리
    public void test()
    {
        GameManager.instance.testAllPuzzle();
    }

}
