using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SimDataScriptableObject", order = 1)]
public class SimDataSO : ScriptableObject
{
    public float screenSizeX;
    public float screenSizeY;
}
