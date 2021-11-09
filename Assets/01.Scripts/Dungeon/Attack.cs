using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class Attack : MonoBehaviour
{
    [SerializeField]
    private GameObject bullet;
    public void Start()
    {
        DungeonUIManager.instance.buttons[0].GetComponent<Button>().onClick.AddListener(() =>
        {
            StartCoroutine(AttackCo());
        });
    }
    public IEnumerator AttackCo()
    {
        DungeonUIManager.instance.DownUI();
        //fightPanel.transform.DOMoveY(characterStatePanel.transform.position.y - 5f, .8f);
        DungeonUIManager.instance.currentCharacter.transform.DOMoveY(DungeonUIManager.instance.currentCharacter.transform.position.y - 5, .8f);
        for (int i = 0; i < 3; i++)
        {
            DungeonUIManager.instance.ponCharacterStateObjs[i].SetActive(true);
        }
        yield return new WaitForSeconds(.8f);
        PlayerAttack(DungeonUIManager.instance.MeeleAttackCheck());
        for (int i = 0; i < 3; i++)
        {
            DungeonUIManager.instance.characterStateObjs[i].transform.position =
                new Vector3(DungeonUIManager.instance.ponCharacterStateObjs[i].transform.position.x,
                DungeonUIManager.instance.characterStateObjs[i].transform.position.y, DungeonUIManager.instance.characterStateObjs[i].transform.position.z);
        }

    }

    public void PlayerAttack(bool isMeeleAttack)
    {
        Sequence sequence = DOTween.Sequence();
        if (isMeeleAttack)
        {
            Vector3 originPos = DungeonUIManager.instance.currentPlayer.transform.position;
            sequence.Append(DungeonUIManager.instance.currentPlayer.transform.DOMove(DungeonUIManager.instance.monsterObj.transform.position, .3f)).SetEase(Ease.OutCirc);
            sequence.Append(DungeonUIManager.instance.currentPlayer.transform.DOMove(originPos, .5f).SetEase(Ease.InBack).OnComplete(() =>
            {
                StartCoroutine(DungeonUIManager.instance.SetDefaultUI());
            }));
            DungeonUIManager.instance.AttackEachOther(true);
            sequence.Insert(.2f, Camera.main.DOShakeRotation(.1f, 5f));  //나중에 다시 생각해보기
        }
        else
        {
            GameObject currB = Instantiate(bullet, DungeonUIManager.instance.currentPlayer.transform);
            sequence.Append(currB.transform.DOMove(DungeonUIManager.instance.monsterObj.transform.position,.3f)).SetEase(Ease.OutCirc).OnComplete(()=>
            {
                Destroy(currB, .3f);
                StartCoroutine(DungeonUIManager.instance.SetDefaultUI());
            });
            DungeonUIManager.instance.AttackEachOther(true);
            sequence.Insert(.2f, Camera.main.DOShakeRotation(.1f, 5f));
        }
    }
}
