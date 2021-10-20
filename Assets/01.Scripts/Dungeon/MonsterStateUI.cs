using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;   

public class MonsterStateUI : MonoBehaviour
{
    public Text mName;
    public Image mImage;
    public List<Slider> stateSliders;
    public float hp;
    public float mp;
    public float attackDamage;
    public float defense;
    public float attackTime;

    public List<Tween> readyAttack;

    public void Start()
    {
        readyAttack = new List<Tween>();
        readyAttack.Add(stateSliders[2].DOValue(1,attackTime).SetEase(Ease.Linear).OnComplete(() =>
        {
            SetMonsterAttack();
        }));
        readyAttack.Add(stateSliders[2].DOValue(0,.5f).SetEase(Ease.Linear));
        readyAttack[1].Pause().SetAutoKill(false);
        readyAttack[0].SetAutoKill(false);
    }


    public void SetMonsterAttack()
    {
        //transform.DOMoveX(transform.position.x - 6.5f, .8f);
        DungeonUIManager.instance.SetDefaultUI();
        for (int i = 0; i < 3; i++)
        {
            DungeonUIManager.instance.StateTweens[i].Pause();
        }
       
        StartCoroutine(AttackMonster());
    }

    public IEnumerator AttackMonster()
    {
        yield return new WaitForSeconds(.8f);
        readyAttack[1].Rewind();
        readyAttack[1].Play().OnComplete(() =>
        {
            readyAttack[0].Rewind();
        });
        Sequence sequence = DOTween.Sequence();
        sequence.Append(DungeonUIManager.instance.monsterObj.transform.DOMoveX
            (DungeonUIManager.instance.monsterObj.transform.position.x - 10f, .3f).SetLoops(2, LoopType.Yoyo));
        sequence.Insert(.2f, Camera.main.DOShakeRotation(.1f, 5f));
        yield return new WaitForSeconds(1f);
        //Debug.Log("Rewind");
        StartCoroutine(EndAttackMonster());
    }

    public IEnumerator EndAttackMonster()
    {
        transform.DOMoveX(transform.position.x + 6.5f, .8f);
        for (int i = 0; i < 3; i++)
        {
            DungeonUIManager.instance.StateTweens[i].Play();
        }
        yield return null;
        //Debug.Log("Play");
        readyAttack[0].Play();
    }
}
