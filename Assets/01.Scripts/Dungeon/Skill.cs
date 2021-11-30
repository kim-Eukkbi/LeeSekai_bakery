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
        DungeonUIManager.instance.skillPanel.transform.DOMoveY
            (DungeonUIManager.instance.ponCharacterStateObjs[0].GetComponentInParent<Transform>().position.y, .5f); //전투 선택 Ui를 내렸으니까 스킬 UI를 내리는중
    }

    public IEnumerator SkillUIDown()
    {
        DungeonUIManager.instance.skillPanel.transform.DOMoveY
            (DungeonUIManager.instance.fightPanel.transform.position.y, .5f); //스킬 UI를 내리기
        yield return new WaitForSeconds(.8f);
        DungeonUIManager.instance.UPFightUI();//스킬 UI를 내렸으니까 전투 선택 UI를 다시 올려주는 함수
    }

}
