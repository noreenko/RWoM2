using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace RimWorldOfMagic.UI;

// ReSharper disable once InconsistentNaming
public static class ITabUtils
{
    private static Dictionary<string, string> textBuffers = new Dictionary<string, string>();
    
    
    public static void DrawSubTabHeader()
    {
        
    }
    
    /// <summary>
    /// Creates a float setting UI with label on first line and slider + text input on second line
    /// </summary>
    /// <param name="rect">The rect to draw within</param>
    /// <param name="value">Reference to the float setting value</param>
    /// <param name="translationKey">Translation key for the label</param>
    /// <param name="min">Minimum slider value</param>
    /// <param name="max">Maximum slider value</param>
    /// <param name="bufferId">Unique ID for the text buffer (use setting name)</param>
    /// <param name="formatString">Format string for displaying the number (e.g., "F1", "F0")</param>
    /// <returns>The height used (2 * Text.LineHeight)</returns>
    public static float DrawFloatSetting(Rect rect, ref float value, string translationKey, float min, float max, string bufferId, string formatString = "F1")
    {
        // Initialize buffer if it doesn't exist
        if (!textBuffers.ContainsKey(bufferId))
        {
            textBuffers[bufferId] = value.ToString(formatString);
        }
        
        float lineHeight = Text.LineHeight;
        
        // Label on first line
        Rect labelRect = new Rect(rect.x, rect.y, rect.width, lineHeight);
        Widgets.Label(labelRect, translationKey.Translate());
        
        // Slider and text field on second line
        Rect controlsRect = new Rect(rect.x, rect.y + lineHeight, rect.width, lineHeight);
        float textFieldWidth = Text.CalcSize("99999").x + 10f; // Width for 5 digits plus padding
        Rect textFieldRect = new Rect(controlsRect.xMax - textFieldWidth, controlsRect.y, textFieldWidth, controlsRect.height);
        Rect sliderRect = new Rect(controlsRect.x, controlsRect.y, controlsRect.width - textFieldWidth - 10f, controlsRect.height);
        
        // Slider
        float newValue = Widgets.HorizontalSlider(sliderRect, value, min, max, true);
        
        // Update buffer when slider changes
        if (!Mathf.Approximately(newValue, value))
        {
            value = newValue;
            textBuffers[bufferId] = newValue.ToString(formatString);
            RimWorldOfMagicSettings.settingsChanged = true;
        }
        
        // Text input field
        string newBuffer = Widgets.TextField(textFieldRect, textBuffers[bufferId]);
        if (newBuffer != textBuffers[bufferId])
        {
            textBuffers[bufferId] = newBuffer;
            if (float.TryParse(textBuffers[bufferId], out float parsedValue))
            {
                value = Mathf.Clamp(parsedValue, min, max);
            }

            RimWorldOfMagicSettings.settingsChanged = true;
        }
        
        return lineHeight * 2f; // Return height used
    }
    
    /// <summary>
    /// Creates an int setting UI with label on first line and slider + text input on second line
    /// </summary>
    /// <param name="rect">The rect to draw within</param>
    /// <param name="value">Reference to the int setting value</param>
    /// <param name="translationKey">Translation key for the label</param>
    /// <param name="min">Minimum slider value</param>
    /// <param name="max">Maximum slider value</param>
    /// <param name="bufferId">Unique ID for the text buffer (use setting name)</param>
    /// <returns>The height used (2 * Text.LineHeight)</returns>
    public static float DrawIntSetting(Rect rect, ref int value, string translationKey, int min, int max, string bufferId)
    {
        float floatValue = value;
        float result = DrawFloatSetting(rect, ref floatValue, translationKey, min, max, bufferId, "F0");
        value = (int)floatValue;
        return result;
    }
}