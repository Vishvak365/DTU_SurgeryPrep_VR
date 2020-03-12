using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Attach to a gameobject to allow transitions to occur when that object is clicked
public class Transition : MonoBehaviour
{

    public Camera view;


    //how fast the interpolation increases per second
    public float speed;

    [SerializeField]
    private string direction;

    private SuperBlur.SuperBlur blur;
    private bool transitioningStart, transitioning;
    private Skybox sky;
    private TransitionLogic transitionLogic;

    // Start is called before the first frame update
    void Start()
    {
        blur = view.GetComponent<SuperBlur.SuperBlur>();
        sky = view.GetComponent<Skybox>();
        transitioningStart = false;
        transitionLogic = DataCollection.ME.GetComponent<TransitionLogic>();
    }

    private void OnMouseDown()
    {
        transitioning = true;
        transitioningStart = true;
    }

    //change node based off transition node position
    private void ChangeNodeFromDirection()
    {
        int change = 0;

        //side x side square matrix
        int totalLocs = DataCollection.ME.skyboxMaterials.Count;
        int side = (int)Mathf.Sqrt(totalLocs);

        switch (direction)
        {
            case "N":
                change = -side;
                break;
            case "NE":
                change = -side + 1;
                break;
            case "E":
                change = 1;
                break;
            case "SE":
                change = side + 1;
                break;
            case "S":
                change = side;
                break;
            case "SW":
                change = side - 1;
                break;
            case "W":
                change = -1;
                break;
            case "NW":
                change = (-side) - 1;
                break;
        }

        if (change != 0)
        {
            DataCollection.ME.currentNode += change;
        }
    }

    private void Update()
    {
        if (transitioning)
        {
            if (transitioningStart)
            {
                if(blur.interpolation <= 0.25)
                {
                    blur.interpolation += speed;
                } else if(blur.interpolation <= 0.50f)
                {
                    blur.interpolation += speed;
                    blur.iterations = 2;
                    blur.downsample = 1;
                } else if(blur.interpolation <= 0.75f)
                {
                    blur.interpolation += speed;
                    blur.iterations = 3;
                    blur.downsample = 2;
                }
                else if(blur.interpolation < 1.00f)
                {
                    if (blur.interpolation + speed < 1.00f)
                        blur.interpolation += speed;
                    else
                        blur.interpolation = 1.00f;
                    blur.iterations = 4;
                    blur.downsample = 3;
                }
                else
                {
                    transitioningStart = false;

                    ChangeNodeFromDirection();
                    transitionLogic.UpdateTransitions();
                }
            }
            else
            {
                Debug.Log(DataCollection.ME.currentNode);

                if (blur.interpolation <= 1.00f && blur.interpolation > 0.75f)
                {
                    blur.interpolation -= speed;
                } else if (blur.interpolation <= 0.75f && blur.interpolation > 0.50f)
                {
                    blur.interpolation -= speed;
                    blur.iterations = 3;
                    blur.downsample = 2;
                } else if (blur.interpolation <= 0.50f && blur.interpolation > 0.25f)
                {
                    blur.interpolation -= speed;
                    blur.iterations = 2;
                    blur.downsample = 1;
                } else if (blur.interpolation <= 0.25f && blur.interpolation > 0)
                {
                    if (blur.interpolation - speed > 0)
                    {
                        blur.interpolation -= speed;
                        blur.iterations = 1;
                        blur.downsample = 0;
                    }
                    else
                    {
                        blur.interpolation = 0;
                        transitioning = false;
                    }


                }
            }
        }
    }


}
