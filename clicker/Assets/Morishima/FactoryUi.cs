using naichilab.Scripts.Extensions;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.UIElements;

public class FactoryUi : MonoBehaviour
{
    AddFactoryBase factoryBase;
    public enum ButtonType
    {
        Point,
        Amount,
        Speed
    }
    public ButtonType buttonType;
    public Text buttonName;
    public Text buttonCost;
    public Text buttonLevel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        factoryBase = GameManager.Instance.GetFactory();
        if (buttonName != null)
        {
            switch (buttonType)
            {
                case ButtonType.Point:
                    buttonName.text = "ポイント";
                    break;
                case ButtonType.Amount:
                    buttonName.text = "量";
                    break;
                case ButtonType.Speed:
                    buttonName.text = "速度";
                    break;
            }
        }
        if (buttonCost != null)
        {
            switch (buttonType)
            {
                case ButtonType.Point:
                    buttonCost.text = factoryBase.GetPointCost().ToReadableString();
                    break;
                case ButtonType.Amount:
                    buttonCost.text = factoryBase.GetAmountCost().ToReadableString();
                    break;
                case ButtonType.Speed:
                    buttonCost.text = factoryBase.GetSpeedCost().ToReadableString();
                    break;
            }
        }
        if (buttonLevel != null)
        {
            switch (buttonType)
            {
                case ButtonType.Point:
                    buttonLevel.text = factoryBase.PointNum.ToString();
                    break;
                case ButtonType.Amount:
                    buttonLevel.text = factoryBase.AmountNum.ToString();
                    break;
                case ButtonType.Speed:
                    buttonLevel.text = factoryBase.SecSpeedNum.ToString();
                    break;
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (buttonCost != null)
        {
            switch (buttonType)
            {
                case ButtonType.Point:
                    buttonCost.text = factoryBase.GetPointCost().ToReadableString();
                    break;
                case ButtonType.Amount:
                    buttonCost.text = factoryBase.GetAmountCost().ToReadableString();
                    break;
                case ButtonType.Speed:
                    buttonCost.text = factoryBase.GetSpeedCost().ToReadableString();
                    break;
            }
        }
        if (buttonLevel != null)
        {
            switch (buttonType)
            {
                case ButtonType.Point:
                    buttonLevel.text = factoryBase.PointNum.ToString();
                    break;
                case ButtonType.Amount:
                    buttonLevel.text = factoryBase.AmountNum.ToString();
                    break;
                case ButtonType.Speed:
                    buttonLevel.text = factoryBase.SecSpeedNum.ToString();
                    break;
            }
        }
    }

    public void OnClickButton()
    {
        switch (buttonType)
        {
            case ButtonType.Point:
                if (factoryBase.PointBuy())
                {
                    buttonLevel.text = factoryBase.PointNum.ToString();
                }
                break;
            case ButtonType.Amount:
                if (factoryBase.AmountBuy())
                {
                    buttonLevel.text = factoryBase.AmountNum.ToString();
                }
                break;
            case ButtonType.Speed:
                if (factoryBase.SpeedBuy())
                {
                    buttonLevel.text = factoryBase.SecSpeedNum.ToString();
                }
                break;
        }
    }
}
