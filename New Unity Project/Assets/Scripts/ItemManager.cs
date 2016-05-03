using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour {

    public UnityEngine.UI.Text itemInfo;
    public Click click;
    public double mCost;
    public double cCost;
    public int tickMetalValue;
    public int tickCrystalValue;
    public int buildingLevel;
    public string buildingName;
    private double baseMCost;
    private double baseCCost;
    public Color standard;
    public Color affordable;

    // Use this for initialization
    void Start () {
        baseMCost = mCost;
        baseCCost = cCost;
    }
	
	// Update is called once per frame
	void Update () {
        itemInfo.text = buildingName +" (" +"Level " + buildingLevel + ")" + "\nMetal: " + mCost +" Crystal: " + cCost;
        if (click.crystal >= cCost & click.metal >= mCost )
        {
            GetComponent<Image>().color = affordable;
        }
        else{
            GetComponent<Image>().color = standard;
        }        
    }

    public void PurschasedItem() {
        if (click.metal >= mCost && click.crystal >= cCost) {
            click.metal -= mCost;
            click.crystal -= cCost;
            buildingLevel += 1;
            mCost = System.Math.Round(baseMCost * Mathf.Pow(1.5f, buildingLevel));
            cCost = System.Math.Round(baseCCost * Mathf.Pow(1.5f, buildingLevel));
        }
        //check if the level is 10, 50 ,etc.
        if (buildingLevel == 100)
        {
            tickMetalValue = tickMetalValue * 100;
            tickCrystalValue = tickCrystalValue * 100;
        }
        else if (buildingLevel == 75)
        {
            tickMetalValue = tickMetalValue * 35;
            tickCrystalValue = tickCrystalValue * 35;
        }
        else if (buildingLevel == 50)
        {
            tickMetalValue = tickMetalValue * 15;
            tickCrystalValue = tickCrystalValue * 15;
        }
        else if (buildingLevel == 25)
        {
            tickMetalValue = tickMetalValue * 7;
            tickCrystalValue = tickCrystalValue * 7;
        }
        else if (buildingLevel == 10)
        {
            tickMetalValue = tickMetalValue * 3;
            tickCrystalValue = tickCrystalValue * 3;
        }
    }

    
}
