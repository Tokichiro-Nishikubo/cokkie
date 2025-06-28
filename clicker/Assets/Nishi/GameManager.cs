using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    private List<AddFactoryBase> _factorys = new List<AddFactoryBase>();

    private void Start()
    {
        PlayerManager.Instance.AddMoney(100);
        _factorys.Add(new TestFactory());
    }

    private void Update()
    {
        float deltaSec = Time.deltaTime;

        for(int i = 0; i < _factorys.Count; i++)
        {
            AddFactoryBase factory = _factorys[i];

            // ‚¨‹à’Ç‰Á
            double perSecAdd = factory.GetAddBaseMoneyPerSec();
            double add = perSecAdd * (double)deltaSec;
            PlayerManager.Instance.AddMoney(add);
        }

        if (Input.GetKeyDown(KeyCode.B)) _factorys[0].Buy(1);
    }

    public List<AddFactoryBase> GetFactorys() { return _factorys; }
}
