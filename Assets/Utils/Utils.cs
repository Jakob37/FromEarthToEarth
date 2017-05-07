using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Assets.Utils {
    public static class Utils {

        public static Vector2 GetSpriteSizeInPixels(SpriteRenderer sprite_renderer) {
            Vector2 sprite_size = sprite_renderer.sprite.rect.size;
            Vector2 sprite_size_pixels = sprite_size / sprite_renderer.sprite.pixelsPerUnit;
            return sprite_size_pixels;
        }

        public static List<string[]> ParseTextToSplitList(string resource_name, string splitter, 
            int expected_length=2, bool enforce_expected_length=false) {

            string[] lines = ParseTextToArray(resource_name);
            List<string[]> split_entries = new List<string[]>();

            for (int i = 0; i < lines.Length; i++) {

                string line = lines[i];
                string[] values = Regex.Split(line, splitter);

                if (enforce_expected_length && values.Length != expected_length) {
                    throw new ArgumentException("Expected length " + expected_length + ", received: " + values);
                }

                split_entries.Add(values);
            }
            return split_entries;
        }

        public static T ParseEnum<T>(string value) {
            return (T)Enum.Parse(typeof(T), value, true);
        }

        public static string[] ParseTextToArray(string resource_name) {

            TextAsset board_text = (TextAsset)Resources.Load(resource_name);
            var splitFile = new string[] { "\r\n", "\r", "\n" };
            string[] lines = board_text.text.Split(splitFile, StringSplitOptions.None);
            return lines;
        }
    }
}
