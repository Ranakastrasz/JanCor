using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    

    public static GameManager Active;

    public bool Playing = false;

    public Ship player;
    public Ship enemy;
    public Console console;

    public GameObject GameOverPanel;
    public GameObject LevelWinPanel;

    public GameObject BulletContainer;

    public Text GameOverText;
    public Text LevelWinText;

    public Text LevelWinTimerText;


    public Text EquationText;

    public int currentLevel = 0;

    public float gameDifficulty = 1f;

    public float shipDistance;

    // Use this for initialization
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);

        Active = this;

        currentLevel = 0;

        Invoke("NextLevel", 1.0f);
        PrintEquation("");
        shipDistance = Vector3.Magnitude(player.transform.position - enemy.transform.position) - (player.GetComponent<CircleCollider2D>().radius * player.transform.lossyScale.magnitude);
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
    
    void DrawText()
    {

    }

    public void NewTurn()
    {
        Invoke("Attack",0.5f);
    }

    void Attack()
    {
        enemy.Attack();
    }

    public void Die(Ship ship)
    {
        if (ship == player)
        {
            GameOver();
        }
        if (ship == enemy)
        {
            player.Repair(2);
            LevelWin();
        }

        // If player died, gameover, else next round. Or something)
    }

    public void GameOver()
    {
        Playing = false;
        GameOverPanel.SetActive(true);
        GameOverText.text = "Game Over!";
    }

    public void LevelWin()
    {
        Playing = false;
        LevelWinPanel.SetActive(true);
        LevelWinText.text = "Next Level in";
        InvokeRepeating("WinTimer", 1, 1);
        LevelWinTimerText.text = "5";
    }

    private void WinTimer()
    {
        LevelWinTimerText.text = (int.Parse(LevelWinTimerText.text) - 1).ToString();
        SoundManager.Active.PlaySound(SoundManager.Active.beep,1f);
        if (LevelWinTimerText.text == "0")
        {
            NextLevel();
            CancelInvoke("WinTimer");
        }
    }

    internal float GetSpeedModifier(int level)
    {
        // 
        return 0.8f + ((0.1f * gameDifficulty) * level);
    }

    private void NextLevel()
    {
        // 4 rounds. 
        // Enemies have 4-5-6-7 health
        // Addition, Subtraction, Multiplication, Division.
        // Base speed of 1,1.5,2,2.5
        // 

        if (!Playing)
        {
            Playing = true;
            currentLevel++;
            //GameOverPanel.SetActive(false);
            LevelWinPanel.SetActive(false);
            
            enemy.Repair(currentLevel + 2, true);
            NewTurn();
        }
        else
        {
            Debug.Log("cannot start new level when level already started");
        }
    }

    public void PrintEquation(string iEquation)
    {
        EquationText.text = iEquation;

    }


}
    