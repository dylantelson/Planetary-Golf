using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelComplete : MonoBehaviour {

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LevelSelect()
    {
        SceneManager.LoadScene("LevelSelectLevel");
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
