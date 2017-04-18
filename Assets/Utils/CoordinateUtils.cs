using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Utils {
    public static class CoordinateUtils {

        public static Vector2 GetSpriteSizeInPixels(SpriteRenderer sprite_renderer) {
            Vector2 sprite_size = sprite_renderer.sprite.rect.size;
            Vector2 sprite_size_pixels = sprite_size / sprite_renderer.sprite.pixelsPerUnit;
            return sprite_size_pixels;
        }


    }
}
