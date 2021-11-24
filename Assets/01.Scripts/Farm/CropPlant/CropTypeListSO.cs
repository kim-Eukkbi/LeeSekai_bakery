using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FarmSO/CropTypeListSO")]
public class CropTypeListSO : ScriptableObject
{
    //작물 리스트
    public List<CropTypeSO> cpList;
}
