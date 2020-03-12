using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Used to keep track of pertinent data at any point in time during a run.
public class DataCollection : MonoBehaviour
{

    public static DataCollection ME;

    public int currentNode;

    public List<Material> skyboxMaterials;

    public Dictionary<string, float> timeSpentPerObject = new Dictionary<string, float>();

    public Dictionary<int, float> timeSpentPerNode = new Dictionary<int, float>();


    void Awake()
    {

        if (ME != null)
            GameObject.Destroy(ME);
        else
            ME = this;

        DontDestroyOnLoad(this);

    }

    void Start()
    {

    }

    private void Update()
    {
    }



    public override string ToString()
    {
        string output = "";

        output += "Node count: " + skyboxMaterials.Count.ToString() + "\n";

        output += "Object Info:\n";
        if (timeSpentPerObject.Count == 0)
        {
            output += "no info recorded\n";
        }
        else
        {
            foreach (KeyValuePair<string, float> obj in timeSpentPerObject)
            {
                output += "Object: " + obj.Key + " Seconds: " + obj.Value.ToString() + "\n";
            }
        }



        output += "Node Info:\n";
        if (timeSpentPerNode.Count == 0)
        {
            output += "no info recorded\n";
        }
        else
        {
            foreach (KeyValuePair<int, float> node in timeSpentPerNode)
            {
                output += "Node #" + node.Key.ToString() + " Seconds: " + node.Value.ToString() + "\n";
            }
        }


        return output;
    }
}
