using UnityEngine;

public class PointFactory : AddFactoryBase
{
    public PointFactory()
    {
        Name = "Point Factory";
        Level = 0;
        _addCost = 1000;
    }

    public override double GetBuyCost(int buyLevel)
    {
        return _addCost * Mathf.Pow(1.2f, buyLevel);
    }

    public override double GetBuyCostGroup(int buyNum)
    {
        double sum = 0;
        for (int i = 0; i < buyNum; i++) sum += GetBuyCost(Level + i);

        return sum;
    }
}
