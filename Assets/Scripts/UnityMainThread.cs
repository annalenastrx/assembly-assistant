using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityMainThread : MonoBehaviour {

	internal static UnityMainThread wkr;
	Queue<Action> jobs = new Queue<Action>();

	// Use this for initialization
	void Awake () {
		wkr = this;
	}
	
	// Update is called once per frame
	void Update () {
		while(jobs.Count > 0){
			jobs.Dequeue().Invoke();
		}
	}

	internal void AddJob(Action newJob){
		jobs.Enqueue(newJob);
	} 
}
