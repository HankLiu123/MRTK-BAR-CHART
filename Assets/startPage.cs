using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class startPage : MonoBehaviour
{
    public InputField input;
    public Button confirm;
    public Text t1, t2, t3, t4, t5;
    static public  string[] barLabels;
    static public  float[] barDatas;
    public int barIndex;
    // Start is called before the first frame update
    void Start()
    {
        confirm.onClick.AddListener(readchartLabelInputOnClick);
        t2.enabled = false;
        t3.enabled = false;
        t4.enabled = false;
        t5.enabled = false;
        barIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void readchartLabelInputOnClick()
    {
        PlayerPrefs.SetString("chartLabel", input.text);
        confirm.onClick.RemoveAllListeners();
        confirm.onClick.AddListener(readPostFixInputOnClick);
        input.text = "";
        t1.enabled = false;
        t2.enabled = true;

    }

    public void readPostFixInputOnClick()
    {
        PlayerPrefs.SetString("postFix", input.text);
        confirm.onClick.RemoveAllListeners();
        confirm.onClick.AddListener(readSizeOnClick);
        input.text = "";
        t2.enabled = false;
        t3.enabled = true;
    }
    public void readSizeOnClick()
    {
        PlayerPrefs.SetInt("size", int.Parse(input.text));

        input.text = "";
        t3.enabled = false;
        t4.text = "Now, please type in " + PlayerPrefs.GetInt("size") + " bar labels:";
        int size = PlayerPrefs.GetInt("size");
        barLabels = new string[size];
        barDatas = new float[size];
        t4.enabled = true;
        confirm.onClick.RemoveAllListeners();
        confirm.onClick.AddListener(inputBarLabel);

    }

    public void inputBarLabel()
    {
        //PlayerPrefsX
        barLabels[barIndex] = input.text;

      
        input.text = "";
        
        if (barIndex == barLabels.Length - 1)
        {
            confirm.onClick.RemoveAllListeners();
            confirm.onClick.AddListener(inputBarData);
            t4.enabled = false;
            t5.enabled = true;
            this.barIndex = 0;
            t5.text = "Last, input " + PlayerPrefs.GetInt("size") + " data to chart:";
            input.text = "";

        }
        else
        {
            barIndex++;
        }



    }
    public void inputBarData()
    {
        barDatas[barIndex] = float.Parse(input.text);
        input.text = "";

       
        if (barIndex == barLabels.Length-1)
        {
            confirm.onClick.RemoveAllListeners();
            SceneManager.LoadScene("barChart");
        }
                   
            barIndex++;

    }
}
