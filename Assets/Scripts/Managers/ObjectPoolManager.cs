using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    [SerializeField] GameObject _enemyManPrefab; 
    [SerializeField] GameObject _enemyGirlPrefab; // 인스펙터창에서 지정 필요
    [SerializeField] GameObject _spawnerParent;
    [SerializeField] GameObject _hitEffectPrefab;

    const int POOLSIZE = 256;
    const int EFFECTSIZE = 28;

    GameObject[] _enemyObjs;
    public Enemy[] _enemies;

    GameObject[] _spawners;
    GameObject[] _hitEffect;

    void Awake()
    {
        _enemyObjs = new GameObject[POOLSIZE];
        _enemies = new Enemy[POOLSIZE];
        _spawners = new GameObject[_spawnerParent.transform.childCount];

        for (int i = 0; i < _spawners.Length; ++i)
            _spawners[i] = _spawnerParent.transform.GetChild(i).gameObject;

        _hitEffect = new GameObject[EFFECTSIZE];

        Generate();
    }

    void Generate()
    {
        for(int i=0; i<_enemyObjs.Length; i++) 
        {
            int flag = Random.Range(0, 2);
            Debug.Log(flag);
            if(flag == 0)
                _enemyObjs[i] = Instantiate(_enemyManPrefab);
            else
                _enemyObjs[i] = Instantiate(_enemyGirlPrefab);

            _enemies[i] = _enemyObjs[i].GetComponent<Enemy>();

            _enemyObjs[i].SetActive(false);
        }

        for(int i=0; i<_hitEffect.Length; ++i)
        {
            _hitEffect[i] = Instantiate(_hitEffectPrefab);
            _hitEffect[i].SetActive(false);
        }
    }

    public void PoolsOff()
    {
        for(int i=0; i<_enemyObjs.Length; i++)
        {
            _enemyObjs[i].SetActive(false);
        }
        print("POOLS OFF");
    }

    public GameObject GetEnemyObjReady(float moveSpeed, float decisionTime)
    {
        for(int i=0; i<_enemyObjs.Length; i++)
        {
            if (!_enemyObjs[i].activeSelf)
            {
                _enemyObjs[i].SetActive(true);
                _enemies[i].moveSpeed = moveSpeed;
                _enemies[i].decisionTime = decisionTime;
                return _enemyObjs[i];
            }
        }
        Debug.Log("ALL ENEMIES ARE ACTIVE NOW");
        return null;
    }

    public GameObject GetSpawner(int i)
    {
        return _spawners[i];
    }

    public GameObject GetHitEffect()
    {
        for (int i = 0; i < _hitEffect.Length; i++)
        {
            if (!_hitEffect[i].activeSelf)
            {
                _hitEffect[i].SetActive(true);
                return _hitEffect[i];
            }
        }
        return null;
    }

}
