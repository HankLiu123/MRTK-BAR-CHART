using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class buttonPressed : MonoBehaviour
{
    //GameObject changeValueUi;
    public GameObject button;
    public GameObject chart;
    public GameObject changeValUi;

    public Text t1, t2, t3;
    public Button confirm;
    public InputField inputF;
    public int index;
    // Start is called before the first frame update
    void Start()
    {
        t2.text = "(There are at most" + PlayerPrefs.GetInt("size") + " bars)";
        t3.enabled = false;

        confirm.onClick.AddListener(readIndex);

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void disappear()
    {
        changeValUi.SetActive(true);
        button.SetActive(false);
        chart.SetActive(false);
    }
    public void readIndex()
    {
        index = int.Parse(inputF.text)-1;
        inputF.text= "";
       // Debug.Log("readIndex");
        confirm.onClick.RemoveAllListeners();
        
        confirm.onClick.AddListener(changeValue);
        t1.enabled = false;
        t2.enabled = false;
        t3.enabled = true;

    }
    public void changeValue()
    {
  
        startPage.barDatas[index] = int.Parse(inputF.text);
        inputF.text = "";

        confirm.onClick.RemoveAllListeners();
       // Debug.Log("changeValue");
        changeValUi.SetActive(false);
        t1.enabled = true;
        t2.enabled = true;
        t3.enabled = false;
        BarChartManager.isDisEnabled = true;
        button.SetActive(true);
        chart.SetActive(true);
        confirm.onClick.AddListener(readIndex);

    }
   
}
