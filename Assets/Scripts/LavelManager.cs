using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavelManager : MonoBehaviour {

	// Use this for initialization
	public static LavelManager sharedInstance;

	// creating a list for all the level blocks
	public List<LevelBlock> allTheLevelBlocks = new List<LevelBlock>(); 
		
	// creating a list for current level blocks
	public List<LevelBlock> currentLevelBlocks = new List<LevelBlock>(); 

	public Transform levelStartPosition;	

	void Awake() {

		if(sharedInstance == null){
			sharedInstance = this;
		}

	}

	void Start () {
		
		GenerateInitialBlocks();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void AddLevelBlock(){

		int randomIdx = Random.Range(0, allTheLevelBlocks.Count);
		
		LevelBlock block;

		Vector3 spawnPosition  = Vector3.zero; //set the position in 0

		if(currentLevelBlocks.Count == 0){
			block = Instantiate(allTheLevelBlocks[0]); // if we dont have blocks, we asign the firt block [0] 
		   //this is the basic block for all the levels
		   //we need to use the method Instantiate to return a copy for this object

		   spawnPosition = levelStartPosition.position;
		   //we asign the position block to the star position we added in unity environment
		}else{
			block = Instantiate(allTheLevelBlocks[randomIdx]); //else we add a random block with the random variable
			int index = currentLevelBlocks.Count - 1;
			spawnPosition = currentLevelBlocks[index].endPoint.position; 
			// in this case we assign the position of the block in the endpoint of the previous block we added in the current blocks array
		}

		block.transform.SetParent(this.transform, false); // we specify all the blocks will be child of the LevelManager

		Vector3 correction = new Vector3(
			spawnPosition.x - block.startPoint.position.x,
			spawnPosition.y - block.startPoint.position.y,
			0
		); // match the exit point of one block with the start point of the current block and asign the position

		block.transform.position = correction;

		currentLevelBlocks.Add(block); // add the new block to the current blocks array
	}

	public void RemoveLevelBlock(){
		LevelBlock oldBlock = currentLevelBlocks[0];
		currentLevelBlocks.Remove(oldBlock);
		Destroy(oldBlock.gameObject);
	}

	public void RemoveAllLevelBlokcs(){

		while(currentLevelBlocks.Count > 0){
			RemoveLevelBlock();
		}
	}

	public void GenerateInitialBlocks(){

		for (int i = 0; i < 2; i++)
		{
			AddLevelBlock();	
		}
	}
}
