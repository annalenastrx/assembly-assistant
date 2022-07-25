using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

[RequireComponent(typeof(AudioSource))]
public class AvatarControllerRemote : MonoBehaviour
{
    [Header("Building - 2D optimized")]
    [Tooltip("Order of conditions between 1 and 4!")]
    public int orderBuildingEasy = 0;
    public ModelsEasy buildingModelEasy;
    public int orderBuildingHard = 0;
    public ModelsHard buildingModelHard;

    [Header("Pointing - 3D optimized")]
    [Tooltip("Order of conditions between 1 and 4!")]
    public int orderPointingEasy = 0;
    
    public ModelsEasy pointingModelEasy;
    public int orderPointingHard = 0;
    public ModelsHard pointingModelHard;

    public enum ModelsEasy {robot, tractor, grader, quad, drone};
    public enum ModelsHard {fly, plane, motorcycle, excavator, seaplane};

    
    [Header("_________________")]
    public Animator animator;
    public Camera cam;
    public Transform targetPointing;
    public TCPTestClient tcpClient;
    public ModelStepsManager modelStepsManager;
    public AudioSource audioSourceLipSync;
    
    #region private
    private int modelCounter = 0;
    private int conditionCounter = 0;
    private int voiceInstructionCounter = 0;
    private string[,] conditions;

    private int[] voiceInstructionNumbersPointing;
    private int[] voiceInstructionNumbersBuilding;

    private string modelName;
    private bool pointing;
    private bool building;

    private GameObject previousModelStep = null;
    private Dictionary<int, GameObject> modelSteps;

    private bool nextCond = false;

    private AudioSource audioSource;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        conditions = new string[2, 4];

        // Numbers of voice instructions for all 10 models
        voiceInstructionNumbersBuilding = new int[] {36, 35, 35, 37, 31, 41, 45, 37, 43, 47};
        voiceInstructionNumbersPointing = new int[] {53, 52, 46, 49, 34, 44, 52, 43, 45, 51};


        try
        {
            if(orderBuildingEasy != 0 && orderBuildingHard != 0 && orderPointingEasy != 0 && orderPointingHard != 0)
            {
                conditions[0, orderBuildingEasy-1] = "building";
                conditions[1, orderBuildingEasy-1] = buildingModelEasy.ToString();

                conditions[0, orderBuildingHard-1] = "building";
                conditions[1, orderBuildingHard-1] = buildingModelHard.ToString();

                conditions[0, orderPointingEasy-1] = "pointing";
                conditions[1, orderPointingEasy-1] = pointingModelEasy.ToString();

                conditions[0, orderPointingHard-1] = "pointing";
                conditions[1, orderPointingHard-1] = pointingModelHard.ToString();
            }
            else{
                Debug.LogError("Condition number missing!!");
            }

        }
        catch{
            Debug.LogError("Order of conditions wrong!");
        }

