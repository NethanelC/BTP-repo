using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesRoom : Room
{
    [SerializeField] private Enemy _enemyPrefab;
    private int _amountOfEnemies;
    private List<Enemy> _enemyList = new();
    protected override void SpawnInteractables()
    {
        _amountOfEnemies = Random.Range(3, 5);
        for (int i = 0; i < _amountOfEnemies; i++)
        {
            var enemy = Instantiate(_enemyPrefab, transform, false);
            enemy.Init(this);
            enemy.transform.localPosition = new Vector2(Random.Range(-10, 10), Random.Range(-4.5f, 4.5f));
            _enemyList.Add(enemy);
        }
    }
    public void EnemyDied()
    {
        if(--_amountOfEnemies == 0)
        {
            CompleteRoom();
        }
    }
    public void DestructRoom()
    {
        for (int i = 0; i < _enemyList.Count; i++)
        {
            _enemyList[i].Die();
        }
    }
}
