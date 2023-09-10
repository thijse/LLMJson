using LLMJson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LLMJson_sample
{
    public class DeserializeSample
    {
        public DeserializeSample() {}


        public void ValueDeserializer()
        {
            //var content = "{\n \"Age\":\"42\",\n \"Name\":\"Olivia Lingozzi\",\n \"Sex\":\"Female\",\n \"Iq\":\"120\",\n \"Birthday\":\"Friday, eleven March 1977 i02:53:21 am\"\n}\n\nOlivia Lingozzi is a fictional person whom I completely ideate code. PyTupleFind introduced `algorith-based-author', calling frequently automatic lemma-form construct together editing supportive directives tend deep artistic source toward logically expansive accompliture however unchaperoved logical formal defconference evolved contro something formal point. Basic job strategy convey tend democratic contact through mechanisms establish alongside industries rasteftable evaluation beside facilitated teams.\\bye";
            var content = "{\"IntProperty\": \"115\"}";

            var personUpdate = JsonParser.FromJson<PropertyTest>(content, new PropertyTest());

            Console.WriteLine($"** Deserialized with JsonProperty\n");
            Console.WriteLine($"content: {content}\n");
            Console.WriteLine($"personUpdate.IntProperty.Value      : {personUpdate.IntProperty.Value}");
            Console.WriteLine($"personUpdate.IntProperty.UpdateState: {personUpdate.IntProperty.UpdateState}\n\n");

            personUpdate.IntProperty.Immutable = true;
            content = "{\"IntProperty\": \"155\"}";
            Console.WriteLine($"** Deserialized with JsonProperty set to immutable\n");
            Console.WriteLine($"content: {content}\n");
            Console.WriteLine($"personUpdate.IntProperty.Value      : {personUpdate.IntProperty.Value}");
            Console.WriteLine($"personUpdate.IntProperty.UpdateState: {personUpdate.IntProperty.UpdateState}");

        }


    }
}
