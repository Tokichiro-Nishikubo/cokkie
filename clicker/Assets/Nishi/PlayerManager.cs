using UnityEngine;
using Utils;

public class PlayerManager : SingletonMonoBehaviour<PlayerManager>
{
    public double Money { get; private set; }


    public void UseMoney(double money)
    {
        Money -= money;
        if (Money <= 0) Money = 0; // ‚È‚¢‚Æ‚ÍŽv‚¤‚ªˆê‰ž
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
