using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private Text cLevelText, nLevelText;
    private Image fill;

    private int level;
    private float startDistance, distance;

    private GameObject player, finish,hand;
    private TextMesh levelNo;
    void Awake()
    {
        cLevelText = GameObject.Find("CurrentLevel").GetComponent<Text>();
        nLevelText = GameObject.Find("NextLevel").GetComponent<Text>();
        fill = GameObject.Find("Fill").GetComponent<Image>();

        player = GameObject.Find("Player");
        finish = GameObject.Find("Finish");
        hand = GameObject.Find("Hand");

        levelNo = GameObject.Find("LevelNo").GetComponent<TextMesh>();
    }
    private void Start()
    {
        level = PlayerPrefs.GetInt("Level");
        levelNo.text = "LEVEL " + level;
        nLevelText.text = level + 1 + "";
        cLevelText.text = level.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
