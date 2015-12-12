using UnityEngine;

using System.Collections.Generic;

public static class Helper {
    //NOTE: Adapted from http://www.emoticode.net/c-sharp/hex-color-to-rgb-color.html
    public static Color HexToColor(string hexColor) {
        if (hexColor.IndexOf('#') != -1) {
            hexColor = hexColor.Replace("#", "");
        }

        float red = 0;
        float green = 0;
        float blue = 0;

        if (hexColor.Length == 6) {
            red = (float) (int.Parse(hexColor.Substring(0, 2), System.Globalization.NumberStyles.AllowHexSpecifier)) / 255;
            green = (float) (int.Parse(hexColor.Substring(2, 2), System.Globalization.NumberStyles.AllowHexSpecifier)) / 255;
            blue = (float) (int.Parse(hexColor.Substring(4, 2), System.Globalization.NumberStyles.AllowHexSpecifier)) / 255;
        }
        else if (hexColor.Length == 3) {
            red = (float) (int.Parse(hexColor[0].ToString() + hexColor[0].ToString(), System.Globalization.NumberStyles.AllowHexSpecifier)) / 255;
            green = (float) (int.Parse(hexColor[1].ToString() + hexColor[1].ToString(), System.Globalization.NumberStyles.AllowHexSpecifier)) / 255;
            blue = (float) (int.Parse(hexColor[2].ToString() + hexColor[2].ToString(), System.Globalization.NumberStyles.AllowHexSpecifier)) / 255;
        }

        return new Color(red, green, blue);
    }

    private static string GetHex(int dec) {
        string alpha = "0123456789ABCDEF";
        if (dec < 0) return "0";
        if (dec >= alpha.Length) return "F";

        return "" + alpha[dec];
    }

    public static string ColorToHex(Color color) {
        float red = color.r * 255;
        float green = color.g * 255;
        float blue = color.b * 255;

        string a = GetHex(Mathf.FloorToInt(red / 16));
        string b = GetHex(Mathf.RoundToInt(red % 16));
        string c = GetHex(Mathf.FloorToInt(green / 16));
        string d = GetHex(Mathf.RoundToInt(green % 16));
        string e = GetHex(Mathf.FloorToInt(blue / 16));
        string f = GetHex(Mathf.RoundToInt(blue % 16));

        string z = a + b + c + d + e + f;

        return z;
    }
}
