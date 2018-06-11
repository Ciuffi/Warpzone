using UnityEngine;

    public class RainbowColors {
        private static Color[] colors =
            {Color.red, Color.cyan, Color.green, Color.yellow, Color.magenta};
        private static int _colornum = 0;
        public static Color PickNextColor() {
            _colornum++;
            if (_colornum > colors.Length) _colornum = 0;
            return colors[_colornum];

        }
    }