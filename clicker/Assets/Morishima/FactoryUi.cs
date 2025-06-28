using naichilab.Scripts.Extensions;
using TMPro;
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
    public TextMeshProUGUI buttonName;
    public TextMeshProUGUI buttonCost;
    public TextMeshProUGUI buttonLevel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        factoryBase = GameManager.Instance.GetFactory();
        if (buttonName != null)
        {
            switch (buttonType)
            {
                case ButtonType.Point:
                    buttonName.text = "�|�C���g";
                    break;
                case ButtonType.Amount:
                    buttonName.text = "��";
                    break;
                case ButtonType.Speed:
                    buttonName.text = "���x";
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
                    int level = factoryBase.SecSpeedNum;
                    if (level >= 9)
                    {
                        // ���x�̍ő僌�x����9
                        buttonCost.text = "MAX";
                    }
                    else
                    {
                        buttonCost.text = factoryBase.GetSpeedCost().ToReadableString();
                    }
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
        var mousePos = Input.mousePosition;
        Debug.Log($"OnClickButton: {mousePos}");
        switch (buttonType)
        {
            case ButtonType.Point:
                if (factoryBase.IsPointBuy())
                {
                    factoryBase.PointBuy();
                }   
                break;
            case ButtonType.Amount:
                if(factoryBase.IsAmountBuy())
                {
                    factoryBase.AmountBuy();
                }
                break;
            case ButtonType.Speed:
                int level = factoryBase.SecSpeedNum;
                if (level >= 9) break;
                if (factoryBase.IsSpeedBuy())
                {
                    factoryBase.SpeedBuy();
                }
                break;
        }
    }
}
