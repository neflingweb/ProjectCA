using UnityEngine;
using System.Collections;

public class GoldPerSec : MonoBehaviour {


    public UnityEngine.UI.Text metalPerSec;
    public UnityEngine.UI.Text crystalPerSec;
    public Click click;
    public ItemManager[] items;
    public Technologies[] tech;


    // Use this for initialization
    void Start()
    {
        StartCoroutine(AutoTick());
    }

    // Update is called once per frame
    void Update()
    {
        metalPerSec.text = "Mps: " + getMetalPerSec();
        crystalPerSec.text = "Cps: " + getCrystalPerSec();
        //mDisplay.text = getGoldPerSec() + " DC/sec";
    }

    public double getGlobalMetalBonus()
    {
        double metalBonus = 0;
        double baseBonus = 1;
        foreach (Technologies item in tech)
        {
            metalBonus = metalBonus + (item.techLevel * item.metalBonusPerLvl);
        }
        return metalBonus + baseBonus;
    }

    public double getGlobalCrystalBonus()
    {
        double crystalBonus = 0;
        double baseBonus = 1;
        foreach (Technologies item in tech)
        {
            crystalBonus = crystalBonus + (item.techLevel * item.crystalBonusPerLvl);
        }
        return crystalBonus + baseBonus;
    }




    public double getMetalPerSec() {
        double tick = 0;
        foreach (ItemManager item in items) {
            tick += (item.buildingLevel * item.tickMetalValue);
            
        }
        if (getGlobalMetalBonus() <= 1)
        {
            return tick * getGlobalMetalBonus();
        }
        else {
            return tick * getGlobalMetalBonus() - tick;
        }


        }
    
    public double getCrystalPerSec()
    {
        double tick = 0;
        foreach (ItemManager item in items)
        {
            tick += (item.buildingLevel * item.tickCrystalValue);
        }
        if (getGlobalCrystalBonus() <= 1)
        {
            return tick * getGlobalCrystalBonus();
        }
        else
        {
            return tick * getGlobalCrystalBonus() - tick;
        }
    }

    public void AutoResourcePerSec() {
        click.metal += getMetalPerSec()  / 10;
        click.crystal += getCrystalPerSec()  / 10;
       

    }

    IEnumerator AutoTick() {
        while (true) {
            AutoResourcePerSec();
            yield return new WaitForSeconds(0.10f);
        }
    }






}






