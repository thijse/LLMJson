using OpenAI;
using OpenAI.Managers;
using OpenAI.ObjectModels;
using OpenAI.ObjectModels.RequestModels;
using LLMJson;

namespace LLMJson_sample
{


    public class SerializeSample
    {

        public SerializeSample() { }


        public string GetPrompt(string prompt)
        {
            var person     = new Person();
            var personJson = person.ToJson(OutputModes.ValueAndDescription);
            return prompt + JsonWriter.GetPrompt() + personJson;
        }



        public void ValueSerializer()
        {
            var Jan     = new Person();
            var janJson = JsonWriter.ToJson(
                Jan,
                OutputModes.Value
           );
            Console.WriteLine($"** Serialized object with values\n\n{janJson}\n\n");
        }

        public void ValueAndDescriptionSerializer()
        {
            var Jan = new Person();
            var janJson = JsonWriter.ToJson(
                Jan,
                OutputModes.ValueAndDescription
           );
            Console.WriteLine($"** Serialized object with values and descriptions\n\n{janJson}\n\n");
        }

        public void DescriptionSerializer()
        {
            var Jan = new Person();
            var janJson = JsonWriter.ToJson(
                Jan,
                OutputModes.Description
           );
            Console.WriteLine($"** Serialized object with descriptions\n\n{janJson}\n\n");
        }

        public void CustomSerializer()
        {
            var Jan = new Person();
            var janJson = JsonWriter.ToJson(
                Jan,
                OutputModes.Custom,
                (value, type, description) => $"{value}{" \\\\ ".IfBothNotEmpty(description.IfBothNotEmpty(". ") + "The field is of type ".IfBothNotEmpty(type))}\n"
           );
            Console.WriteLine($"** Serialized object with custom serializer\n\n{janJson}\n\n");
        }



        public void CustomPropertyDisabledSerializer()
        {
            var Jan       = new Person();
            Jan.Iq.Visible = false; 

            var janJson = JsonWriter.ToJson(
                Jan,
                OutputModes.Custom,
                (value, type, description) => $"{value}{" \\\\ ".IfBothNotEmpty(description.IfBothNotEmpty(". ") + "The field is of type ".IfBothNotEmpty(type))}\n"
           );
            Console.WriteLine($"** Serialized object with custom property IQ disabled \n\n{janJson}\n\n");
        }


    }
}   
