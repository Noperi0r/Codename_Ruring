using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    enum EPathMode
    { 
        None,
        Linear_Right,
        Linear_Left,
        Linear_Down,
        Curve_Down,
        Curve_Up
    }

    [SerializeField] EPathMode _spawnerPathMode;
    //GameManager _gameManager;
    [SerializeField] ObjectPoolManager _poolManager;

    Vector3 _targetPos;


    void Start()
    {
        if (!_poolManager)
            _poolManager = FindObjectOfType<ObjectPoolManager>();

        _targetPos = transform.GetChild(0).transform.position; // WC

        // TEST
        //ReleaseEnemy(5.0f, 2.0f, 8);
    }

    public void ReleaseEnemy(float moveSpeed, float decisionTime, int spawnPoint)
    {
        if (_spawnerPathMode == EPathMode.None) return;

        GameObject enemyObj = _poolManager.GetEnemyObjReady(moveSpeed, decisionTime);
        Enemy enemy = enemyObj.GetComponent<Enemy>();

        enemyObj.transform.position = transform.position;

        enemy.tweens = DOTween.Sequence();

        switch (_spawnerPathMode)
        {
            case EPathMode.Linear_Right:
                enemy.EnemyRb.velocity = new Vector2(enemy.moveSpeed, 0);
                break;

            case EPathMode.Linear_Left:
                enemy.EnemyRb.velocity = new Vector2(-enemy.moveSpeed, 0);
                break;

            case EPathMode.Linear_Down:
                enemy.EnemyRb.velocity = new Vector2(0, -enemy.moveSpeed);
                break;

            case EPathMode.Curve_Down:
                enemy.tweens.Append(enemy.transform.DOMoveX(_targetPos.x, enemy.moveSpeed).SetEase(Ease.InQuad));
                enemy.tweens.Join(enemy.transform.DOMoveY(_targetPos.y, enemy.moveSpeed).SetEase(Ease.OutQuad));
                break;

            case EPathMode.Curve_Up:
                enemy.tweens.Append(enemy.transform.DOMoveX(_targetPos.x, enemy.moveSpeed).SetEase(Ease.OutQuad));
                enemy.tweens.Join(enemy.transform.DOMoveY(_targetPos.y, enemy.moveSpeed).SetEase(Ease.InQuad));
                break;
        }
        SpriteRenderer fanSprite = enemy.fan.GetComponent<SpriteRenderer>();
        fanSprite.flipX = (spawnPoint >= 0 && spawnPoint <= 6) ? true : false;
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.GetChild(0).position);
    }

    // Shrink 속도 모드 따라서 in gmanager

    // 어느 스포너인지 
    // 움직이는 속도

}
