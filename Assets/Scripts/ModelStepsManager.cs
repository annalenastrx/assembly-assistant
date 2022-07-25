using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ModelStepsManager : MonoBehaviour
{
    [Header("Robot")]
    public GameObject[] robotModelSteps;
    public int[] robotShowing;
    public int[] robotPointing;

    [Header("Tractor")]
    public GameObject[] tractorModelSteps;
    public int[] tractorrShowing;
    public int[] tractorPointing;

    [Header("Grader")]
    public GameObject[] graderModelSteps;
    public int[] graderShowing;
    public int[] graderPointing;

    [Header("Quad")]
    public GameObject[] quadModelSteps;
    public int[] quadShowing;
    public int[] quadPointing;

    [Header("Drone")]
    public GameObject[] droneModelSteps;
    public int[] droneShowing;
    public int[] dronePointing;

    [Header("Fly")]
    public GameObject[] flyModelSteps;
    public int[] flyShowing;
    public int[] flyPointing;

    [Header("Plane")]
    public GameObject[] planeModelSteps;
    public int[] planeShowing;
    public int[] planePointing;

    [Header("Motorcycle")]
    public GameObject[] motorcycleModelSteps;
    public int[] motorcycleShowing;
    public int[] motorcyclePointing;

    [Header("Excavator")]
    public GameObject[] excavatorModelSteps;
    public int[] excavatorShowing;
    public int[] excavatorPointing;

    [Header("Seaplane")]
    public GameObject[] seaplaneModelSteps;
    public int[] seaplaneShowing;
    public int[] seaplanePointing;

    private Dictionary<int, GameObject> instructionToModel;
    
    /// <summary>
    /// This method creates a dictionary of virtual model visualization steps 
    /// (displayed in front of the avatar) and voice instruction numbers where model
    /// steps should be enabled.
    /// </summary>
    /// <param name="model">The model name.</param>
    /// <param name="pointing">The instruction mode - true: pointing, false: building.</param>
    /// <returns>A dictionary of virtual model and instruction number.</returns>
    public Dictionary<int, GameObject> GetModelSteps(string model, bool pointing)
    {
        instructionToModel = new Dictionary<int, GameObject>();

        switch (model)
        {
            case "robot":
                for (int i = 0; i < robotModelSteps.Length; i++)
                {
                    if (robotModelSteps[i] != null)
                    {
                        if (pointing)
                        {
                            instructionToModel.Add(robotPointing[i], robotModelSteps[i]);
                        }
                        else
                        {
                            instructionToModel.Add(robotShowing[i], robotModelSteps[i]);
                        }
                        robotModelSteps[i].SetActive(false);
                    }
                }
                return instructionToModel;

            case "tractor":
                for (int i = 0; i < tractorModelSteps.Length; i++)
                {
                    if (tractorModelSteps[i] != null)
                    {
                        if (pointing)
                        {
                            instructionToModel.Add(tractorPointing[i], tractorModelSteps[i]);
                        }
                        else
                        {
                            instructionToModel.Add(tractorrShowing[i], tractorModelSteps[i]);
                        }
                        tractorModelSteps[i].SetActive(false);
                    }
                }
                return instructionToModel;

            case "grader":
                for (int i = 0; i < graderModelSteps.Length; i++)
                {
                    if (graderModelSteps[i] != null)
                    {
                        if (pointing)
                        {
                            instructionToModel.Add(graderPointing[i], graderModelSteps[i]);
                        }
                        else
                        {
                            instructionToModel.Add(graderShowing[i], graderModelSteps[i]);
                        }
                        graderModelSteps[i].SetActive(false);
                    }

                }
                return instructionToModel;

            case "quad":
                for (int i = 0; i < quadModelSteps.Length; i++)
                {
                    if (quadModelSteps[i] != null)
                    {
                        if (pointing)
                        {
                            instructionToModel.Add(quadPointing[i], quadModelSteps[i]);
                        }
                        else
                        {
                            instructionToModel.Add(quadShowing[i], quadModelSteps[i]);
                        }
                        quadModelSteps[i].SetActive(false);
                    }

                }
                return instructionToModel;

            case "drone":
                for (int i = 0; i < droneModelSteps.Length; i++)
                {
                    if (droneModelSteps[i] != null)
                    {
                        if (pointing)
                        {
                            instructionToModel.Add(dronePointing[i], droneModelSteps[i]);
                        }
                        else
                        {
                            instructionToModel.Add(droneShowing[i], droneModelSteps[i]);
                        }
                        droneModelSteps[i].SetActive(false);
                    }

                }
                return instructionToModel;

            case "fly":
                for (int i = 0; i < flyModelSteps.Length; i++)
                {
                    if (flyModelSteps[i] != null)
                    {
                        if (pointing)
                        {
                            instructionToModel.Add(flyPointing[i], flyModelSteps[i]);
                        }
                        else
                        {
                            instructionToModel.Add(flyShowing[i], flyModelSteps[i]);
                        }
                        flyModelSteps[i].SetActive(false);
                    }

                }
                return instructionToModel;

            case "plane":
                for (int i = 0; i < planeModelSteps.Length; i++)
                {
                    if (planeModelSteps[i] != null)
                    {
                        if (pointing)
                        {
                            instructionToModel.Add(planePointing[i], planeModelSteps[i]);
                        }
                        else
                        {
                            instructionToModel.Add(planeShowing[i], planeModelSteps[i]);
                        }
                        planeModelSteps[i].SetActive(false);
                    }

                }
                return instructionToModel;

            case "motorcycle":
                for (int i = 0; i < motorcycleModelSteps.Length; i++)
                {
                    if (motorcycleModelSteps[i] != null)
                    {
                        if (pointing)
                        {
                            instructionToModel.Add(motorcyclePointing[i], motorcycleModelSteps[i]);
                        }
                        else
                        {
                            instructionToModel.Add(motorcycleShowing[i], motorcycleModelSteps[i]);
                        }
                        motorcycleModelSteps[i].SetActive(false);
                    }

                }
                return instructionToModel;

            case "excavator":
                for (int i = 0; i < excavatorModelSteps.Length; i++)
                {
                    if (excavatorModelSteps[i] != null)
                    {
                        if (pointing)
                        {
                            instructionToModel.Add(excavatorPointing[i], excavatorModelSteps[i]);
                        }
                        else
                        {
                            instructionToModel.Add(excavatorShowing[i], excavatorModelSteps[i]);
                        }
                        excavatorModelSteps[i].SetActive(false);
                    }

                }
                return instructionToModel;

            case "seaplane":
                for (int i = 0; i < seaplaneModelSteps.Length; i++)
                {
                    if (seaplaneModelSteps[i] != null)
                    {
                        if (pointing)
                        {
                            instructionToModel.Add(seaplanePointing[i], seaplaneModelSteps[i]);
                        }
                        else
                        {
                            instructionToModel.Add(seaplaneShowing[i], seaplaneModelSteps[i]);
                        }
                        seaplaneModelSteps[i].SetActive(false);
                    }

                }
                return instructionToModel;

            default:
                return null;

        }
    }
}
