using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MillManager : MonoBehaviour
{
    public MillManager Instance = null;

    //�̳��� ���ۼ� UI�� ������ �������ش�

    //�ؾ��� �� ������ ����Ʈ�� UI����
    //�����ǿ� ���� ���UI ���� �� ����

    //�����۵��� ����Ʈ
    public List<MillItem> items = new List<MillItem>();
    //���ʿ� ������ �̹���
    public MillItem leftInvisibleItem;
    //�����ʿ� ������ �̹���
    public MillItem rightInvisibleItem;

    //������ ����Ʈ
    [SerializeField]
    private List<RecipeSO> recipes;

    //���콺�� Ŭ���ߴ� �� ����
    private float startPos;
    private float endPos;

    //�ڵ尡 ������� �����̴� �κ��� �и�������
    private MillUIMove uIMove;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }

        //����Ʈ�� �ε����ش�
        recipes = Resources.Load<RecipeListSO>(typeof(RecipeListSO).Name).recipes;

        uIMove = GetComponent<MillUIMove>();
    }

    private void Start()
    {
        //������ �����ش�
        uIMove.SetMillItems(items, leftInvisibleItem, rightInvisibleItem);
    }

    void Update()
    {
        //���߿� InputManager�� ������
        if (Input.GetMouseButtonDown(0))
        {
            startPos = Input.mousePosition.x;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            endPos = Input.mousePosition.x;

            if (startPos < endPos)
            {
                uIMove.MoveRight();
            }
            else if (startPos > endPos)
            {
                uIMove.MoveLeft();
            }
        }
    }
}
