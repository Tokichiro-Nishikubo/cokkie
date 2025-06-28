using System;
using UnityEngine;

public class AddFactoryBase
{
    public string Name { get; protected set; }

    public int PointNum { get; protected set; } = 0;

    public int AmountNum { get; protected set; } = 0;

    public double SecSpeed { get; protected set; } = 1;
    public int SecSpeedNum { get; protected set; } = 0;

    public int TotalAmplificationNum { get; protected set; } = 0;

    // ÉxÅ[ÉXâ¡éZó 
    private double _baseAdd = 1;

    // î{ó¶
    private double _mulPoint= 100;
    private double _mulAmount = 10;
    private double _mulSecSpeed = 5;
    private double _mulTotalAmplification = 1.5;

    // äÓëbíl
    protected double _basePointCost = 8;
    protected double _baseAmountCost = 4;
    protected double _baseSpeedCost = 3;
    protected double _baseTotalAmplificationCost = 10;

    public virtual double GetAddBaseMoneyPerSec()
    {
        return PointNum * AmountNum * _baseAdd / SecSpeed;
    }

    public double GetPointCost()
    {
        return _basePointCost * Math.Pow(_mulPoint, PointNum);
    }

    public double GetAmountCost()
    {
        return _baseAmountCost * Math.Pow(_mulAmount, AmountNum);
    }

    public double GetSpeedCost()
    {
        return _baseSpeedCost * Math.Pow(_mulSecSpeed, SecSpeedNum);
    }
    public double GetTotalAmplificationCost()
    {
        return _baseTotalAmplificationCost * Math.Pow(_mulTotalAmplification, TotalAmplificationNum);
    }


    public bool PointBuy()
    {
        double cost = GetPointCost();
        if (PlayerManager.Instance.Money < cost) return false;

        PointNum++;
        return true;
    }

    public bool AmountBuy()
    {
        double cost = GetAmountCost();
        if (PlayerManager.Instance.Money < cost) return false;

        AmountNum++;
        return true;
    }

    public bool SpeedBuy()
    {
        double cost = GetSpeedCost();
        if (PlayerManager.Instance.Money < cost) return false;

        SecSpeedNum++;
        SecSpeed = 1 - SecSpeedNum;
        return true;
    }
    public bool TotalAmplificationBuy()
    {
        double cost = GetTotalAmplificationCost();
        if (PlayerManager.Instance.Money < cost) return false;

        TotalAmplificationNum++;
        return true;
    }

    public bool IsPointBuy()
    {
        return PlayerManager.Instance.Money < GetPointCost();
    }

    public bool IsAmountBuy()
    {
        return PlayerManager.Instance.Money < GetAmountCost();
    }

    public bool IsSpeedBuy()
    {
        return PlayerManager.Instance.Money < GetSpeedCost();
    }

    public bool IsTotalAmplificationBuy()
    {
        return PlayerManager.Instance.Money < GetTotalAmplificationCost();
    }
    public void ForceSetPointNum(int num)
    {
        PointNum = num;
    }

    public void ForceSetAmountNum(int num)
    {
        AmountNum = num;
    }

    public void ForceSetSpeedNum(int num)
    {
        SecSpeedNum = num;
        SecSpeed = 1 - SecSpeedNum;
    }
    public void ForceSetTotalAmplificationNum(int num)
    {
        TotalAmplificationNum = num;
    }
}
