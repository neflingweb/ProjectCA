using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Technologies : MonoBehaviour {

    public UnityEngine.UI.Text techInfo;
    public Click click;
    private double baseMCost;
    private double baseCCost;
    public double mCost;
    public double cCost;
    public string techName;
    public int metalBonusPerLvl;
    public int crystalBonusPerLvl;
    public int techLevel;
    public Color standard;
    public Color affordable;
    

    // Use this for initialization
    void Start () {
        baseMCost = mCost;
        baseCCost = cCost;
        
       
    }
	
	// Update is called once per frame
	void Update () {
        techInfo.text = techName + " (" + "Level " + techLevel + ")" + "    Metal: " + mCost + " Crystal: " + cCost + "\n Current bonus: " + (techLevel)*2 + "x";
         if (click.crystal >= cCost & click.metal >= mCost)
         {
             GetComponent<Image>().color = affordable;
         }
         else
         {
             GetComponent<Image>().color = standard;
         }
         
   }


    public void PurschasedItem()
    {
        if (click.metal >= mCost && click.crystal >= cCost)
        {
            click.metal -= mCost;
            click.crystal -= cCost;
            techLevel += 1;
            mCost = System.Math.Round(baseMCost * Mathf.Pow(10.7f, techLevel));
            cCost = System.Math.Round(baseCCost * Mathf.Pow(10.7f, techLevel));
            
            
            
            
        }
    }


    
}
