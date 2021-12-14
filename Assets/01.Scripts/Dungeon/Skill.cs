using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Playables;
using Cinemachine;
using DG.Tweening;

public class Skill : MonoBehaviour
{
    public Button backBtn;
    public List<Button> skills = new List<Button>();
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
        for(int i =0; i  < 3;i ++)
        {
            skills[i].interactable = true;
            skills[i].GetComponentInChildren<Text>().text = DungeonUIManager.instance.currentCharacterStateUI.skillSets[i + 1].skillName;
        }
        DungeonUIManager.instance.skillPanel.transform.DOMoveY
            (DungeonUIManager.instance.ponCharacterStateObjs[0].GetComponentInParent<Transform>().position.y, .5f); //���� ���� Ui�� �������ϱ� ��ų UI�� ��������
    }

    public IEnumerator SkillUIDown()
    {
        DungeonUIManager.instance.skillPanel.transform.DOMoveY
            (DungeonUIManager.instance.fightPanel.transform.position.y, .5f); //��ų UI�� ������
        yield return new WaitForSeconds(1f);
        DungeonUIManager.instance.UPFightUI();//��ų UI�� �������ϱ� ���� ���� UI�� �ٽ� �÷��ִ� �Լ�
    }

    public void SkillUse(int index)
    {
        StateUI ui = DungeonUIManager.instance.currentCharacterStateUI;
        for (int i = 0; i < 3; i++)
        {
            skills[i].interactable = false;
        }

        float result = ui.mp -= ui.skillSets[index + 1].mpCost;

        if ((result < 0))
        {
            return;
        }


        DungeonUIManager.instance.currentPlayer.GetComponent<PlayableDirector>().playableAsset 
            = DungeonUIManager.instance.currentCharacterStateUI.skillSets[index +1].skillTimeLine;

        DungeonUIManager.instance.currentPlayer.GetComponent<PlayableDirector>().Play();

        for (int i = 0; i < DungeonUIManager.instance.currentCharacterStateUI.skillSets[index +1].rededTime.Count; i++)
        {
            StartCoroutine(Reded(DungeonUIManager.instance.currentCharacterStateUI.skillSets[index +1].rededTime[i]));
        }

        DungeonUIManager.instance.SkillDamageCul(DungeonUIManager.instance.currentCharacterStateUI.skillSets[index + 1].skillDamage);

        ui.stateSliders[1]
            .DOValue((result) / 100,.5f);

    }


    public IEnumerator Reded(float delay)
    {
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = DungeonUIManager.instance.vCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        yield return new WaitForSeconds(delay);
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 20f;
        cinemachineBasicMultiChannelPerlin.m_FrequencyGain = 1f;
        DungeonUIManager.instance.monsterObj.GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(.1f);
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0f;
        cinemachineBasicMultiChannelPerlin.m_FrequencyGain = 0f;
        DungeonUIManager.instance.monsterObj.GetComponent<SpriteRenderer>().color = Color.white;
    }


}
