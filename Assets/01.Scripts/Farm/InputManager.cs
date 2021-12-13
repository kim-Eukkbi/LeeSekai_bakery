using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    //여기서는 뭘 해주냐
    //일단 클릭한 밭이 어떤놈인지 가져와
    //물이 뿌려져있으면 작물을 심어
    //물이 안뿌려져있으면 물을 줘
    //12345를 작물로 하자
    //작물은 CropManager에서 가져오는걸로

    //이제는 그냥 이름만 FarmManager고 InputManager가 맞는듯 ㅇㅇ

    //메인캠 담아놓는 변수
    public static InputManager Instance = null;

    private Camera mainCam;

    //그래픽 레이케스터 한번 써보자
    public GraphicRaycaster graphicRaycaster;
    public EventSystem eventSystem;

    //그래픽 레이케스터 포인터
    private PointerEventData pointer;

    //플레이어 담아놓는 변수
    public PlayerMove player;

    [Header("제작UI들")]
    public CanvasGroup fryUI;
    public CanvasGroup ovenUI;
    public CanvasGroup domaUI;

    private InventorySlot firstSlot;
    private InventorySlot secondSlot;

    public bool isUIOpen = false;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }

        mainCam = Camera.main;

        pointer = new PointerEventData(eventSystem);
    }

    void Update()
    {
        if (isUIOpen) return;

        if(Input.GetMouseButtonDown(0))
        {
            //UI를 클릭한게 아니라면
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    //print(hit.transform.gameObject.name);

                    //현재 선택된 슬롯 뽑아오고
                    InventorySlot inventorySlot = InventoryManager.Instance.NowSelectedInventory();

                    //UseItem과 Harvest 분리 성공

                    //만약 선택된 슬롯의 현재 아이템이 손이라면
                    if (inventorySlot.CurrentItem() == inventorySlot.handItem)
                    {
                        //수확해버려
                        inventorySlot.CurrentItem().UseItem(hit, player);
                    }
                    else
                    {
                        //아니라면 그대로 아이템 사용
                        inventorySlot.UseItem(hit, player);
                    }
                }
            }
            else
            {
                //임시적으로 가장 가까운 인벤토리 슬롯을 받아온다
                InventorySlot tmpSlot = InventoryManager.Instance.FindNearestSlot(pointer, graphicRaycaster);

                //있다면 예가 첫번째 슬롯
                if (tmpSlot != null)
                {
                    firstSlot = tmpSlot;
                }
            }
        }

        if(Input.GetMouseButtonUp(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                //임시적으로 가장 가까운 인벤토리 슬롯을 받아온다
                InventorySlot tmpSlot = InventoryManager.Instance.FindNearestSlot(pointer, graphicRaycaster);

                //있다면 예가 두번째 슬롯
                if (tmpSlot != null)
                {
                    secondSlot = tmpSlot;
                }

                //두 슬롯이 다르다면 && 둘다 null이 아니라면
                if(firstSlot != secondSlot && firstSlot != null && secondSlot != null)
                {
                    //두개를 바꿔줘
                    InventoryManager.Instance.SwapInventoryItem(firstSlot, secondSlot);

                    //슬롯 두개 초기화
                    firstSlot = null;
                    secondSlot = null;
                }
            }
        }

        if(Input.GetKeyDown(KeyCode.E))
        {
            //e키를 누르면 주변의 컬라이더를 가져온다
            Collider2D[] cols = Physics2D.OverlapCircleAll(player.transform.position, 1f);

            foreach (var col in cols)
            {
                if(col.gameObject.CompareTag("BakeryHouse"))
                {
                    //만약 빵집오브젝트라면
                    player.Teleport(player.bakeryTpPos, player.bakeryVcamConfiner);
                    break;
                }
                else if(col.gameObject.CompareTag("Fry"))
                {
                    fryUI.alpha = 1;
                    fryUI.interactable = true;
                    fryUI.blocksRaycasts = true;
                    isUIOpen = true;
                    break;
                }
                else if(col.gameObject.CompareTag("Oven"))
                {
                    ovenUI.alpha = 1;
                    ovenUI.interactable = true;
                    ovenUI.blocksRaycasts = true;
                    isUIOpen = true;
                    break;
                }
                else if(col.gameObject.CompareTag("Doma"))
                {
                    domaUI.alpha = 1;
                    domaUI.interactable = true;
                    domaUI.blocksRaycasts = true;
                    isUIOpen = true;
                    break;
                }
                else if(col.gameObject.CompareTag("HorseCar"))
                {
                    //마차를 탔을 때 해줄 일을 하면 댐
                }
            }
        }
    }
}
