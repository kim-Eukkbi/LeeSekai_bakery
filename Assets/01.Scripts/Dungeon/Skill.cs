using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class Skill : MonoBehaviour
{
    public Button backBtn;
    public void Start()
    {
        backBtn.onClick.AddListener(() => //�ڷΰ��� ��ư�� ������ ����Ǵ� �Լ��� �־���
        {
            StartCoroutine(SkillUIDown()); //�ڷΰ��� ��ư
        });
        DungeonUIManager.instance.fightbuttons[1].GetComponent<Button>().onClick.AddListener(() => //��ų ��� ��ư�� ������ ����Ǵ� �Լ�
        {
            StartCoroutine(SkillUIUp());//��ų UI�� �ø�
        });
    }

    public IEnumerator SkillUIUp()
    {
        DungeonUIManager.instance.DownFightUI(); //���� ���� Ui�� �����ִ� �Լ�
        yield return new WaitForSeconds(.8f);
        DungeonUIManager.instance.skillPanel.transform.DOMoveY
            (DungeonUIManager.instance.ponCharacterStateObjs[0].GetComponentInParent<Transform>().position.y, .5f); //���� ���� Ui�� �������ϱ� ��ų UI�� ��������
    }

    public IEnumerator SkillUIDown()
    {
        DungeonUIManager.instance.skillPanel.transform.DOMoveY
            (DungeonUIManager.instance.fightPanel.transform.position.y, .5f); //��ų UI�� ������
        yield return new WaitForSeconds(.8f);
        DungeonUIManager.instance.UPFightUI();//��ų UI�� �������ϱ� ���� ���� UI�� �ٽ� �÷��ִ� �Լ�
    }

}
