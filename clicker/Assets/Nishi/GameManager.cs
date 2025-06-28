using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    private AddFactoryBase _factory = new AddFactoryBase();

    [SerializeField] private GameObject _text;
    [SerializeField] private GameObject _nextLevelText;
    [SerializeField] private GameObject _store;

    [SerializeField] private GameObject _reachInfText;

    private double addLev = 1;

    private string _socreKey = "Score";
    private string _facPointLevKey = "FPoint";
    private string _facAmountKey = "FAmount";
    private string _facSpeedKey = "FSpeed";

    private void Start()
    {
        if(!Load()) PlayerManager.Instance.AddMoney(100);
    }

    private void Update()
    {
        float deltaSec = Time.deltaTime;

        double scaler = 1;
        if (InfinitMouseManager.Instance._isInfinityTime) scaler = 5;

        // お金追加
        double perSecAdd = _factory.GetAddBaseMoneyPerSec() * scaler;
        double add = perSecAdd * (double)deltaSec;
        PlayerManager.Instance.AddMoney(add);

        Debug.Log("add: " + perSecAdd);

        Save();
    }

    public AddFactoryBase GetFactory() { return _factory; }

    public void ReachInf()
    {
        _text.SetActive(false);
        _nextLevelText.SetActive(false);
        _store.SetActive(false);
        _reachInfText.SetActive(true);
    }

    private void Save()
    {
        SephirothTools.SephirothPlayerPrefsExtension.Save<double>(_socreKey, PlayerManager.Instance.Money);
        PlayerPrefs.SetInt(_facPointLevKey, _factory.PointNum);
        PlayerPrefs.SetInt(_facAmountKey, _factory.AmountNum);
        PlayerPrefs.SetInt(_facSpeedKey, _factory.SecSpeedNum);
        PlayerPrefs.Save();
    }

    private bool Load()
    {
        if (!PlayerPrefs.HasKey(_facPointLevKey)) return false;

        // スコアロード
        double set = SephirothTools.SephirothPlayerPrefsExtension.Load<double>(_socreKey);
        PlayerManager.Instance.ForceSetMoney(set);

        // 点のロード
        _factory.ForceSetPointNum(PlayerPrefs.GetInt(_facPointLevKey));
        _factory.ForceSetAmountNum(PlayerPrefs.GetInt(_facAmountKey));
        _factory.ForceSetSpeedNum(PlayerPrefs.GetInt(_facSpeedKey));

        return true;
    }

}
