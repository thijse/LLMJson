using static LLMJson_sample.DeserializeSample;

namespace LLMJson_sample
{
    internal class Program
    {


        static async Task Main(string[] args)
        {
            
            //var deserializeSample = new DeserializeSample();
            //deserializeSample.ValueDeserializer();
            //deserializeSample.MalformattedFieldsDeserializer();
            //deserializeSample.CustomPropsDeserializer();
            

            
            //var serializeSample = new SerializeSample();
            //serializeSample.ValueSerializer();
            //serializeSample.DescriptionSerializer();
            //serializeSample.ValueAndDescriptionSerializer();
            //serializeSample.CustomSerializer();
            //serializeSample.CustomPropertyDisabledSerializer();
            

            var jsonSendReceive = new SerializeDeserializeSample();

            //jsonSendReceive.SerializeAndDeserialize();
            await jsonSendReceive.SerializeToLlmAndDeserialize();
            //await jsonSendReceive.SendJson();
            //jsonSendReceive.TestParser();
            //jsonSendReceive.TestWriter();
            //jsonSendReceive.TestProperty();

            //jsonSendReceive.ObjectToJsonDescription();
            Console.WriteLine("Tests done!");
        }
    }
}