using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    //인벤토리 매니저는 인벤토리를 총괄하는 녀석이다
    //지금 퀵슬롯이 어딘지, 인벤토리 슬롯끼리의 아이템을 바꾸던지 등등을 해주는 곳이다

    //인벤토리들이 담겨있을 부모오브젝트를 가져오자
    [SerializeField]
    private GameObject quickSlockParent;
    [SerializeField]
    private GameObject inventorySlockParent;

    //현재 선택된 인벤토리
    [SerializeField]
    private InventorySlot selectedQuickSlot;
    //인벤토리가 선택되었을 때 위에 보여줄 오브젝트
    [SerializeField]
    private RectTransform selectedInventoryTrm;

    //선택된 인벤토리 오브젝트가 숫자별로 위치할 x값(어차피 y값은 같으니깐)
    [SerializeField]
    private float[] selectedInventoryXPoints;
    //위에서 나온 그 같은 y값 ㅋㅋ
    [SerializeField]
    private float selectedInventoryYPoint;

    //퀵슬롯과 인벤토리슬롯을 선언해주고
    [SerializeField]
    private List<InventorySlot> quickSlots;
    [SerializeField]
    private List<InventorySlot> inventorySlots;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }

        //메모리를 할당해주자
        quickSlots = new List<InventorySlot>();
        inventorySlots = new List<InventorySlot>();

        //부모의 자식오브젝트들의 컴포넌트를 리스트로 긁어온다
        quickSlots = quickSlockParent.GetComponentsInChildren<InventorySlot>().ToList();
        inventorySlots = inventorySlockParent.GetComponentsInChildren<InventorySlot>().ToList();

        for (int i = 0; i < quickSlots.Count; i++)
        {
            int a = i;

            quickSlots[a].GetComponentInChildren<Button>().onClick.AddListener(() =>
            {
                SelectInventory(a);
            });
        }

        //시작할땐 1번을 선택한상태로
        SelectInventory(0);
    }

    private void Update()
    {
        //숫자 키가 눌리면 그에 해당하는 인벤토리를 선택해준다
        //함수에 전달하는 값은 키번호가 아닌 리스트에서의 번호
        if (Input.GetKeyDown(KeyCode.Alpha1)) SelectInventory(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) SelectInventory(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) SelectInventory(2);
        if (Input.GetKeyDown(KeyCode.Alpha4)) SelectInventory(3);
        if (Input.GetKeyDown(KeyCode.Alpha5)) SelectInventory(4);
        if (Input.GetKeyDown(KeyCode.Alpha6)) SelectInventory(5);
        if (Input.GetKeyDown(KeyCode.Alpha7)) SelectInventory(6);
        if (Input.GetKeyDown(KeyCode.Alpha8)) SelectInventory(7);
        if (Input.GetKeyDown(KeyCode.Alpha9)) SelectInventory(8);
        if (Input.GetKeyDown(KeyCode.Alpha0)) SelectInventory(9);
    }

    private void SelectInventory(int idx)
    {
        //선택된슬롯 바꿔주고
        selectedQuickSlot = quickSlots[idx];
        //시각적으로 오브젝트도 옮겨준다
        selectedInventoryTrm.anchoredPosition = new Vector2(selectedInventoryXPoints[idx], selectedInventoryYPoint);
    }

    public Item NowSelectedItem()
    {
        return selectedQuickSlot.item;
    }
}
