using System;
using UnityEngine;

public class AddFactoryBase
{
    public string Name { get; protected set; }

    public int PointNum { get; protected set; } = 0;

    public int AmountNum { get; protected set; } = 0;

    public double SecSpeed { get; protected set; } = 0;
    public int SecSpeedNum { get; protected set; } = 0;

    // ÉxÅ[ÉXâ¡éZó 
    private double _baseAdd = 1;

    // î{ó¶
    private double _mulPoint= 100;
    private double _mulAmount = 10;
    private double _mulSecSpeed = 5;

    // äÓëbíl
    protected double _basePointCost = 8;
    protected double _baseAmountCost = 4;
    protected double _baseSpeedCost = 3;

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
}
