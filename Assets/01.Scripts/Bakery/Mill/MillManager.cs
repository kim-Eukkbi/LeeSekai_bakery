using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MillManager : MonoBehaviour
{
    public static MillManager Instance = null;

    //이놈은 제작소 UI의 모든것을 관리해준다

    //해야할 일 레시피 리스트랑 UI연동
    //레시피에 따라서 재료UI 생성 및 삭제

    //아이템들의 리스트
    public List<MillItem> items = new List<MillItem>();

    //레시피 리스트
    [SerializeField]
    private List<RecipeSO> recipes;

    //마우스를 클릭했다 땐 지점
    private float startPos;
    private float endPos;

    //코드가 길어지니 움직이는 부분을 분리해주자
    private MillUIMove uIMove;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }

        //리스트를 로드해준다
        recipes = Resources.Load<RecipeListSO>(typeof(RecipeListSO).Name).recipes;

        uIMove = GetComponent<MillUIMove>();
    }

    private void Start()
    {
        //정보를 보내준다
        uIMove.SetMillItems(items);
    }

    void Update()
    {
        //나중에 InputManager를 만들자
        if (Input.GetMouseButtonDown(0))
        {
            startPos = Input.mousePosition.x;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            endPos = Input.mousePosition.x;

            if (startPos < endPos)
            {
                //---> 드래그
                uIMove.MoveRight();
            }
            else if (startPos > endPos)
            {
                //<--- 드래그
                uIMove.MoveLeft();
            }
        }
    }
}
