using UnityEngine;

public class AddFactoryBase
{
    public string Name { get; protected set; }

    public int Level { get; protected set; }

    protected double _addCost = 0;

    public virtual double GetBuyCostGroup(int buyNum)
    {
        double sum = 0;
        for (int i = 0; i < buyNum; i++) sum += GetBuyCost(Level + buyNum);

        return sum;
    }

    public virtual double GetBuyCost(int buyLevel)
    {
        return 0;
    }

    public bool Buy(int buyNum = 1)
    {
        double cost = GetBuyCostGroup(buyNum);
        if (PlayerManager.Instance.Money < cost) return false;

        PlayerManager.Instance.UseMoney(cost);
        Level += buyNum;
        return true;
    }

    public bool IsBuy(int buyNum = 1)
    {
        double cost = GetBuyCostGroup(buyNum);

        return PlayerManager.Instance.Money >= cost;
    }
}
