using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager main;
    public int levelNumber;
    public void Start() {
        main = this;
    }
}
