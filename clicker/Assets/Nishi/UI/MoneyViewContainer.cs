using UnityEngine;
using UnityEngine.UI;

public class MoneyViewContainer : MonoBehaviour
{
    [SerializeField] private Text _text;

    private void Update()
    {
        _text.text = PlayerManager.Instance.Money.ToString("F2");
    }
}
