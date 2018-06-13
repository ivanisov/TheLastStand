using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public struct UnitWaves
{
    public List<MovingUnits> reserveUnits;
}

public class Barrack : MonoBehaviour
{

    public Unit spawnUnit;
    public Transform spawnPosition;
    public float spawnTime = 5;
    public BaseController baseController;
    public UnitWaves[] unitWawes;

    private bool isActive;

    public void ActiveBarrack()
    {
        isActive = true;
        Spawn();
    }

    public void DeactiveBarrack()
    {
        isActive = false;
        StopCoroutine(Spawning());
    }

    public void Spawn()
    {
        if (isActive && !GameManager.Instance.isGameComplete)
        {
            if (baseController.currentWaveID < unitWawes.Length)
            {
                if (unitWawes[baseController.currentWaveID].reserveUnits.Count > 0)
                {
                    MovingUnits unit = unitWawes[baseController.currentWaveID].reserveUnits[0];
                    if (unit != null)
                    {
                        unitWawes[baseController.currentWaveID].reserveUnits.Remove(unit);

                        unit.transform.SetParent(baseController.transform);
                        unit.transform.position = spawnPosition.position;
                        unit.gameObject.SetActive(true);
                        unit.ResetUnit();
                        unit.baseController = baseController;
                        baseController.AddUnit(unit);
                        StartCoroutine(Spawning());
                    }
                }
            }
            else
            {
                GameManager.Instance.CompleteGame();
            }
        }
    }

    private IEnumerator Spawning()
    {
        yield return new WaitForSeconds(spawnTime);
        Spawn();
    }
}
