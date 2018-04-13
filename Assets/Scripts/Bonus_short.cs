using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus_short : Bonus {

    protected override void Action()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<BonusSize>().DecreaseSize();
        Destroy(gameObject);
    }

    


}
