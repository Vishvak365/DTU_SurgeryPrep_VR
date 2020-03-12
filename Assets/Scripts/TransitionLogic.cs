using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionLogic : MonoBehaviour
{

    public GameObject N, NE, E, SE, S, SW, W, NW;

    public Camera mainCamera;

    /*
     * Update surrounding transition blocks based on current node # and # of objects in square matrix 
     * 
     * Square matrix of form
     * 0 1 2
     * 3 4 5
     * 6 7 8
     */

    void Start()
    {
        DataCollection.ME.currentNode = DataCollection.ME.skyboxMaterials.Count / 2;

        mainCamera.GetComponent<Skybox>().material = DataCollection.ME.skyboxMaterials[DataCollection.ME.currentNode];
    }

    public void UpdateTransitions()
    {
        int node = DataCollection.ME.currentNode;

        //side x side square matrix
        int totalLocs = DataCollection.ME.skyboxMaterials.Count;
        int side = (int)Mathf.Sqrt(totalLocs);

        mainCamera.GetComponent<Skybox>().material = DataCollection.ME.skyboxMaterials[node];

        int[] directions = GetDirection(node, side);
        int horizontal = directions[0];
        int vertical = directions[1];
        

        ResetAll();


        if(horizontal == 1) //right side
        {
            if(vertical == 1) //top right
            {
                Activate(W);

                Activate(SW);
                Activate(S);

            }

            if(vertical == 0) // center right
            {
                Activate(W);

                Activate(SW);
                Activate(S);

                Activate(NW);
                Activate(N);
            }

            if(vertical == -1) //bottom right
            {
                Activate(W);

                Activate(NW);
                Activate(N);
            }
        } 
        else if(horizontal == 0) //central column
        {
            if (vertical == 1) //top center
            {
                Activate(W);
                Activate(E);

                Activate(SW);
                Activate(S);
                Activate(SE);
            }

            if (vertical == 0) // central + central
            {
                Activate(W);
                Activate(E);

                Activate(SW);
                Activate(S);
                Activate(SE);

                Activate(NW);
                Activate(N);
                Activate(NE);
            }

            if (vertical == -1) //bottom central
            {
                Activate(W);
                Activate(E);

                Activate(NW);
                Activate(N);
                Activate(NE);

            }
        }
        else if(horizontal == -1) //left side
        {
            if (vertical == 1) //top left
            {
                Activate(E);

                Activate(S);
                Activate(SE);
            }

            if (vertical == 0) // central left
            {
                Activate(E);

                Activate(S);
                Activate(SE);

                Activate(N);
                Activate(NE);
            }

            if (vertical == -1) //bottom left
            {
                Activate(E);

                Activate(N);
                Activate(NE);

            }
        }

    }


    private void Activate(GameObject transitionObject)
    {
        transitionObject.GetComponent<Renderer>().enabled = true;
    }

    private void ResetAll()
    {
        N.GetComponent<Renderer>().enabled = false;

        NE.GetComponent<Renderer>().enabled = false;

        E.GetComponent<Renderer>().enabled = false;

        SE.GetComponent<Renderer>().enabled = false;

        S.GetComponent<Renderer>().enabled = false;

        SW.GetComponent<Renderer>().enabled = false;

        W.GetComponent<Renderer>().enabled = false;

        NW.GetComponent<Renderer>().enabled = false;
    }

    public int[] GetDirection(int n, int side)
    {


        int horizontal;
        int vertical;

        if (n % side == 0) //LHS of Square Matrix
        {
            horizontal = -1;
        }
        else if (n % side == side - 1) //RHS of Square Matrix
        {
            horizontal = 1;
        }
        else //central column
        {
            horizontal = 0;
        }

        if (n / side == 0) //top row
        {
            vertical = 1;
        }
        else if (n / side >= side - 1) //bottom row
        {
            vertical = -1;
        }
        else //central row
        {
            vertical = 0;
        }

        int[] arr = new int[] {horizontal, vertical};
        return arr;
    }
}
