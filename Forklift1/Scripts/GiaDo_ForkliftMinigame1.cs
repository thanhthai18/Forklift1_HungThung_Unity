using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiaDo_ForkliftMinigame1 : AbstractMyCar_Minigame
{
    private void Start()
    {
        currentLane = 3;
    }

    private void Update()
    {
        if(!GameController_ForkliftMinigame1.instance.isLose && !GameController_ForkliftMinigame1.instance.isWin && GameController_ForkliftMinigame1.instance.isBegin)
        {
            if (Input.GetMouseButtonDown(0))
            {
                mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition) - mainCamera.transform.position;
            }
            if (Input.GetMouseButtonUp(0))
            {
                lastPos = mainCamera.ScreenToWorldPoint(Input.mousePosition) - mainCamera.transform.position;

                if (mousePos.y > -2f)
                {
                    MoveCar_UpDown(mousePos, lastPos, 3, 2);

                    if (GameController_ForkliftMinigame1.instance.tutorial1.activeSelf)
                    {
                        GameController_ForkliftMinigame1.instance.tutorial1.SetActive(false);
                        GameController_ForkliftMinigame1.instance.tutorial1.transform.DOKill();
                        GameController_ForkliftMinigame1.instance.tutorial2.SetActive(false);
                        GameController_ForkliftMinigame1.instance.tutorial2.transform.DOKill();
                        GameController_ForkliftMinigame1.instance.Begin();
                    }
                }

            }

            
        }       
    }
}
