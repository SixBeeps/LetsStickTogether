using UnityEngine;
using UnityEngine.UI;

public class LevelPickerButton : MonoBehaviour
{
    public int levelNum;
    void Start()
    {
        GetComponent<Button>().interactable = (PlayerPrefs.GetInt("bestLevel", 0) >= levelNum);
    }
}
