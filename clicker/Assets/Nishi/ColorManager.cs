using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class ColorManager : SingletonMonoBehaviour<ColorManager>
{
    [SerializeField] public List<Color> LevelsColor = new List<Color>();
}
