using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    private AddFactoryBase _factory = new AddFactoryBase();

    private double addLev = 1;

    private void Start()
    {
        PlayerManager.Instance.AddMoney(100);
    }

    private void Update()
    {
        float deltaSec = Time.deltaTime;

        // ‚¨‹à’Ç‰Á
        double perSecAdd = _factory.GetAddBaseMoneyPerSec();
        double add = perSecAdd * (double)deltaSec;
        PlayerManager.Instance.AddMoney(add);

        PlayerManager.Instance.AddMoney(Math.Pow(10, addLev));

        if(Input.GetKeyDown(KeyCode.B))
        {
            addLev++;
        }
        else if(Input.GetKeyDown(KeyCode.S))
        {
            addLev--;
        }

    }

    public AddFactoryBase GetFactory() { return _factory; }
}
