using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MillManager : MonoBehaviour
{
    //이놈은 제작소 UI의 모든것을 관리해준다

    //해야할 일 레시피 리스트랑 UI연동
    //레시피에 따라서 재료UI 생성 및 삭제

    //아이템들의 리스트
    public List<MillItem> millItems = new List<MillItem>();
    //재료들의 리스트
    public List<IngredientItem> ingredientItems = new List<IngredientItem>();
    //재료 프리팹 생성할 부모
    public Transform ingredintParentTrm;
    //프리팹
    public IngredientItem ingredientItemPrefab;

    [Header("리소스폴더에서 로드할 위치")]
    public string resourceName;

    //레시피 리스트
    [SerializeField]
    private List<RecipeSO> recipes;

    //마우스를 클릭했다 땐 지점
    private float startPos;
    private float endPos;

    private MillItem currentItem { get { return millItems[3]; } }

    [Header("UI관련")]
    //빵 이름 텍스트
    public Text breadName;
    public Button makeBtn;

    private CanvasGroup canvasGroup;

    //코드가 길어지니 움직이는 부분을 분리해주자
    private MillUIMove uIMove;

    private void Start()
    {
        //리스트를 로드해준다
        //도구에 맞게 리소스는 따로 로드해주자
        recipes = Resources.Load<RecipeListSO>(resourceName).recipes;

        uIMove = GetComponent<MillUIMove>();

        for (int i = 0; i < recipes.Count; i++)
        {
            millItems[i + 2].UpdateUI(recipes[i]);
        }

        //정보를 보내준다
        uIMove.SetMillItems(millItems);

        UpdateUI();
        MakeIngredientItemUIs();

        canvasGroup = GetComponent<CanvasGroup>();

        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;

        makeBtn.onClick.AddListener(MakeBread);
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

            UpdateUI();

            if(currentItem.recipe != null)
            {
                MakeIngredientItemUIs();
            }
            else
            {
                //있다면
                if (ingredientItems.Count > 0)
                {
                    //다 날려
                    for (int i = ingredientItems.Count - 1; i >= 0; i--)
                    {
                        IngredientItem item = ingredientItems[i];

                        ingredientItems.Remove(item);
                        Destroy(item.gameObject);
                    }
                }
            }
        }
    }

    private void UpdateUI()
    {
        //아직은 빵 이름이 뭔지만 나타내주자

        if (currentItem.recipe == null)
        {
            breadName.text = "알 수 없음";
        }
        else
        {
            breadName.text = currentItem.recipe.bread.itemName;
        }
    }

    private void MakeBread()
    {
        //재료가 다 있다면
        if(CanMake())
        {
            foreach (var ingredient in currentItem.recipe.ingredients)
            {
                InventorySlot slot = InventoryManager.Instance.FindSameItemSlot(ingredient);
                slot.SubItem(1);
            }

            InventoryManager.Instance.AddItem(currentItem.recipe.bread);
        }
    }

    private bool CanMake()
    {
        List<IngredientSO> ingredients = currentItem.recipe.ingredients;

        //현재 선택된 빵에 필요한 재료리스트
        foreach (var ingredient in ingredients)
        {
            //없다면
            if(InventoryManager.Instance.FindSameItemSlot(ingredient) == null)
            {
                //못만들어
                return false;
            }
        }

        return true;
    }

    //재료 갯수에 맞게 프리팹 생성해주는 함수임
    private void MakeIngredientItemUIs()
    {
        List<IngredientSO> ingredients = currentItem.recipe.ingredients;

        //있다면
        if (ingredientItems.Count > 0)
        {
            //다 날려
            for (int i = ingredientItems.Count - 1; i >= 0; i--)
            {
                IngredientItem item = ingredientItems[i];

                ingredientItems.Remove(item);
                Destroy(item.gameObject);
            }
        }

        if(ingredients.Count > 0 && ingredients != null)
        {
            for (int i = 0; i < ingredients.Count; i++)
            {
                IngredientItem item = Instantiate(ingredientItemPrefab, ingredintParentTrm);

                int cnt = 0;

                InventorySlot sameSlot = InventoryManager.Instance.FindSameItemSlot(ingredients[i]);

                if(sameSlot != null)
                {
                    cnt = sameSlot.CurrentCount();
                }

                item.UpdateUI(ingredients[i], cnt);
                ingredientItems.Add(item);
            }
        }
    }
}
