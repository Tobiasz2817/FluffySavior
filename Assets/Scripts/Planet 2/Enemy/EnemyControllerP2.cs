using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControllerP2 : MonoBehaviour
{
    public void StartGame()
    {
        var enemys = FindObjectsOfType<EnemyShoot>();

        float nextInvokeShooting;
        foreach (var enemy in enemys)
        {
            nextInvokeShooting = Random.Range(2f, 7f);

            StartCoroutine(InvokeShootInTime(nextInvokeShooting,enemy));
        }
    }

    private IEnumerator InvokeShootInTime(float time,EnemyShoot enemy)
    {
        yield return new WaitForSeconds(time);
        enemy.InvokeShooting();
    }

}
