using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmTile : MonoBehaviour
{
    //��������Ʈ�� (0 = ��������, 1 = ������)
    [SerializeField]
    private List<Sprite> farmSprites;

    private SpriteRenderer sr;


    //���� �����ִ� ��������
    public bool isWet = false;
    //���� �۹��� �ɰ��ִ� ��������
    public bool isPlanted = false;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();

        farmSprites = new List<Sprite>();
        farmSprites = Resources.Load<FarmTileSpriteListSO>(typeof(FarmTileSpriteListSO).Name).farmTileSprites;
    }

    //���� �����ִ� �������� Ȯ��
    public bool IsWet()
    {
        return isWet;
    }

    //���ִ� �Լ�
    public void Water(bool isWatered)
    {
        isWet = isWatered;

        //���� ����������
        if(isWet)
        {
            sr.sprite = farmSprites[1];
        }
        else
        {
            sr.sprite = farmSprites[0];
        }
    }

    //�۹��ɴ� �Լ�
    public void Plant(CropTypeSO crop)
    {
        //�̹� �۹��� �ɰ����ִٸ� ����
        if (isPlanted) return;

        //�۹��� �ɰ�ٰ� �˷���
        isPlanted = true;
        //���� ��ġ�� �۹��� ����
        Instantiate(crop.prefab, transform.position, Quaternion.identity);
    }
}
