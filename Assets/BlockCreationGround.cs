using UnityEngine;
using System.Collections;

public class BlockCreationGround : MonoBehaviour {

    public GameObject block_prefab;

    private int available_blocks = 4;

    public bool HasAvailableBlocks() {
        return available_blocks > 0;
    }

    public Block GetBlock() {
        if (available_blocks <= 0) {
            throw new System.ArgumentException("Attempted retrieving block, but there is no block available");
        }

        available_blocks -= 1;
        GameObject block = GameObject.Instantiate(block_prefab);
        return block.GetComponent<Block>();
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other) {

        print("Triggered ground!");
    }

    void OnTriggerExit(Collider other) {

        print("Untriggered ground!");
    }
}
