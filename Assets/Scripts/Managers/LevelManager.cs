using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private int lives = 10;

    public int TotalLives { get; set; }
    public int CurrentWave { get; set; }
    
    private void Start()
    {
        TotalLives = lives;
        CurrentWave = 1;
    }

    private void ReduceLives(Enemy enemy)
    {
        int damage = enemy.DamageToCarrot;
        TotalLives -= damage;
        
        Debug.Log($"敌人到达终点！扣减 {damage} 点生命值，剩余 {TotalLives}");
        
        if (TotalLives <= 0)
        {
            TotalLives = 0;
            GameOver();
        }
    }

    public void TakeDamage(int damage = 1)
    {
        TotalLives -= damage;
        TotalLives = Mathf.Max(0, TotalLives);
        
        Debug.Log($"受到伤害！扣减 {damage} 点生命值，剩余 {TotalLives}");
        
        if (TotalLives <= 0)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        UIManager.Instance.ShowGameOverPanel();
    }
    
    private void WaveCompleted()
    {
        CurrentWave++;
        AchievementManager.Instance.AddProgress("Waves10", 1);
        AchievementManager.Instance.AddProgress("Waves20", 1);
        AchievementManager.Instance.AddProgress("Waves50", 1);
        AchievementManager.Instance.AddProgress("Waves100", 1);
    }
    
    private void OnEnable()
    {
        Enemy.OnEndReached += ReduceLives;
        Spawner.OnWaveCompleted += WaveCompleted;
    }

    private void OnDisable()
    {
        Enemy.OnEndReached -= ReduceLives;
        Spawner.OnWaveCompleted -= WaveCompleted;
    }
}