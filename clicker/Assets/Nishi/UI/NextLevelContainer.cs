using naichilab.Scripts.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NextLevelContainer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;

    // Update is called once per frame
    void Update()
    {
        //int nextLevel =  PlayerManager.Instance.Money.GetLevel() + 1;
        //_text.text = DoubleExtensions.GetLevelStr(nextLevel);

        //if (nextLevel - 1 >= ColorManager.Instance.LevelsColor.Count) return;
        //_text.color = ColorManager.Instance.LevelsColor[nextLevel - 1];
    }
}
