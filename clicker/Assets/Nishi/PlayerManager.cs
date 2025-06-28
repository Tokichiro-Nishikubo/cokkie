using UnityEngine;
using Utils;

public class PlayerManager : SingletonMonoBehaviour<PlayerManager>
{
    public double Money { get; private set; } = 0;


    private void Start()
    {
        
    }

    public void UseMoney(double money)
    {
        Money -= money;
        if (Money <= 0) Money = 0; // �Ȃ��Ƃ͎v�����ꉞ
    }

    public void AddMoney(double add)
    {
        Money += add;
    }

    public void ForceSetMoney(double set)
    {
        Money = set;
    }
}
