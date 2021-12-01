using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Season
{
    ALL,
    SPRING,
    SUMMER,
    FALL,
    WINTER
}

[CreateAssetMenu(menuName = "FarmSO/CropTypeSO")]
public class CropTypeSO : ScriptableObject
{
    //�̳��� �۹��̴�
    //�̸��� �־�ߵǳ� ����
    public string cropName;
    //�̰� �����ϴ� ��������Ʈ��
    public List<Sprite> growSprite;
    //������
    public Transform prefab;
    //���� ������ ����
    public Season growSeason;
    //�����ϴµ� �ɸ��� ��¥
    public float growDay;
    //�̳��� ��¥�� �ð����� �ٲ� ��

    //�ð��� ���� ��������Ʈ ��ȭ�� �ٸ������� ����� ��
}
