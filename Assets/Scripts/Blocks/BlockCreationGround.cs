using UnityEngine;
using System.Collections;

public class BlockCreationGround : MonoBehaviour {

    public GameObject block_prefab;

    public Block GetBlock() {
        GameObject block = GameObject.Instantiate(block_prefab);
        Block block_script = block.GetComponent<Block>();
        block_script.Initialize();
        return block_script;
    }
}