        StartNextCondition();
    }

    void Update()
    {
        if (Input.GetKeyDown("space") && building)
        {
            if (nextCond)
            {
                StartNextCondition();
            }
            else
            {
                tcpClient.SendClientMessage("space");
                StartNextBuildingGesture();
            }
        }
        else if(Input.GetMouseButtonDown(0) && pointing)
        {
            if (nextCond)
            {
                StartNextCondition();
            }
            else
            {
                RaycastHit hit;
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit))
                {

                    targetPointing.position = hit.point;
                    tcpClient.SendClientMessage("pos:" + hit.point.ToString());
                    StartNextPointingGesture();
                }
            }
        }
        else if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            tcpClient.SendClientMessage("left");
            voiceInstructionCounter--;
            PlayAudioClip(false);
        }
        else if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            tcpClient.SendClientMessage("right");
            PlayAudioClip(true);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            tcpClient.SendClientMessage("down");
            if (pointing)
            {
                animator.SetBool("pointRight", false);
                animator.SetBool("pointLeft", false);
            }
        }
    }

    /// <summary>
    /// This method starts the next user study condition (conditions are a combination of model 
    /// and instruction mode - pointing or building). It exports assembly time of the previous round, 
    /// resets all instruction counters and starts the first instruction of the next condition. 
    /// </summary>
    public void StartNextCondition()
    {
        if (conditionCounter == conditions.Length-1){
            animator.SetBool("end", true);
            return;
        }
        voiceInstructionCounter = 0;
                    
        modelName = conditions[1,conditionCounter];
        modelCounter = GetModelId(modelName);
        
        animator.SetInteger("instructionCounter", voiceInstructionCounter);

        if(conditions[0,conditionCounter] == "building")
        {
            animator.SetBool("build", true);
            animator.SetInteger("modelCounter", modelCounter);
            
            building = true;
            pointing = false;
        }
        else
        {
            building = false;
            pointing = true;
        }
        modelSteps = modelStepsManager.GetModelSteps(modelName, pointing);
        PlayAudioClip(true);
        nextCond = false;
    }
    
    /// <summary>
    /// This method starts the next animation for the building condition.
    /// </summary>
    public void StartNextBuildingGesture()
    {
        animator.SetInteger("instructionCounter", voiceInstructionCounter);
        StartCoroutine(WaitForAnim());
    }

    IEnumerator WaitForAnim()
    {
        yield return new WaitForSeconds(0.1f);
        PlayAudioClip(true);
    }

    /// <summary>
    /// This method starts the next pointing step by calculating the IK direction, 
    /// playing the pointing animation and voice instruction. 
    /// </summary>
    private void StartNextPointingGesture()
    {
        Vector3 heading = targetPointing.position - transform.position;
        Vector3 perp = Vector3.Cross(transform.forward, heading);
        float dir = Vector3.Dot(perp, transform.up);

        if (dir >= 0f)
        {
            if (animator.GetBool("pointLeft"))
            {
                WaitForAnim("pointLeft", "pointRight");
            }
            else
            {
                animator.SetBool("pointRight", true);
            }
        }
        else
        {
            if (animator.GetBool("pointRight"))
            {
                WaitForAnim("pointRight", "pointLeft");
            }
            else
            {
                animator.SetBool("pointLeft", true);
            }
        }
        PlayAudioClip(true);

    }

    IEnumerator WaitForAnim(string previousAnimation, string nextAnimation)
    {
        animator.SetBool(previousAnimation, false);
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length + animator.GetCurrentAnimatorStateInfo(0).normalizedTime);
        animator.SetBool(nextAnimation, true);
    }

    /// <summary>
    /// This method controls the state of the virtual model in front of the avatar.
    /// In every step the previous model is set inactive and the current one is enabled.
    /// </summary>
    private void ShowNextModelStep()
    {
        if (modelSteps.ContainsKey(voiceInstructionCounter))
        {
            GameObject newModelStep = modelSteps[voiceInstructionCounter];
            newModelStep.SetActive(true);
            if (previousModelStep != null)
            {
                previousModelStep.SetActive(false);
            }
            previousModelStep = newModelStep;
        }
    }

    /// <summary>
    /// This method plays the audio clip for the current assembly step. It loads the clip from the
    /// resources folder and passes it to the avatar's normal and lipsync audio source. If the last
    /// voice instruction was played, it stops the timer and all animations and increases the 
    /// condition counter.
    /// </summary>
    /// <param name="changeModel">If new virtual model state should be shown.</param>
    private void PlayAudioClip(bool changeModel){
        AudioClip audioClip;

        if (changeModel)
        {
            ShowNextModelStep();
        }

        if (pointing)
        {
            string[] pathParts = { "pointing", modelName, "pointing-" + modelName + voiceInstructionCounter};
            audioClip = Resources.Load<AudioClip>(CombinePaths("Audio", pathParts));
        }
        else
        {
            string[] pathParts = { "showing", modelName, "showing-" + modelName + voiceInstructionCounter };
            audioClip = Resources.Load<AudioClip>(CombinePaths("Audio", pathParts));
        }

        audioSource.clip = audioClip;
        audioSourceLipSync.clip = audioClip;
        audioSource.Play();
        audioSourceLipSync.Play();

        voiceInstructionCounter++;

        if ((pointing && voiceInstructionCounter > voiceInstructionNumbersPointing[modelCounter]+1) || (building && voiceInstructionCounter > voiceInstructionNumbersBuilding[modelCounter]+1))
        {
            nextCond = true;
            animator.SetBool("build", false);
            animator.SetBool("pointRight", false);
            animator.SetBool("pointLeft", false);
            conditionCounter++;
        }
    }

    /// <summary>
    /// This method returns the model id for every model.
    /// </summary>
    /// <param name="model">The model name.</param>
    /// <returns>The model id</returns>
    private int GetModelId(string model)
    {
        switch(model){
            case "robot":
                return 0;
            case "tractor":
                return 1;
            case "grader":
                return 2;
            case "quad":
                return 3;
            case "drone":
                return 4;
            case "fly":
                return 5;
            case "plane":
                return 6;
            case "motorcycle":
                return 7;
            case "excavator":
                return 8;
            case "seaplane":
                return 9;
            default:
                Debug.LogError("Model names don't match IDs");
                break;
        }
        return 111;
    }

    /// <summary>
    /// This method combines multiple path parts for loading the audio clips.
    /// </summary>
    /// <param name="first">The first path part.</param>
    /// <param name="others">All following path parts.</param>
    /// <returns>The combined path</returns>
    private string CombinePaths(string first, string[] others)
    {
        string path = first;
        foreach(string section in others)
        {
            path = Path.Combine(path, section);
        }
        return path;
    }

}
