using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fountain : MonoBehaviour
{

    public float heal = 1;
    public float healSpeed = 1;
    public float healRange = 1;
    public BaseController baseController;

    private bool isActive;
    private float timer = 0;

    public void Activate(bool active)
    {
        isActive = active;
    }

    private void Healing()
    {
        Unit[] units = baseController.unitsOnMap.ToArray();
        for (int index = 0; index < units.Length; index++)
        {
            if (Vector2.Distance(units[index].transform.position, transform.position) < healRange)
            {
                units[index].SetHealth(heal);
            }
        }
    }

    void Update()
    {
        if (isActive)
        {
            if (timer <= 0)
            {
                timer = healSpeed;
                Healing();
            }
            else
            {
                timer -= Time.deltaTime;
            }
        }
    }
}
