using UnityEngine;
using System.Collections;

public class LeverPole : MonoBehaviour {

    public Vector3 orig_pos;
    public Vector3 OrigPos { get { return orig_pos; } }

    private Vector3 orig_scale;

    private float scale_factor = 4f;

    private bool scaling_mode = false;

    private float current_scale_factor;
    private float current_offset;

    public float y_shift;

    private SpriteRenderer renderer;
    private Texture2D tex;
    private BoxCollider2D coll;

	void Start () {
        orig_pos = gameObject.transform.localPosition;
        orig_scale = transform.localScale;
        current_scale_factor = 1;

        renderer = GetComponent<SpriteRenderer>();
        tex = renderer.sprite.texture;

        coll = GetComponent<BoxCollider2D>();

        // MakePartTransparent(tex.height * (2 - current_offset));
    }

    private void MakePartTransparent(float y_range, float y_start=0) {

        Texture2D newTex = new Texture2D(tex.width, tex.height, TextureFormat.ARGB32, false);
        newTex.SetPixels32(tex.GetPixels32());

        float width = newTex.width;
        float height = newTex.height;

        for (int y = 0; y <= y_range; y++) {
            for (int x = 0; x < width; x++) {
                newTex.SetPixel(x, y, new Color(1, 1, 1, 0));
            }
        }
        newTex.Apply();
        renderer.sprite = Sprite.Create(newTex, renderer.sprite.rect, new Vector2(0.5f, 0.5f));
    }

    void Update() {

        if (scaling_mode) {
            transform.localScale = new Vector2(orig_scale.x, current_scale_factor);
            y_shift = (transform.localScale.y - orig_scale.y) / (scale_factor - 1);
            transform.localPosition = orig_pos + new Vector3(0, y_shift);
        }
        else {
            transform.localPosition = orig_pos + new Vector3(0, current_offset);
        }

        // coll.size = new Vector2();
        coll.transform.localPosition = orig_pos + new Vector3(0, current_offset);
    }

    public void AssignCurrentScale(float current_scale) {
        current_scale_factor = current_scale * orig_scale.y;
    }

    public void AssignOffset(float offset) {
        current_offset = offset;
    }
}
