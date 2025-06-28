using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    private AddFactoryBase _factory = new AddFactoryBase();

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

    }

    public AddFactoryBase GetFactory() { return _factory; }
}
