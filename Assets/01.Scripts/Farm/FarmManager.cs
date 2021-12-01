using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmManager : MonoBehaviour
{
    //���⼭�� �� ���ֳ�
    //�ϴ� Ŭ���� ���� ������� ������
    //���� �ѷ��������� �۹��� �ɾ�
    //���� �Ȼѷ��������� ���� ��
    //12345�� �۹��� ����
    //�۹��� CropManager���� �������°ɷ�

    //����ķ ��Ƴ��� ����
    private Camera mainCam;

    //�÷��̾� ��Ƴ��� ����
    public PlayerMove player;

    private void Awake()
    {
        mainCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector2 clickPos = mainCam.ScreenToWorldPoint(Input.mousePosition);

            Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit))
            {
                //print(hit.transform.gameObject.name);

                //Ŭ���� Ÿ���� �����´�
                FarmTile farmTile = hit.transform.GetComponent<FarmTile>();

                if(farmTile != null)
                {
                    //�����ִ»��¶��
                    if(farmTile.isWet)
                    {
                        //�ƹ� �۹��� �ɰ������� �ʴٸ�
                        if(!farmTile.isPlanted)
                        {
                            //���� �ȱ��� �ȿ� �ִٸ�
                            if(player.IsInRange(clickPos))
                            {
                                //�ൿ �� �� �ִ� ���¶��
                                if (player.CanAction())
                                {
                                    //���� ���õǾ��ִ� �۹��� �ɴ´�
                                    farmTile.Plant(CropManager.Instance.cropType);
                                    player.PlayAnimation(player.PlantName);
                                }
                            }
                        }
                        else
                        {
                            //�۹��� �ɰ����ִ� ���´ϱ� �۹��� ��������
                            Crop crop = farmTile.plantedCrop;
                            //���� ������ ���� ���¶��
                            if(crop.isGrowEnd)
                            {
                                //���� �ȱ��� �ȿ� �ִٸ�
                                if (player.IsInRange(clickPos))
                                {
                                    //�׸��� �ൿ�� �� �ִ� ���¶��
                                    if (player.CanAction())
                                    {
                                        //��Ȯ�ع���
                                        crop.Harvest();
                                        player.PlayAnimation(player.HarvestName);
                                        farmTile.Resetting();
                                    }
                                }
                            }
                        }
                    }
                    //�������� ���� ���¶��
                    else
                    {
                        //���� �ȱ��� �ȿ� �ִٸ�
                        if (player.IsInRange(clickPos))
                        {
                            //�׸��� �ൿ�� �� �ִ� ���¶��
                            if (player.CanAction())
                            {
                                //���� ����
                                farmTile.Water(true);
                                player.PlayAnimation(player.WaterName);
                            }
                        }
                    }
                }
            }
        }
    }
}
