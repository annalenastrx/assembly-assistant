using UnityEngine;
using System;
using System.Collections;

[RequireComponent(typeof(Animator))] 

public class IKControl : MonoBehaviour {
    
    public Animator animator;

	public bool ikActiveRight = false;
    public bool ikActiveLeft = false;

	public Transform rightWrist;
	public Transform leftWrist;
    public Transform rightIndex;
    public Transform leftIndex;
    public Transform pointingTarget = null;

	public float weightRight = 0.0f;
	public float weightLeft = 0.0f;

    void Start () 
    {
        animator = GetComponent<Animator>();
    }

    void LateUpdate(){

        if(ikActiveRight)
        {
            // Rotate right index finger towards target
            Vector3 directionR = 2* (pointingTarget.position - rightIndex.position).normalized;
            rightIndex.rotation = Quaternion.LookRotation(directionR);
		    rightIndex.Rotate(90,90,90);
        }
        else if(ikActiveLeft)
        {
            // Rotate left index finger towards target
            Vector3 directionL = 2* (pointingTarget.position - leftIndex.position).normalized;
            leftIndex.rotation = Quaternion.LookRotation(directionL);
		    leftIndex.Rotate(90,90,90);
        }
    }

	void OnAnimatorIK()
    {
        if(animator) 
        {
            // If IK is active, set position and rotation directly to the goal
            if(ikActiveRight) 
            {
				if(weightRight < 1.0f)
                {
					weightRight += 0.05f;
				}

                // Set right hand target position and rotation, if one has been assigned
                if(pointingTarget != null) 
                {
					animator.SetLookAtWeight(weightRight*2f);
                    animator.SetLookAtPosition(pointingTarget.position);

					// Get vector pointing to opposite direction of wrist
					Vector3 direction = 2* (pointingTarget.position - rightWrist.position).normalized;
 
         			// Create rotation to look at the target
         			pointingTarget.rotation = Quaternion.LookRotation(direction);
					pointingTarget.Rotate(0,0,-90f);

                    animator.SetIKPositionWeight(AvatarIKGoal.RightHand,weightRight);
                    animator.SetIKRotationWeight(AvatarIKGoal.RightHand,weightRight);  
                    animator.SetIKPosition(AvatarIKGoal.RightHand,pointingTarget.position);
                    animator.SetIKRotation(AvatarIKGoal.RightHand,pointingTarget.rotation);
                }        
                
            }
            
            // If IK is not active, set position and rotation of hand back to the original position
            else 
            {          
                animator.SetIKPositionWeight(AvatarIKGoal.RightHand,0);
                animator.SetIKRotationWeight(AvatarIKGoal.RightHand,0); 

				weightRight = 0;
            }

			if(ikActiveLeft) 
            {

				if(weightLeft < 1.0f)
                {
					weightLeft += 0.05f;
				}   

                // Set left hand target position and rotation, if one has been assigned
                if(pointingTarget != null) {
                    animator.SetLookAtWeight(weightLeft*2f);
                    animator.SetLookAtPosition(pointingTarget.position);

					// Get vector pointing to opposite direction of wrist
					Vector3 direction = 2* (pointingTarget.position - leftWrist.position).normalized;
 
         			// Create the rotation we need to be in to look at the target
         			pointingTarget.rotation = Quaternion.LookRotation(direction);
					pointingTarget.Rotate(0,0,90f);

                    animator.SetIKPositionWeight(AvatarIKGoal.LeftHand,weightLeft);
                    animator.SetIKRotationWeight(AvatarIKGoal.LeftHand,weightLeft);  

                    animator.SetIKPosition(AvatarIKGoal.LeftHand,pointingTarget.position);
                    animator.SetIKRotation(AvatarIKGoal.LeftHand,pointingTarget.rotation);
                }        
                
            }
            
            // If IK is not active, set position and rotation of the hand back to original position
            else {          
                animator.SetIKPositionWeight(AvatarIKGoal.LeftHand,0);
                animator.SetIKRotationWeight(AvatarIKGoal.LeftHand,0); 

				weightLeft = 0;
            }
            if(!ikActiveLeft && !ikActiveRight)
            {
                animator.SetLookAtWeight(0);
            }
        }
    }

	public void SetIkActiveRight(){
		ikActiveRight = true;
	}

	public void ResetIkActiveRight(){
		ikActiveRight = false;
	}

	public void SetIkActiveLeft(){
		ikActiveLeft = true;
	}

	public void ResetIkActiveLeft(){
		ikActiveLeft = false;
	}
}
