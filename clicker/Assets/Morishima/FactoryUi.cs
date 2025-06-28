using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.UIElements;

public class FactoryUi : MonoBehaviour
{
    public Text buttonName;
    public Text buttonCost;
    public Text buttonLevel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (buttonName == null || buttonCost == null || buttonLevel == null) return;

        AddFactoryBase factory = GetComponent<AddFactoryBase>();
        if (factory == null) return;

        buttonName.text = factory.Name;
        buttonCost.text = factory.GetBuyCost(1).ToString("F0");
        buttonLevel.text = "Lv." + factory.Level.ToString();

        // É{É^ÉìÇÃóLå¯/ñ≥å¯Çê›íË
        //GetComponent<Button>().interactable = factory.IsBuy(1);

    }
}
