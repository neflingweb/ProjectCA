using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class UpgradeManager : MonoBehaviour {

    public Click click;
    public UnityEngine.UI.Text itenInfo;
    public double cost;
    public int count = 0;
    public int clickPower;
    public string itemName;
    private double baseCost;
    

    void Start() {
        baseCost = cost;
    }
    // Update is called once per frame
    void Update () {
        itenInfo.text = itemName + "\nCost:" + cost + "\nPower: +" + clickPower;
        
    }
    /*
    public void PurschasedUpgrade() {
        if (click.gold >= cost) {
            click.gold -= cost;
            count += 1;
            click.goldperclick += clickPower;
            cost = Mathf.Round(cost * 1.15f);            
            cost =Mathf.Round( baseCost * Mathf.Pow(1.15f, count));
        }
    }
    */
}
