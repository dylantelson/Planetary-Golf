using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelSelectMenu : MonoBehaviour {

    public int levelToLoad;

    public void changeLevel(string level)
    {
        if (level == "Exit")
        {
            Application.Quit();
        }
        else
        SceneManager.LoadScene("Level" + level);
    }

}
