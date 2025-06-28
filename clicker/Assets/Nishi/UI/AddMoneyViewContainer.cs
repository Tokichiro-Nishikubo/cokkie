using naichilab.Scripts.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AddMoneyViewContainer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;

    private void Update()
    {
        double scaler = 1;
        if (InfinitMouseManager.Instance._isInfinityTime) scaler = GameManager.Instance.GetFactory().TotalAmplificationNum + 2;

        double add = GameManager.Instance.GetFactory().GetAddBaseMoneyPerSec() * scaler;
        _text.text = "+" + add.ToReadableString() + "/s";
    }
}
