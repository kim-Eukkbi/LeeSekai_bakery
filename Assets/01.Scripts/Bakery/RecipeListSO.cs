using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BakerySO/RecipeListSO")]
public class RecipeListSO : ScriptableObject
{
    //�����ǵ��� ����Ʈ ���߿� ���ҽ��ε� �Ἥ �ε��Ұ���
    public List<RecipeSO> recipes;
}
