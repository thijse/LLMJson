using LLMJson;

namespace LLMJson_sample
{
    public class DeserializeSample
    {

        public class PropertyTest
        {
            public int           AgeInt    { get; set; } = 0;
            public JsonProp<int> IQIntProp { get; set; } = new(0, "Intelligence coefficient");
        }

        public void ValueDeserializer()
        {
            var contentInstance = new PropertyTest();

            var content = "{\"AgeInt\": \"21\",\n \"IQIntProp\":\"125\"}";
            contentInstance = content.FromJson<PropertyTest>(contentInstance);

            Console.WriteLine($"** Deserialized with JsonProperty\n");
            Console.WriteLine($"content: {content}\n");
            Console.WriteLine($"personUpdate.IQIntProp.Value      : {contentInstance.IQIntProp.Value}");
            Console.WriteLine($"personUpdate.IQIntProp.UpdateState: {contentInstance.IQIntProp.UpdateState}\n\n");


            content = "{\"AgeInt\": \"21\",\n \"IQIntProp\":\"135\"}";
            contentInstance.IQIntProp.Immutable = true;
            contentInstance = content.FromJson<PropertyTest>(contentInstance);
            Console.WriteLine($"** Deserialized with JsonProperty set to immutable\n");
            Console.WriteLine($"content: {content}\n");
            Console.WriteLine($"personUpdate.IQIntProp.Value      : {contentInstance.IQIntProp.Value}");
            Console.WriteLine($"personUpdate.IQIntProp.UpdateState: {contentInstance.IQIntProp.UpdateState}");

        }


        public class MalformattedFields
        {
            public int                 OrdinalNumber { get; set; } = 0;
            public float               FloatNumber   { get; set; } = 0.0f;
            public JsonProp<DateTime>  DateTimeProp  { get; set; } = new(default, "Date time");
        }

        public void MalformattedFieldsDeserializer()
        {
            // Use Microsoft recognizers. These recognize all kinds of variations of floats, dates etc, but are very slow
            JsonParser.UseRecognizer = true;

            var contentInstance = new MalformattedFields();

            var content = "{\"OrdinalNumber\": \"eleventh\",\n \"FloatNumber\":\"eight point six\",\n \"DateTimeProp\":\"8:00pm 5 jan 2021\" }";
            contentInstance = content.FromJson<MalformattedFields>(contentInstance);

            Console.WriteLine($"** Deserialized with JsonProperty\n");
            Console.WriteLine($"content: {content}\n");
            Console.WriteLine($"personUpdate.OrdinalNumber      : {contentInstance.OrdinalNumber}");
            Console.WriteLine($"personUpdate.FloatNumber        : {contentInstance.FloatNumber}");
            Console.WriteLine($"personUpdate.DateTimeProp       : {contentInstance.DateTimeProp}");
            
        }



        public class CustomProps
        {
            public PercentageProp  Percentage1  { get; set; } = new(10.0f,true);
            public PercentageProp  Percentage2  { get; set; } = new(20.0f,true);
            public TemperatureProp Temperature1 { get; set; } = new(20.0f, true);
            public TemperatureProp Temperature2 { get; set; } = new(20.0f, true);
        }

        public void CustomPropsDeserializer()
        {

            var contentInstance = new CustomProps();

            var content = "{\"Percentage1\": \"fifty eight percents\",\n \"Percentage2\":\"67 %\" ,\n \"Temperature1\":\"99 C\" ,\n \"Temperature2\":\"It is 88 degrees Celsius\"}";
            contentInstance = content.FromJson<CustomProps>(contentInstance);

            Console.WriteLine($"** Deserialized with custom JsonProperty\n");
            Console.WriteLine($"content: {content}\n");
            Console.WriteLine($"contentInstance.Percentage1      : {contentInstance.Percentage1}");
            Console.WriteLine($"contentInstance.Percentage2      : {contentInstance.Percentage2}");
            Console.WriteLine($"contentInstance.Temperature1      : {contentInstance.Temperature1}");
            Console.WriteLine($"contentInstance.Temperature2      : {contentInstance.Temperature2}");
        }

    }
}
