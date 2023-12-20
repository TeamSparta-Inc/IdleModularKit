using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

        CurrencyManager.instance.InitCurrencyManager();
        EquipmentManager.instance.InitEquipmentManager();
        StatusUpgradeManager.instance.InitStatusUpgradeManager();


        ES3.Save<bool>("Init_Game", true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
