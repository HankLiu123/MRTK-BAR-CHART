using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[HelpURL("https://tibcosoftware.github.io/Augmented-Reality/3DCharts/")]
public class BarChartManager : MonoBehaviour
{
    [Header("Bar Prefab")]
    [Tooltip("store here your Ground Plane Prefab to be used.")]
    public GameObject GroundPrefab;
    [Tooltip("this is how each Bar of the Barchart should look like.")]
    public GameObject BarPrefab;
    [Tooltip("store here a simple TextMesh to be used.")]
    public GameObject LabelPrefab;
    [Tooltip("general Chart Label, below the BarChart.")]
    public  string ChartLabel;
    [Tooltip("define if all bars should be rendered in centered mode.")]
    public  bool centered;
    [Tooltip("displayed after each Scaling Variable.")]
    public  string postFix;

    [Header("Bar Label")]
    [Tooltip("a Label Value shown at each Bar.")]
    public  string[] BarLabel;

    [Header("Bar Scaling")]
    [Tooltip("the scale Value define size for each Bar.")]
    public  float[] BarSize;

    [Header("Bar Color")]
    [Tooltip("Color to display for each Bar.")]
    public  Color[] BarColor;
    public static bool isDisEnabled;


    // Start is called before the first frame update
    void Start()
    {
        buildBar();
        isDisEnabled = false;
        
    }
     void Update()
    {
        //Debug.Log(this.isActiveAndEnabled);
 
        
        if (isDisEnabled)
        {
            Debug.Log(isDisEnabled);
            destroyOldChart();
            buildBar();
            isDisEnabled = false;
        }
        
    }
    public void destroyOldChart()
    {
        GameObject[] allCloneBar = GameObject.FindGameObjectsWithTag("cloneBar");
        GameObject[] allCloneValue = GameObject.FindGameObjectsWithTag("cloneLabel");
        GameObject[] allCloneLabel = GameObject.FindGameObjectsWithTag("Finish");

        foreach (GameObject clone in allCloneBar)
        {
            Destroy(clone);
        }
        foreach (GameObject clone in allCloneValue)
        {
            Destroy(clone);
        }
        foreach (GameObject clone in allCloneLabel)
        {
            Destroy(clone);
        }
        Destroy(GameObject.FindGameObjectWithTag("labelObj"));
    }
    public void buildBar()
    {

        postFix = PlayerPrefs.GetString("postFix");
        ChartLabel = PlayerPrefs.GetString("chartLabel");

        int size = PlayerPrefs.GetInt("size");
        BarLabel = new string[size];
        BarSize = new float[size];
        BarColor = new Color[size];


        for (int i = 0; i < size; i++)
        {
            Color color = new Color(Random.Range(0.3F, 1F), Random.Range(0.3F, 1F), Random.Range(0.3F, 1F));
            BarLabel[i] = startPage.barLabels[i];
            BarSize[i] = startPage.barDatas[i];
            BarColor[i] = color;

        }


        var scale = transform.localScale.x;
        var scale_y = transform.localScale.y;
        var scale_z = transform.localScale.z;

        this.transform.localScale = new Vector3(scale * BarSize.Length / 2, scale_y, scale_z);
        this.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        var GroundObj = Instantiate(GroundPrefab, new Vector3(transform.position.x, transform.position.y - scale_y / 2, transform.position.z), Quaternion.identity);
        Renderer rend = GroundObj.GetComponent<Renderer>();
        rend.material.SetColor("_SpecColor", new Color32(222, 222, 222, 80));


        GroundObj.transform.localScale = new Vector3(scale * BarSize.Length / 2, 0.01f * scale, scale_z / 2);
        GroundObj.GetComponent<Rigidbody>().mass = 1;
        GroundObj.transform.parent = this.transform;

        //Chart Bars
        for (int i = 0; i < BarSize.Length; i++)
        {

            // Bar Rendering
            var BarObj = Instantiate(BarPrefab, new Vector3(transform.position.x - transform.localScale.x / 2 + transform.localScale.x / BarSize.Length * i + 0.04f, transform.position.y, transform.position.z), Quaternion.identity);
            BarObj.gameObject.tag = "cloneBar";
            BarObj.transform.parent = this.transform;
            rend = BarObj.GetComponent<Renderer>();
            rend.material.SetColor("_SpecColor", BarColor[i]);

            var scaledSize = (1f / 100) * BarSize[i];
            BarObj.GetComponent<Bar>().size = scaledSize;

            BarObj.GetComponent<Bar>().scale = scale;
            BarObj.GetComponent<Bar>().centered = centered;
            BarObj.GetComponent<Bar>().displayColor = BarColor[i];
            BarObj.GetComponent<Rigidbody>().mass = 1;

            // Value Rendering
            var ValueObj = Instantiate(LabelPrefab, new Vector3(transform.position.x - transform.localScale.x / 2 + transform.localScale.x / BarSize.Length * i + 0.04f, transform.position.y + scaledSize * scale / 2, transform.position.z - transform.localScale.z / 4), Quaternion.identity);
            ValueObj.transform.localScale = new Vector3(0.09f * scale, 0.09f * scale, 0.09f * scale);
            ValueObj.GetComponent<TextMesh>().text = BarSize[i].ToString() + postFix;
            ValueObj.transform.parent = this.transform;
            ValueObj.gameObject.tag = "cloneLabel";

            // Label Rendering
            var BarLabelObj = Instantiate(LabelPrefab, new Vector3(transform.position.x - transform.localScale.x / 2 + transform.localScale.x / BarSize.Length * i + 0.04f, transform.position.y - scale_y / 2 + 0.1f * scale, transform.position.z - transform.localScale.z / 4), Quaternion.identity);
            BarLabelObj.transform.localScale = new Vector3(0.09f * scale, 0.09f * scale, 0.09f * scale);
            BarLabelObj.GetComponent<TextMesh>().text = BarLabel[i];
            BarLabelObj.transform.parent = this.transform;
            BarLabelObj.gameObject.tag = "Finish";


        }

        //Chart Label
        var LabelObj = Instantiate(LabelPrefab, new Vector3(transform.position.x, transform.position.y - scale_y / 2 - 0.1f * scale, transform.position.z - transform.localScale.z / 2), Quaternion.identity);
        LabelObj.transform.localScale = new Vector3(0.1f * scale, 0.1f * scale, 0.1f * scale);
        LabelObj.GetComponent<TextMesh>().text = ChartLabel;
        LabelObj.transform.parent = this.transform;
        LabelObj.gameObject.tag = "labelObj";
    }

}
