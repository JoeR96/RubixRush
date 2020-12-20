using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathMenu : MonoBehaviour
{
    public void DeathReset()
    {
        Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        //player.ResetFunction();

    }
}
