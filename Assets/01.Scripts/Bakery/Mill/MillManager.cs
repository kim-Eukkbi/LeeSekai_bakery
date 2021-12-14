using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MillManager : MonoBehaviour
{
    //�̳��� ���ۼ� UI�� ������ �������ش�

    //�ؾ��� �� ������ ����Ʈ�� UI����
    //�����ǿ� ���� ���UI ���� �� ����

    //�����۵��� ����Ʈ
    public List<MillItem> millItems = new List<MillItem>();
    //������ ����Ʈ
    public List<IngredientItem> ingredientItems = new List<IngredientItem>();
    //��� ������ ������ �θ�
    public Transform ingredintParentTrm;
    //������
    public IngredientItem ingredientItemPrefab;

    [Header("���ҽ��������� �ε��� ��ġ")]
    public string resourceName;

    //������ ����Ʈ
    [SerializeField]
    private List<RecipeSO> recipes;

    //���콺�� Ŭ���ߴ� �� ����
    private float startPos;
    private float endPos;

    private MillItem currentItem { get { return millItems[3]; } }

    [Header("UI����")]
    //�� �̸� �ؽ�Ʈ
    public Text breadName;
    public Button makeBtn;

    private CanvasGroup canvasGroup;

    //�ڵ尡 ������� �����̴� �κ��� �и�������
    private MillUIMove uIMove;

    private void Start()
    {
        //����Ʈ�� �ε����ش�
        //������ �°� ���ҽ��� ���� �ε�������
        recipes = Resources.Load<RecipeListSO>(resourceName).recipes;

        uIMove = GetComponent<MillUIMove>();

        for (int i = 0; i < recipes.Count; i++)
        {
            millItems[i + 2].UpdateUI(recipes[i]);
        }

        //������ �����ش�
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
                //---> �巡��
                uIMove.MoveRight();
            }
            else if (startPos > endPos)
            {
                //<--- �巡��
                uIMove.MoveLeft();
            }

            UpdateUI();

            if(currentItem.recipe != null)
            {
                MakeIngredientItemUIs();
            }
            else
            {
                //�ִٸ�
                if (ingredientItems.Count > 0)
                {
                    //�� ����
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
        //������ �� �̸��� ������ ��Ÿ������

        if (currentItem.recipe == null)
        {
            breadName.text = "�� �� ����";
        }
        else
        {
            breadName.text = currentItem.recipe.bread.itemName;
        }
    }

    private void MakeBread()
    {
        //��ᰡ �� �ִٸ�
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

        //���� ���õ� ���� �ʿ��� ��Ḯ��Ʈ
        foreach (var ingredient in ingredients)
        {
            //���ٸ�
            if(InventoryManager.Instance.FindSameItemSlot(ingredient) == null)
            {
                //�������
                return false;
            }
        }

        return true;
    }

    //��� ������ �°� ������ �������ִ� �Լ���
    private void MakeIngredientItemUIs()
    {
        List<IngredientSO> ingredients = currentItem.recipe.ingredients;

        //�ִٸ�
        if (ingredientItems.Count > 0)
        {
            //�� ����
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
