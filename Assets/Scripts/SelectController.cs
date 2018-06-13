using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectController : MonoBehaviour
{

    public static SelectController Instance { get; private set; }

    public BaseController baseController;

    public List<Unit> selectedUnits { get; set; }

    private Rect rect;
    private bool draw, isActive;
    private Vector2 startPos;
    private Vector2 endPos;

    void Awake()
    {
        if (Instance == null) Instance = this;
        selectedUnits = new List<Unit>();
    }

    public void Activate(bool active)
    {
        isActive = active;
    }

    public void SelectUnit(Unit unit)
    {
        if (selectedUnits.Contains(unit))
        {
            selectedUnits.Clear();
            unit.downSelector.SetActive(false);
        }
        else
        {
            selectedUnits.Add(unit);
            unit.downSelector.SetActive(true);
        }
    }

    public void SetTarget(Unit target)
    {
        for (int index = 0; index < selectedUnits.Count; index++)
        {
            selectedUnits[index].GetComponent<MovingUnits>().SetTarget(target.transform);
        }
        selectedUnits.Clear();
    }

    private bool CheckUnit(Unit unit)
    {
        bool result = false;
        foreach (Unit u in selectedUnits)
        {
            if (u == unit) result = true;
        }
        return result;
    }

    private void Select()
    {
        for (int index = 0; index < baseController.unitsOnMap.Count; index++)
        {
            Vector2 tmp = new Vector2(Camera.main.WorldToScreenPoint(baseController.unitsOnMap[index].transform.position).x, Screen.height - Camera.main.WorldToScreenPoint(baseController.unitsOnMap[index].transform.position).y);

            if (rect.Contains(tmp))
            {
                if (selectedUnits.Count == 0)
                {
                    selectedUnits.Add(baseController.unitsOnMap[index]);
                }
                else if (!CheckUnit(baseController.unitsOnMap[index]))
                {
                    selectedUnits.Add(baseController.unitsOnMap[index]);
                }
            }
        }

        if (selectedUnits.Count > 0)
        {
            for (int index = 0; index < selectedUnits.Count; index++)
            {
                if (selectedUnits[index] != null)
                {
                    selectedUnits[index].downSelector.SetActive(true);
                }
            }
        }
    }

    private void Deselect()
    {
        if (selectedUnits.Count > 0)
        {
            for (int index = 0; index < selectedUnits.Count; index++)
            {
                if (selectedUnits[index] != null)
                {
                    selectedUnits[index].downSelector.SetActive(false);
                }
            }
        }
        rect.Set(0, 0, 0, 0);
        selectedUnits.Clear();
    }

    void OnGUI()
    {
        if (isActive)
        {
            if (Input.GetMouseButtonDown(0))
            {
                startPos = Input.mousePosition;
                draw = true;
            }

            if (Input.GetMouseButtonUp(0))
            {
                draw = false;
                Select();
            }

            if (draw)
            {
                endPos = Input.mousePosition;
                if (startPos == endPos) return;

                if (Vector2.Distance(startPos, endPos) > 10f)
                {
                    rect = new Rect(Mathf.Min(endPos.x, startPos.x),
                                    Screen.height - Mathf.Max(endPos.y, startPos.y),
                                    Mathf.Max(endPos.x, startPos.x) - Mathf.Min(endPos.x, startPos.x),
                                    Mathf.Max(endPos.y, startPos.y) - Mathf.Min(endPos.y, startPos.y)
                                    );

                    GUI.Box(rect, string.Empty);
                }
                else
                {
                    Deselect();
                }
            }
        }
    }
}
