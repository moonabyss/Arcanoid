using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus_long : Bonus {

    protected override void Action()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<BonusSize>().IncreaseSize();
        Destroy(gameObject);
    }

    


}
