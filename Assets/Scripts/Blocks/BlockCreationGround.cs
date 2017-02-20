using UnityEngine;
using System.Collections;

public class BlockCreationGround : MonoBehaviour {

    public GameObject block_prefab;

    public Block GetBlock() {
        GameObject block = GameObject.Instantiate(block_prefab);
        return block.GetComponent<Block>();
    }
}
