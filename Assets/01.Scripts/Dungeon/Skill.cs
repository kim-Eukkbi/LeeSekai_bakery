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
        backBtn.onClick.AddListener(() => //뒤로가기 버튼을 누르면 실행되는 함수를 넣어줌
        {
            StartCoroutine(SkillUIDown()); //뒤로가기 버튼
        });
        DungeonUIManager.instance.fightbuttons[1].GetComponent<Button>().onClick.AddListener(() => //스킬 사용 버튼을 누르면 실행되는 함수
        {
            StartCoroutine(SkillUIUp());//스킬 UI를 올림
        });
    }

    public IEnumerator SkillUIUp()
    {
        DungeonUIManager.instance.DownFightUI(); //전투 선택 Ui를 내려주는 함수
        yield return new WaitForSeconds(.8f);
        for(int i =0; i  < 3;i ++)
        {
            skills[i].interactable = true;
            skills[i].GetComponentInChildren<Text>().text = DungeonUIManager.instance.currentCharacterStateUI.skillSets[i + 1].skillName;
        }
        DungeonUIManager.instance.skillPanel.transform.DOMoveY
            (DungeonUIManager.instance.ponCharacterStateObjs[0].GetComponentInParent<Transform>().position.y, .5f); //전투 선택 Ui를 내렸으니까 스킬 UI를 내리는중
    }

    public IEnumerator SkillUIDown()
    {
        DungeonUIManager.instance.skillPanel.transform.DOMoveY
            (DungeonUIManager.instance.fightPanel.transform.position.y, .5f); //스킬 UI를 내리기
        yield return new WaitForSeconds(1f);
        DungeonUIManager.instance.UPFightUI();//스킬 UI를 내렸으니까 전투 선택 UI를 다시 올려주는 함수
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
