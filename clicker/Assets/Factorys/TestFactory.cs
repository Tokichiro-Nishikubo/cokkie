using UnityEngine;

public class TestFactory : AddFactoryBase
{
    public TestFactory()
    {
        _baseCost = 10;
        _baseAddMoneyPerSec = 1;
        Name = "�e�X�g���u";
    }

    public override double GetBuyCost(int buyLevel)
    {
        return buyLevel * _baseCost;
    }

    public override double GetAddBaseMoneyPerSec()
    {
        return Level * _baseAddMoneyPerSec;
    }
}
