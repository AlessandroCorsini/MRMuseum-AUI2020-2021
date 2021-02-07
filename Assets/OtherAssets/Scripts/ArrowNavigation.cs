using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowNavigation : MonoBehaviour
{
    public GameObject arrow;
    public GameObject hoveredArrow;
    public GameObject star;
    public GameObject hoveredStar;
    public GameObject gameMenu;

    private bool starredArrow = false;
    private bool disabledArrow = false;

    private float start = 0f;
    private float end;

    void Update()
    {
        if (InputMR.isNewWaypointHappened())
        {
            setArrow();
            InputMR.clearNewWaypoint();
        }

        /*
        if (ArrowManager.isHitRaycasting())
        {
            if (ArrowManager.getRaycastedArrowName().Equals(this.name))
            {
                fakeOnTriggerEnter();
            }
        }

        if (ArrowManager.isUnhitRaycasting())
        {
            if (ArrowManager.getRaycastedArrowName().Equals(this.name))
            {
                fakeOnTriggerExit();
                ArrowManager.resetRaycastedArrowName();
            }
        }
        */
    }

    private void setArrow()
    {
        starredArrow = ArrowManager.getStarredDirections().Contains(this.name);
        disabledArrow = ArrowManager.getDisabledDirections().Contains(this.name);

        if (starredArrow)
        {
            arrow.SetActive(false);
            hoveredArrow.SetActive(false);
            star.SetActive(true);
            hoveredStar.SetActive(false);
        } else if (disabledArrow)
        {
            arrow.SetActive(false);
            hoveredArrow.SetActive(false);
            star.SetActive(false);
            hoveredStar.SetActive(false);
        } else
        {
            arrow.SetActive(true);
            hoveredArrow.SetActive(false);
            star.SetActive(false);
            hoveredStar.SetActive(false);
        }
    }

    private void OnTriggerEnter()
    {
        if (start == 0f)
        {
            start = Time.time;
        }

        if (starredArrow)
        {
            star.SetActive(false);
            hoveredStar.SetActive(true);

        } else if (!disabledArrow)
        {
            arrow.SetActive(false);
            hoveredArrow.SetActive(true);
        }
    }

    private void OnTriggerExit()
    {
        end = Time.time;

        if (starredArrow)
        {
            star.SetActive(true);
            hoveredStar.SetActive(false);
            gameMenu.SetActive(true);
            ActivityIntroduction.SetActivityName(ArrowManager.miniGameName);
        }
        else if (!disabledArrow)
        {
            arrow.SetActive(true);
            hoveredArrow.SetActive(false);
        }


        if (!disabledArrow || starredArrow)
        {
            Debug.Log("pressed time: " + (end - start));

            if (end - start >= InputMR.getStandingTime())
            {
                if (starredArrow) // to implement transactions between scenes
                {
                    if (ActivityIntroduction.GetFirstPress())
                    {
                        ActivityIntroduction.SetFirstPress(false);
                        ActivityIntroduction.setStartIntroduction();
                    }
                    else
                    {
                        PlayerType.InvokeSave(); // Saving player position
                        gameMenu.SetActive(false);
                        string nextSceneName = ArrowManager.getNextSceneName();
                        ActivityIntroduction.SetFirstPress(true);
                        LevelLoader.startTransition(nextSceneName);
                        return;
                    }
                }
                else
                {
                    gameMenu.SetActive(false);
                    ActivityIntroduction.SetFirstPress(true);
                }

                Debug.Log(this.name + ":: Event happended sent to InputMR");
                InputMR.setEventHappened(this.name);
            }
        }



        start = 0f;
    }

    /*
    private void OnMouseDown()
    {
        if (disabledArrow) return;
        
        start = Time.time;

        if (starredArrow)
        {
            star.SetActive(false);
            hoveredStar.SetActive(true);
        } else
        {
            arrow.SetActive(false);
            hoveredArrow.SetActive(true);
        }
    }

    private void OnMouseUp()
    {
        if (disabledArrow) return;

        end = Time.time;

        if (starredArrow)
        {
            star.SetActive(true);
            hoveredStar.SetActive(false);
        }
        else
        {
            arrow.SetActive(true);
            hoveredArrow.SetActive(false);
        }

        if(end - start >= InputMR.getStandingTime())
        {
            Debug.Log(this.name + ":: Event happended sent to InputMR");
            InputMR.setEventHappened(this.name);
        }
    }
    */

}
