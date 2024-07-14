using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngineInternal;

public class Enemy : MonoBehaviour
{
    [SerializeField] float _decisionTime = 5f;
    public float decisionTime
    {
        get { return _decisionTime; }
        set { _decisionTime = value; }
    }
    
    [SerializeField] float _decisionThreshold = 0.3f;
    [SerializeField] float _rotateSpeed = 120.0f;

    GameObject _decisionCircle;
    GameObject _fan;

    public GameObject fan
    {
        get { return _fan; }
        set { fan = value; }
    }
    
    float _decCircleFirstScale;
    float _targetScale;

    public UnityEvent<EHitState> OnDecided;

    float _moveSpeed;
    Rigidbody2D _enemyRb;

    bool _isHit;

    public float moveSpeed
    {
        get { return _moveSpeed; }
        set { _moveSpeed = value; }
    }

    public Rigidbody2D EnemyRb => _enemyRb;

    DG.Tweening.Sequence _tweens;
    public DG.Tweening.Sequence tweens
    {
        get { return _tweens; }
        set { _tweens = value; }
    }

    void Awake()
    {
        _decisionCircle = transform.GetChild(0).gameObject;
        _decCircleFirstScale = _decisionCircle.transform.lossyScale.x;
        _targetScale = transform.lossyScale.x;
        _enemyRb = GetComponent<Rigidbody2D>();

        _fan = transform.transform.GetChild(1).gameObject;

        _tweens = DOTween.Sequence();
    }
    void OnEnable()
    {
        StartCoroutine("ShrinkCircle");
    }
    void OnDisable()
    {
        StopCoroutine("ShrinkCircle");
        ResetStatus_Disable();
    }

    void Update()
    {
        if(!_isHit)
            RotateCircle();

        DebugDecision();
        CheckCircle();
    }

    IEnumerator ShrinkCircle()
    {
        yield return null;
        float newScale = _decCircleFirstScale;

        while (true)
        {
            newScale -= (_decCircleFirstScale - Mathf.Lerp(_decCircleFirstScale, _targetScale, Time.deltaTime / _decisionTime));

            _decisionCircle.transform.parent = null;
            _decisionCircle.transform.localScale = new Vector3(newScale, newScale, newScale);
            _decisionCircle.transform.parent = transform;

            yield return null;
        }
    }

    void CheckCircle()
    {
        float currentScale = _decisionCircle.transform.lossyScale.x;
        float targetScale = transform.lossyScale.x;
        if (currentScale <= targetScale) 
        {
            OnDecided?.Invoke(EHitState.Fail);
            gameObject.SetActive(false);
        }
    }

    void ResetStatus_Disable()
    {
        _decisionCircle.transform.localScale = new Vector3(_decCircleFirstScale, _decCircleFirstScale, _decCircleFirstScale);
        _enemyRb.velocity = Vector2.zero;

        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        SpriteRenderer renderer2 = _decisionCircle.GetComponent<SpriteRenderer>();
        SpriteRenderer renderer3 = _fan.GetComponent<SpriteRenderer>();

        renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, 1);
        renderer2.color = new Color(renderer2.color.r, renderer2.color.g, renderer2.color.b, 1);
        renderer3.color = new Color(renderer3.color.r, renderer3.color.g, renderer3.color.b, 1);

        _tweens.Kill();

        _isHit = false; 
    }

    public void Hit()
    {
        float decision = Mathf.InverseLerp(_targetScale, _decCircleFirstScale, _decisionCircle.transform.lossyScale.x);
        if (decision <= _decisionThreshold)
        {
            OnDecided?.Invoke(EHitState.Success);
            fan.GetComponent<Animator>().SetTrigger("hitSuccess");

            ObjectPoolManager objPool = FindObjectOfType<ObjectPoolManager>();
            GameObject hitEffect = objPool.GetHitEffect();
            hitEffect.transform.position = transform.position;
        }
        else
        {
            OnDecided?.Invoke(EHitState.Fail);
            fan?.GetComponent<Animator>().SetTrigger("hitFail");
        }

        StartCoroutine("OnHit");
    }

    void DebugDecision()
    {
        SpriteRenderer renderer = _decisionCircle.GetComponent<SpriteRenderer>();

        float decision = Mathf.InverseLerp(_targetScale, _decCircleFirstScale, _decisionCircle.transform.lossyScale.x);
        if (decision <= _decisionThreshold)
        {
            renderer.color = Color.white;
        }
    }

    void RotateCircle()
    {
        transform.Rotate(0, 0, _rotateSpeed * Time.deltaTime);
        _fan.transform.Rotate(0,0, -_rotateSpeed * Time.deltaTime);
        _decisionCircle.transform.Rotate(0, 0, -_rotateSpeed * Time.deltaTime * 3);
    }

    IEnumerator OnHit()
    {
        yield return null;
        float elapsedTime = 0;

        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        SpriteRenderer renderer2 = _decisionCircle.GetComponent<SpriteRenderer>();
        SpriteRenderer renderer3 = _fan.GetComponent<SpriteRenderer>();  

        Color initialColor = renderer.color;

        float curCircleScale = _decisionCircle.transform.localScale.x;

        float duration = .3f;

        _isHit = true;

        StopCoroutine("ShrinkCircle");

        while (elapsedTime <= duration)
        {
            elapsedTime += Time.deltaTime;

            float alpha = Mathf.Lerp(initialColor.a, 0, elapsedTime / duration);

            renderer.color = new Color(initialColor.r, initialColor.g, initialColor.b, alpha);
            renderer2.color = new Color(initialColor.r, initialColor.g, initialColor.b, alpha);
            renderer3.color = new Color(initialColor.r, initialColor.g, initialColor.b, alpha);
/*
            float t = elapsedTime / duration;
            float easedT = EaseOutExp(t);
            float curScale = Mathf.Lerp(curCircleScale, _decCircleFirstScale, easedT);

            _decisionCircle.transform.localScale = new Vector3(curScale, curScale, curScale);*/
            _decisionCircle.transform.localScale = new Vector3(_decisionCircle.transform.localScale.x + 0.02f, 
                                                           _decisionCircle.transform.localScale.y + 0.02f, _decisionCircle.transform.localScale.z + 0.02f);


            yield return null;
        }

    }

    float EaseOutExp(float x)
    {
        return x == 1 ? 1 : 1 - Mathf.Pow(2, -10 * x);
    }
}

