using Microsoft.Recognizers.Text.Number;
using Microsoft.Recognizers.Text;
using Microsoft.Recognizers.Text.NumberWithUnit;

namespace LLMJson;

public class PercentageProp : JsonProp<float>
{
    public PercentageProp(float value, bool visible = true, bool immutable = false) :
        base(value, "percentage between 0 and 100", visible, immutable,
            s =>
            {
                var value = SafeParseUtils.PrepString(s).Trim('%');
                // First try to directly interpret as number
                var success = float.TryParse(value, out float floatValue);
                if (success) return new Tuple<float, bool>(floatValue,true);

                // Not possible,? try a Microsoft Recognizer
                value  = SafeParseUtils.RecognizerResultToValue(NumberRecognizer.RecognizePercentage(s, Culture.English))?.Trim('%');
                success = float.TryParse(value, out floatValue);
                return new Tuple<float, bool>(floatValue, success);
            })
    {}
}

public class TemperatureProp : JsonProp<float>
{
    public TemperatureProp(float value, bool visible = true, bool immutable = false) :
        base(value, "Temperature in Celcius", visible, immutable,
            s =>
            {
                var value = SafeParseUtils.PrepString(s).Trim('C');
                // First try to directly interpret as number
                var success = float.TryParse(value, out float floatValue);
                if (success) return new Tuple<float, bool>(floatValue, true);

                // Not possible? try a Microsoft Recognizer
                value = SafeParseUtils.RecognizerResultToValue(NumberWithUnitRecognizer.RecognizeTemperature(s, Culture.English))?.Trim('C');
                // to do: check for Fahrenheit/ Kelvin, convert to C
                success = float.TryParse(value, out floatValue);
                return new Tuple<float, bool>(floatValue, success);
            })
    { }
}