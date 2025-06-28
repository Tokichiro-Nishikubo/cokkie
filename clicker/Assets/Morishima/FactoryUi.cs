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
        Amplification,
        Point,
        Amount,
        Speed
    }
    public ButtonType buttonType;
    public TextMeshProUGUI buttonName;
    public TextMeshProUGUI buttonCost;
    public TextMeshProUGUI buttonLevel;
    public ParticleSystem buttonParticle;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        factoryBase = GameManager.Instance.GetFactory();
        if (buttonName != null)
        {
            switch (buttonType)
            {
                case ButtonType.Amplification:
                    buttonName.text = "倍率";
                    break;
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
                case ButtonType.Amplification:
                    buttonCost.text = factoryBase.GetTotalAmplificationCost().ToReadableString();
                    break;
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
                case ButtonType.Amplification:
                    buttonLevel.text = factoryBase.TotalAmplificationNum.ToString();
                    break;
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
                case ButtonType.Amplification:
                    buttonCost.text = factoryBase.GetTotalAmplificationCost().ToReadableString();
                    break;
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
                        // 速度の最大レベルは9
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
                case ButtonType.Amplification:
                    buttonLevel.text = factoryBase.TotalAmplificationNum.ToString();
                    break;
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
        if (buttonParticle != null)
        {
            buttonParticle.Play();
        }

        Debug.Log($"OnClickButton: {mousePos}");
        switch (buttonType)
        {
            case ButtonType.Amplification:
                if (factoryBase.IsTotalAmplificationBuy())
                {
                    factoryBase.TotalAmplificationBuy();
                }
                break;
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
