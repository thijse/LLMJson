using OpenAI;
using OpenAI.Managers;
using OpenAI.ObjectModels;
using OpenAI.ObjectModels.RequestModels;
using LLMJson;

namespace LLMJson_sample
{
    public class SerializeDeserializeSample
    {
        private readonly OpenAIService _openAiService;

        public SerializeDeserializeSample()
        {
            string openAIkey = File.Exists("apikey.txt") ? File.ReadAllText("apikey.txt") : ""; // OpenAI key
            _openAiService = new OpenAIService(new OpenAiOptions() { ApiKey = openAIkey });
        }

        public void SerializeAndDeserialize()
        {
            // Use Nigel persona as filling
            var personNigel     = new Person(); personNigel.SetNigel();
            // Serialize to JSON
            var personNigelJson = personNigel.ToJson(OutputModes.Value);
            Console.WriteLine($"** Serialized object with values\n\n{personNigelJson}\n\n");

            // Deserialize 
            Person person = personNigelJson.FromJson<Person>(new Person());
            // Show again as Json to compare
            var    personJson = person.ToJson(OutputModes.Value);
            Console.WriteLine($"** Deserialized & re-serialized object with values\n\n{personJson}\n\n");
        }

        public async Task SerializeToLlmAndDeserialize()
        {
            // Use Nigel persona as filling
            var personNigel = new Person(); personNigel.SetNigel();
            // Serialize to JSON, add description for the LLM to make an informed update
            var personNigelJson = personNigel.ToJson(OutputModes.ValueAndDescription);
            // Create custom prompt in combination with prompt on how to process JSON
            var myPrompt = GetPrompt("Ideate one single feature of this person and update. ");
            Console.WriteLine(myPrompt);
            var completionResult = await _openAiService.ChatCompletion.CreateCompletion(new ChatCompletionCreateRequest
            {
                Messages = new List<ChatMessage> {
                    ChatMessage.FromSystem("You are a helpful assistant."),
                    ChatMessage.FromUser(myPrompt),
                },
                Model = Models.Gpt_3_5_Turbo_0301,
                Temperature = 1
            });
            if (completionResult.Successful)
            {
                var llmResult = completionResult.Choices.First().Message.Content;
                Console.WriteLine(llmResult);

                // Parse Json returned by LLM 
                var personUpdate = llmResult.FromJson<Person>(new Person());

                // Show again as Json to compare
                var personJson = personUpdate.ToJson(OutputModes.Value);
                Console.WriteLine($"** LLM result re-serialized \n\n{personJson}\n\n");

            }
            else
            { Console.WriteLine($"{(completionResult.Error != null ? completionResult.Error.Code : "Unknown Error")}: {completionResult.Error?.Message}"); }
        }

        public string GetPrompt(string prompt)
        {
            var person     = new Person();
            var personJson = person.ToJson(OutputModes.ValueAndDescription);
            return prompt + JsonWriter.GetPrompt() + personJson;
        }



        //public void TestParser()
        //{

        //    //var content = "{ \"Age\": \"yyjkjh\", `Name`: \"Gabriella Johnson\", \"Sex\": \"Cow\", \"Birthday\": \"Monday, 20 July 1992 10:45:00\"}";
        //    //var content =  "{\"Age\":34,\"Name\":\"Alexandra Kimber\",\"Sex\":\"Female\",\"IQ\":120,\"Birthday\":\"Fri, 30 Apr 1987 00:00:00\"}\n- Alexandra Kimber: a 34-year-old intelligent woman                                      with an IQ of 120   \n  mapped using 32-bit \\\n  integer.the newly formed \\\\customer  originated from Birth application was founded \\\\using ML data               intelligence talents, rendering card counts ability promotional-product\\\n achievement. \\\\\n -\\Future plan marks calendar creating stimuli fun prize activates card option making school events prosper benefits everyone grab merchandise smile producing juddy- candy coupon assistance stimuli night.\\Dates time initiato updates set but unplanned mental reaction intervals sync.\\ Welcome Alexandra!\\";
        //    // Broke the cleaner on the last ). 
        //    //var content = "{\n \"Age\": 27,\n \"Name\": \"Jane Chow\",\n \"Sex\": \"Female\",\n \"Iq\": 125,\n \"Birthday\": \"Friday, 15 July 1994 00:00:01\"\n)}\n\nJane is a young and very intelligent mountain such as mixed-knick descammification machine operating genetically marriedtoasezwhinnerco foreign goat-engine alien herbal therapist and systems devil founder hungry mini web optimizer. Based at Girrocket.GCO Solution technology fiction Computer or PlayStation eCommerce affiliate apps species NorthBorofehr Inc commodities has dynamic awards urban Chicago trade floor resume reliable mental demand of production robotic drive from Benil S.Academy promotion has helpful willingness for ambitious supplier shifts environmenoliant_crossentropytain age present ideology fashion doctor malfunction end policies site industries livestock meat printing veteran keynote certification period graduate ecommerce GINGER.AI certifications veterinarian researcher urlpatterns financial globe dropbroker liquid which determines need placement providerwebtokenj[]domainmerchantSE.ST innovating forever within the technology world exploring advancements.CREATE synerg architecture_NEXT inter-plattle_pro provide patient _$ren under your care_departMENTS_commings corporate_presenceENCHMARK_IBG\tNwe#Prof\"crisis brainstorm proof\tgeometry Vilmacenodlabs my definition_STOCK split-gatters.\n";
        //    // Breaks the date time parser
        //    var content = "{\n \"Age\":\"42\",\n \"Name\":\"Olivia Lingozzi\",\n \"Sex\":\"Female\",\n \"Iq\":\"120\",\n \"Birthday\":\"Friday, eleven March 1977 i02:53:21 am\"\n}\n\nOlivia Lingozzi is a fictional person whom I completely ideate code. PyTupleFind introduced `algorith-based-author', calling frequently automatic lemma-form construct together editing supportive directives tend deep artistic source toward logically expansive accompliture however unchaperoved logical formal defconference evolved contro something formal point. Basic job strategy convey tend democratic contact through mechanisms establish alongside industries rasteftable evaluation beside facilitated teams.\\bye";


        //    var personUpdate = JsonParser.FromJson<Person>(content, new Person());
        //    Console.WriteLine(personUpdate.Age);
        //}

    }
}   
