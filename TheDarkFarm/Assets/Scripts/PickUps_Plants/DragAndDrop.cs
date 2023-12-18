using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class DragAndDrop : MonoBehaviour {

       private bool selected;

       void Update () {
              if (selected == true) {
                     Vector2 cursorPos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
                     transform.position = new Vector2 (cursorPos.x, cursorPos.y);
              }

              if ((Input.GetMouseButtonUp (0))||(!GameHandler_DayNightPhases.isDayPhase)||(GameHandler.isInside)||(GameHandler_PauseMenu.GameisPaused)) {
                     selected = false;
              }
       }

       void OnMouseOver(){
              if ((Input.GetMouseButtonDown (0))&&(GameHandler_DayNightPhases.isDayPhase)) {
                     selected = true;
              }
       }

}