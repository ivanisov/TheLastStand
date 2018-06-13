using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour
{

    public Barrack[] baracks;
    public BaseController enemyBase;
    public int currentWaveID { get; private set; }

    public List<Unit> unitsOnMap { get; set; }

    [SerializeField] private int timeToNextWave = 30;

    void Awake()
    {
        unitsOnMap = new List<Unit>();
        currentWaveID = 0;
    }

    public void AddUnit(Unit unit)
    {
        unitsOnMap.Add(unit);
    }

    public void RemoveUnit(Unit unit)
    {
        unitsOnMap.Remove(unit);
        if (unitsOnMap.Count <= 0)
        {
            for (int index = 0; index < baracks.Length; index++)
            {
                if (baracks[index].unitWawes[currentWaveID].reserveUnits.Count > 0) return;
            }
            DeactiveBaracks();
            if (baracks[0].unitWawes.Length > currentWaveID)
            {
                currentWaveID++;
            }
            Invoke("ActiveBaracks", timeToNextWave);
        }
    }

    public void ActiveBaracks()
    {
        for (int index = 0; index < baracks.Length; index++)
        {
            baracks[index].ActiveBarrack();
        }
    }

    public void DeactiveBaracks()
    {
        for (int index = 0; index < baracks.Length; index++)
        {
            baracks[index].DeactiveBarrack();
        }
        for (int index = 0; index < unitsOnMap.Count; index++)
        {
            unitsOnMap[index].gameObject.SetActive(false);
        }
    }

    public Unit GetClosestEnemy(Vector2 to)
    {
        if (unitsOnMap.Count > 0)
        {
            Vector2 minDist = unitsOnMap[0].transform.position;
            int minDistID = 0;
            for (int i = 1; i < unitsOnMap.Count; i++)
            {
                if (Vector2.Distance(unitsOnMap[i].transform.position, to) < Vector2.Distance(minDist, to))
                {
                    minDist = unitsOnMap[i].transform.position;
                    minDistID = i;
                }
            }
            return unitsOnMap[minDistID];
        }
        else
        {
            return null;
        }
    }
}
