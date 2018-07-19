using UnityEngine;
using System.Collections;

public class BigFoot : MonoBehaviour {

	UnityEngine.AI.NavMeshAgent agent;
	Animator BigFootAnimator;

	float Health, WalkTime , IdleTime, CampTime, feedTime;
	bool idle, walk, attack, hit, goCamp , feed;
	int WalkCount, IdleCount , CampCount , FeedCount;
	Vector3 WanderTarget, AttackTarget , PlayerTarget; 


	public Transform PlayerTrans;


	void OnEnable()
	{
		
		gameObject.GetComponent<Transform> ().position = SetSpawn(); // Set Position of BigFoot Spawn
		BigFootAnimator =  gameObject.GetComponent<Animator> ();
		agent = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent> ();

		Health = 100;
		WalkTime = IdleTime = CampTime = feedTime = 0;
		WalkCount = IdleCount = CampCount = FeedCount = 0;
		walk = attack = hit = goCamp = feed = false;

		idle = true;
		IdleCount = 1;

		//BigFootAnimator.SetBool ();
	}

	// Use this for initialization
	void Start () 
	{
			
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (idle)
		{
			IdleTime = IdleTime + (Time.deltaTime);
			Idle ();
		} 
		else if (walk) {
			WalkTime = WalkTime + Time.deltaTime;
			Wander ();	
		}
		else if (attack) {
			
		}
		else if (goCamp) {
			
		}
		else if (feed) {
		}
	
	}


	void FixedUpdate()
	{
		
	}
	void Idle()
	{
		if (IdleTime > 6.2f ) {		// Idle to Walk
			BigFootAnimator.SetBool ("Idle", false);
			BigFootAnimator.SetBool ("Walk", true);
			idle = false;
			walk = true;
			IdleTime = 0f;
			StartCoroutine(WaitFun(5));
			SelectWanderTarget ();
			agent.SetDestination (WanderTarget);
			agent.isStopped = false;


		}
		if(Vector3.Distance (gameObject.transform.position ,PlayerTrans.position) < 100) // walk to attack
		{
			Attack ();
		}

		
	}
	void Wander ()
	{
		if (Vector3.Distance(WanderTarget,gameObject.transform.position) < 15) { // walk to idle
			walk = false;
			idle = true;
			agent.isStopped = true;
			BigFootAnimator.SetBool ("Idle", true);
			BigFootAnimator.SetBool ("Walk", false);
		}
		else if(Vector3.Distance (gameObject.transform.position ,PlayerTrans.position) < 100) // walk to attack
		{
			Attack ();
		}
	}

	void Attack()
	{
		
	}

	void ListenFire()
	{
	}

	void SelectWanderTarget()
	{
		int toss = Random.Range (1, 4); 
		int x = 0, z=0;
		if (toss== 1) 
		{
			x = Random.Range (50, 150);
			z =  Random.Range (50, 150);

		}
		else if (toss== 2) 
		{
			x = Random.Range (50, 150);
			z =  Random.Range (350, 450);

		}
		else if (toss== 3) 
		{
			x = Random.Range (350, 450);
			z =  Random.Range (350, 450);

		}
		else if (toss== 4) 
		{
			x = Random.Range (350, 450);
			z =  Random.Range (50, 150);

		}
		WanderTarget = new Vector3 (x, 0f, z);

	}


	Vector3 SetSpawn()
	{
		int x=0, z=0;
		int toss = Random.Range (1, 4); 
		if (toss == 1) 
		{
			x = Random.Range (30, 60);
			z = Random.Range (50, 450);
		}
		else if (toss == 2) {
			z = Random.Range (30, 60);
			x = Random.Range (50, 450);
		}
		else if (toss == 3) {
			x = Random.Range (430, 460);
			z = Random.Range (50, 450);
		}	
		else if (toss == 4) {
			z = Random.Range (430, 460);
			x = Random.Range (50, 450);
		}
		Vector3 Pos = new Vector3 (x,0f,z);
		return Pos;
	}

	IEnumerator WaitFun(int x)
	{
		yield return new WaitForSeconds(x);
	}
}
