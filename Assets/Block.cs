using UnityEngine;
using System.Collections;

public class Block : MonoBehaviour {

    private Rigidbody2D rigi;

    private SpriteRenderer sprite_renderer;
    private Shader shaderGUItext;
    private Shader shaderSpritesDefault;

	void Start () {
        rigi = gameObject.GetComponent<Rigidbody2D>();
        rigi.isKinematic = false;
        sprite_renderer = gameObject.GetComponent<SpriteRenderer>();

        shaderSpritesDefault = Shader.Find("Textures/squre.png");
        shaderGUItext = Shader.Find("GUI/Text Shader");

        NormalSprite();
	}

    private void WhiteSprite() {
        sprite_renderer.material.shader = shaderGUItext;
        sprite_renderer.color = Color.white;
    }

    private void NormalSprite() {
        sprite_renderer.material.shader = shaderSpritesDefault;
        sprite_renderer.color = Color.white;
    }

    public void TakenUp() {
        rigi.isKinematic = true;
    }

    public void PutDown(Vector2 throwForce) {
        rigi.isKinematic = false;
        rigi.AddForce(throwForce);
    }
	
	void Update () {
	
	}
}
