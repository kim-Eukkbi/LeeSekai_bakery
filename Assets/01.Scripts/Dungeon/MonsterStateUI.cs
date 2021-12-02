using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;   

public class MonsterStateUI : MonoBehaviour //몬스터 수치를 이니셜 라이즈 해서 가져온 뒤 필요한 작업을 함
{
    public Text mName;
    public Image mImage;
    public List<Slider> stateSliders;
    public float hp;
    public float mp;
    public float attackDamage;
    public float defense;
    public float attackTime;
    public State state;

    public List<Tween> readyAttack = new List<Tween>(); //트윈 리스트

    public void Start()
    {
        readyAttack.Add(stateSliders[2].DOValue(1,attackTime).SetEase(Ease.Linear).OnComplete(() => //몬스터의 전투게이지를 채워주는 트윈을 리스트에 추가
        {
            SetMonsterAttack();//게이지가 꽉 찼다면 몬스터가 타격하도록 함수 추가
        }));
        readyAttack.Add(stateSliders[2].DOValue(0,.5f).SetEase(Ease.Linear)); //전투 준비 게이지를 줄여주는 트윈
        readyAttack[1].Pause().SetAutoKill(false);//전투 준비를 줄이는 트윈을 바로 실행하면 안되니 중지하고 Rewind 할 수 있게 AutoKill 을 꺼줌
        readyAttack[0].SetAutoKill(false);// 마찬가지로  Rewind 할 수 있게 AutoKill 을 꺼줌
        DungeonUIManager.instance.monsterStateUIobj.transform.DOMoveX(DungeonUIManager.instance.monsterStateUIobj.transform.position.x - 6.5f, .8f); // 몬스터 UI를 셋팅
    }


    public void SetMonsterAttack() // 몬스터가 타격을 하기 위한 준비를 하는 함수
    {
        //transform.DOMoveX(transform.position.x - 6.5f, .8f);
        DungeonUIManager.instance.SetDefaultUI(); //몬스터가 때리기 때문에 모든 UI를 초기화 해줄 필요가 있음
        for (int i = 0; i < 3; i++)
        {
            DungeonUIManager.instance.StateTweens[i].Pause(); //모든 플레이어의 게이지가 차는걸 멈춤
        }
       
        StartCoroutine(AttackMonster()); // 몬스터가 타격하기 위한 함수
    }

    public IEnumerator AttackMonster()// 몬스터가 타격하기 위한 함수
    {
        yield return new WaitForSeconds(.8f);
        readyAttack[1].Rewind(); // 몬스터의 전투 준비 게이지를 줄이는 트윈을 초기화
        readyAttack[1].Play().OnComplete(() => //다 줄어들면
        {
            readyAttack[0].Rewind();//몬스터의 전투 준비 게이지를 채우는 트윈을 초기화
        });
        Sequence sequence = DOTween.Sequence();//시퀸스를 하나 만들고

        sequence.Append(DungeonUIManager.instance.monsterObj.transform.DOMoveX
            (DungeonUIManager.instance.monsterObj.transform.position.x - 10f, .3f).SetLoops(2, LoopType.Yoyo)); // 몬스터가 돌진했다가 다시 원래 위치로 돌아오는 트윈

        DungeonUIManager.instance.AttackEachOther(false);//몬스터가 때린걸 계산하기 위한 함수를 불러줌

        sequence.Insert(.2f, Camera.main.DOShakeRotation(.1f, 5f)); //카메라 셰이크 효과
        yield return new WaitForSeconds(1f);
        StartCoroutine(EndAttackMonster()); //몬스터가 때리고 나서 모든걸 다시 돌려두는 함수
    }

    public IEnumerator EndAttackMonster()//몬스터가 때리고 나서 모든걸 다시 돌려두는 함수
    {
        for (int i = 0; i < 3; i++)
        {
            DungeonUIManager.instance.StateTweens[i].Play(); // 다시 모든 캐릭터의 전투 준비 게이지를 올려주는 함수를 켜줌
        }
        yield return null; //한프레임 기다렸다가
        readyAttack[0].Play(); // 다시 몬스터도 전투준비 게이지를 채움
    }

    public void DeadMonster()// 몬스터가 죽었을때 처리를 위한 함수
    {
        DungeonUIManager.instance.monsterObj.SetActive(false); // 일단 그냥 꺼버리는 걸로 해보자
        DungeonUIManager.instance.monsterStateUIobj.transform.DOMoveX(DungeonUIManager.instance.monsterStateUIobj.transform.position.x + 6.5f, .8f); // 몬스터 UI를 다시 오른쪽으로
        readyAttack[0].Pause();
        DungeonUIManager.instance.StateTweens[0].Pause();
        DungeonUIManager.instance.StateTweens[1].Pause();
        DungeonUIManager.instance.StateTweens[2].Pause();
    }




}
