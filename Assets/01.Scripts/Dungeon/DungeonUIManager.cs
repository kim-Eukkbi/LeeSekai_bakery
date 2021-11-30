using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DungeonUIManager : MonoBehaviour
{
    public static DungeonUIManager instance;
    public void Awake()
    {
        if (instance != null)
        {
            Debug.Log("�ټ��� UImanager�� �������Դϴ� �ϳ��� ����� �����մϴ�");
            Destroy(instance);
        }
        instance = this;
    }

    [Header("ĳ����")]
    public GameObject fightPanel;
    public GameObject skillPanel;
    public GameObject characterStatePanel;
    public List<GameObject> PlayerObjs;
    public List<GameObject> ponCharacterStateObjs;
    public List<StateUI> characterStateObjs;
    public List<GameObject> fightbuttons;

    [Header("����")]
    public GameObject monsterObj;
    public GameObject monsterStateUIobj;


    [Header("ECT")]
    public Canvas mainCanvas;
    public List<Tween> StateTweens;
    [HideInInspector]
    public StateUI currentCharacter;
    [HideInInspector]
    public GameObject currentPlayer;
    private float playerMaxHp;
    private float monsterMaxHp;
    public void Start()
    {
        StateTweens = new List<Tween>();
        StartCoroutine(UpStateUI());
        StartCoroutine(StartFightProcess());
        monsterMaxHp = monsterStateUIobj.GetComponent<MonsterStateUI>().hp;
    }

    public void AttackEachOther(bool isPlayer) //���� �����°� ����ϴ� �κ�
    {
        if(isPlayer)
        {
            MonsterStateUI monsterStateUI = monsterStateUIobj.GetComponent<MonsterStateUI>();
            monsterStateUI.stateSliders[0].DOValue((monsterStateUI.hp - currentCharacter.attackDamage) / monsterMaxHp, .8f);
            if(monsterStateUI.hp <= 0)
            {
                monsterStateUI.DeadMonster();
            }
            else
            {
                monsterStateUI.hp -= currentCharacter.attackDamage;
            } 
        }
        else
        {
            currentCharacter.stateSliders[0].DOValue((currentCharacter.hp - monsterStateUIobj.GetComponent<MonsterStateUI>().attackDamage) / playerMaxHp, .8f);
            currentCharacter.hp -= monsterStateUIobj.GetComponent<MonsterStateUI>().attackDamage;
        }
    }

    public void DownFightUI() //�غ�Ǿ����� �Ŵ��� ������ �Լ�
    {
        Sequence Uiseq = DOTween.Sequence();
        foreach (var item in fightbuttons) item.GetComponent<Button>().interactable = false;
        Uiseq.Append(fightbuttons[3].transform.DOMoveY(fightbuttons[3].transform.position.y - 3.1f, .5f));
        Uiseq.Insert(.1f, fightbuttons[2].transform.DOMoveY(fightbuttons[2].transform.position.y - 3.1f, .5f));
        Uiseq.Insert(.2f, fightbuttons[1].transform.DOMoveY(fightbuttons[1].transform.position.y - 3.1f, .5f));
        Uiseq.Insert(.3f, fightbuttons[0].transform.DOMoveY(fightbuttons[0].transform.position.y - 3.1f, .5f));
    }

    public IEnumerator UpStateUI() // �� ó�� StateUI�� ����� �ø��� �Լ�
    {
        yield return null;
        Sequence Main = DOTween.Sequence();
        Main.Append(characterStateObjs[0].transform.DOMove(ponCharacterStateObjs[0].transform.position, .5f));
        Main.Insert(.1f, characterStateObjs[1].transform.DOMove(ponCharacterStateObjs[1].transform.position, .5f));
        Main.Insert(.2f, characterStateObjs[2].transform.DOMove(ponCharacterStateObjs[2].transform.position, .5f));
        Main.Join(monsterStateUIobj.transform.DOMoveX(monsterStateUIobj.transform.position.x - 6.5f, .8f));
    }

    #region ���� ����
    public IEnumerator StartFightProcess() // UpStateUI �� ������ ���� ��ٸ� ���Ŀ� ������ �غ��ϴ� �Լ�
    {
        yield return new WaitForSeconds(.8f);
        for (int i = 0; i < characterStateObjs.Count; i++)
        {
            int a = i;
            StateTweens.Add(characterStateObjs[a].stateSliders[2]
            .DOValue(1, characterStateObjs[a].attackTime).SetEase(Ease.Linear).OnComplete(() => //�÷��̾��� ������������ ä��� �ڵ�
            {
                monsterStateUIobj.GetComponent<MonsterStateUI>().readyAttack[0].Pause(); // �÷��̾��� ������������ �� á�ٸ� ������ ���� �������� ����
                SetFightUI(a); //�����Ŵ��� �ø� a�� 3���� �ϳ��� �˱� ���� Index
            }));
            StateTweens[i].SetAutoKill(false); //��Ʈ���� Rewind ��Ű�� ���� ����ų ����
        }

        for (int i = 0; i < characterStateObjs.Count; i++) //���� �������� �� á���� �ڿ������� �ٿ��ִ� ��Ʈ���� �߰�
        {
            int a = i; //��Ʈ���� i �ٷ� ���� �ȵ�
            StateTweens.Add(characterStateObjs[a].stateSliders[2].DOValue(0, .5f).SetEase(Ease.Linear));  //�ٿ��ִ� ��Ʈ���� ���� �ִ� ä��� Ʈ�� ���ʿ� �߰�
            StateTweens[3 + a].Pause().SetAutoKill(false);  //�ٷ� �پ��� �ȵǴϱ� ��Ʈ�� ���߰� �����ε� 
        }
    }
    public void SetFightUI(int i) //
    {
        for (int j = 0; j < characterStateObjs.Count; j++)
        {
            if (i == j) continue;
            StateTweens[j].Pause();
            characterStateObjs[j].transform.DOMoveY(characterStateObjs[j].transform.position.y - 5, .8f);
            //PlayerObjs[j].transform.DOMoveX(PlayerObjs[j].transform.position.x - .5f, .5f);
            ponCharacterStateObjs[j].gameObject.SetActive(false);
        }
        currentCharacter = characterStateObjs[i];
        currentPlayer = PlayerObjs[i];
        playerMaxHp = currentCharacter.hp;
        monsterObj.transform.DOMoveX(monsterObj.transform.position.x + .5f, .5f);
        StartCoroutine(SetUIs(i)); 
    }
    public IEnumerator SetUIs(int i)
    {
        yield return new WaitForSeconds(.5f);
        StateTweens[3 + i].Rewind();
        StateTweens[3 + i].Play().OnComplete(() =>
        {
            StateTweens[i].Rewind();
        });

        //PlayerObjs[i].transform.DOMoveX(PlayerObjs[i].transform.position.x + .5f, .5f);
        characterStateObjs[i].transform.DOMove(ponCharacterStateObjs[i].transform.position, .8f);
        yield return new WaitForSeconds(.8f);
        Sequence Uiseq = DOTween.Sequence();
        foreach(var item in fightbuttons) item.GetComponent<Button>().interactable = false;
        Uiseq.Append(fightbuttons[0].transform.DOMoveY(fightbuttons[0].transform.position.y + 3.1f, .5f));
        Uiseq.Insert(.1f, fightbuttons[1].transform.DOMoveY(fightbuttons[1].transform.position.y + 3.1f, .5f));
        Uiseq.Insert(.2f, fightbuttons[2].transform.DOMoveY(fightbuttons[2].transform.position.y + 3.1f, .5f));
        Uiseq.Insert(.3f, fightbuttons[3].transform.DOMoveY(fightbuttons[3].transform.position.y + 3.1f, .5f)).OnComplete(() =>
        {foreach (var item in fightbuttons) item.GetComponent<Button>().interactable = true;});
    }
    #endregion

    public IEnumerator SetDefaultUI()
    {
        //���� ����ġ
        monsterObj.transform.DOMoveX(monsterObj.transform.position.x - .5f, .5f);
        
        for (int i = 0; i < 3; i++)
        {
            int a = i;
            //�÷��̾� ������Ʈ ����ġ��
            // PlayerObjs[a].transform.DOMoveX(currentPlayer.transform.position.x - .5f, .5f);
            //StateUI ����ġ��
            characterStateObjs[a].transform.DOMoveY(ponCharacterStateObjs[a].transform.position.y, .5f);
        }
        yield return new WaitForSeconds(.5f);
        for (int i = 0; i < 3; i++) StateTweens[i].Play();
        monsterStateUIobj.GetComponent<MonsterStateUI>().readyAttack[0].Play();
    }


}
