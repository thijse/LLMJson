using LLMJson;

namespace LLMJson_sample
{

    public class SerializeSample
    {

        public SerializeSample() { }


        public void ValueSerializer()
        {
            var person     = new Person(); person.SetNigel();
            var personJson = JsonWriter.ToJson(
                person,
                OutputModes.Value
           );
            Console.WriteLine($"** Serialized object with values\n\n{personJson}\n\n");
        }

        public void ValueAndDescriptionSerializer()
        {
            var person = new Person(); person.SetNigel();
            var personJson = JsonWriter.ToJson(
                person,
                OutputModes.ValueAndDescription
           );
            Console.WriteLine($"** Serialized object with values and descriptions\n\n{personJson}\n\n");
        }

        public void DescriptionSerializer()
        {
            var person = new Person(); person.SetNigel();
            var personJson = JsonWriter.ToJson(
                person,
                OutputModes.Description
           );
            Console.WriteLine($"** Serialized object with descriptions\n\n{personJson}\n\n");
        }

        public void CustomSerializer()
        {
            var person = new Person(); person.SetNigel();
            var personJson = JsonWriter.ToJson(
                person,
                OutputModes.Custom,
                (value, type, description) => $"{value}{" \\\\ ".IfBothNotEmpty(description.IfBothNotEmpty(". ") + "The field is of type ".IfBothNotEmpty(type))}\n"
           );
            Console.WriteLine($"** Serialized object with custom serializer\n\n{personJson}\n\n");
        }



        public void CustomPropertyDisabledSerializer()
        {
            var person = new Person(); person.SetNigel();
            person.Iq.Visible = false; 

            var personJson = JsonWriter.ToJson(
                person,
                OutputModes.Custom,
                (value, type, description) => $"{value}{" \\\\ ".IfBothNotEmpty(description.IfBothNotEmpty(". ") + "The field is of type ".IfBothNotEmpty(type))}\n"
           );
            Console.WriteLine($"** Serialized object with custom property IQ disabled \n\n{personJson}\n\n");
        }


    }
}   
