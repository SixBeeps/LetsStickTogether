using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager main;
    public Text timerText;
    public Image loseImg;
    public Image winImg;
    public string nextSceneName;
    public System.DateTime? startTime = null;
    void Start()
    {
        main = this;
    }

    void Update()
    {
        if (startTime.HasValue && timerText) {
            var timeElapsed = System.DateTime.Now - startTime;
            timerText.text = timeElapsed.GetValueOrDefault().ToString(@"mm\:ss");
        }
    }

    public void NextLevel() {
        SceneManager.LoadScene(nextSceneName);
    }

    public void LoadSpecificLevel(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }

    public void Restart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LeaveGame() {
        Application.Quit();
    }
}
