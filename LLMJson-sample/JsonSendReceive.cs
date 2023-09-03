using OpenAI;
using OpenAI.Managers;
using OpenAI.ObjectModels;
using OpenAI.ObjectModels.RequestModels;
using LLMJson;

namespace LLMJson_sample
{
    public enum Sex { Male, Female, Other}

    public class Person
    {
        [DescriptionAttribute("Firstname and last name")]
        public string Name                                                { get; set; } = "Jan B Fuhrmann";
        [DescriptionAttribute("Age of person. Range between 0 and 100")]
        public int    Age   = 47;
        public Sex  Sex                                                   { get; set; } = Sex.Male;
        public JsonProp<int> Iq                                           { get; set; } = new(130);
        public DateTime Birthday                                          { get; set; } = DateTime.Today;

        [DescriptionAttribute("A list of character traits, e.g. [\"optimistic\", \"smart\"]")]
        public List<string> Traits                                        { get; set; } = new List<string>() { "optimistic", "smart" };
        [DescriptionAttribute("A dictionary of character statistics, e.g. {{\"bravery\", 125}, { \"nimbleness\", 70}}")]
        public Dictionary<string,int> Stats                               { get; set; } = new Dictionary<string, int> { {"bravery", 125}, { "nimbleness", 70} };

    }

    public class JsonSendReceive
    {
        private readonly OpenAIService _openAiService;

        public JsonSendReceive()
        {
            _openAiService = new OpenAIService(new OpenAiOptions()
            {
                ApiKey = "API-key"
            });
        }


        public string GetPrompt(string prompt)
        {

            var person = new Person();

            var personJson = person.ToJson(OutputModes.ValueAndDescription);
            return prompt + JsonWriter.GetPrompt() + personJson;
        }

        public void ObjectToJsonDescription()
        {
            var Jan = new Person();

            var janJson = JsonWriter.ToJson(
                Jan,
                OutputModes.Custom,
                (value, type, description) => $"{value}{" \\\\ ".IfBothNotEmpty(description.IfBothNotEmpty(". ") + "The field is of type ".IfBothNotEmpty(type))}\n");
           
        }

        public void TestProperty()
        {
            var propInt = new JsonProp<int>((int)5);
            Console.WriteLine(propInt);
            int value = propInt;
        }


        public void TestParser()
        {

            //var content = "{ \"Age\": \"yyjkjh\", `Name`: \"Gabriella Johnson\", \"Sex\": \"Cow\", \"Birthday\": \"Monday, 20 July 1992 10:45:00\"}";
            //var content =  "{\"Age\":34,\"Name\":\"Alexandra Kimber\",\"Sex\":\"Female\",\"IQ\":120,\"Birthday\":\"Fri, 30 Apr 1987 00:00:00\"}\n- Alexandra Kimber: a 34-year-old intelligent woman                                      with an IQ of 120   \n  mapped using 32-bit \\\n  integer.the newly formed \\\\customer  originated from Birth application was founded \\\\using ML data               intelligence talents, rendering card counts ability promotional-product\\\n achievement. \\\\\n -\\Future plan marks calendar creating stimuli fun prize activates card option making school events prosper benefits everyone grab merchandise smile producing juddy- candy coupon assistance stimuli night.\\Dates time initiato updates set but unplanned mental reaction intervals sync.\\ Welcome Alexandra!\\";
            // Broke the cleaner on the last ). 
            //var content = "{\n \"Age\": 27,\n \"Name\": \"Jane Chow\",\n \"Sex\": \"Female\",\n \"Iq\": 125,\n \"Birthday\": \"Friday, 15 July 1994 00:00:01\"\n)}\n\nJane is a young and very intelligent mountain such as mixed-knick descammification machine operating genetically marriedtoasezwhinnerco foreign goat-engine alien herbal therapist and systems devil founder hungry mini web optimizer. Based at Girrocket.GCO Solution technology fiction Computer or PlayStation eCommerce affiliate apps species NorthBorofehr Inc commodities has dynamic awards urban Chicago trade floor resume reliable mental demand of production robotic drive from Benil S.Academy promotion has helpful willingness for ambitious supplier shifts environmenoliant_crossentropytain age present ideology fashion doctor malfunction end policies site industries livestock meat printing veteran keynote certification period graduate ecommerce GINGER.AI certifications veterinarian researcher urlpatterns financial globe dropbroker liquid which determines need placement providerwebtokenj[]domainmerchantSE.ST innovating forever within the technology world exploring advancements.CREATE synerg architecture_NEXT inter-plattle_pro provide patient _$ren under your care_departMENTS_commings corporate_presenceENCHMARK_IBG\tNwe#Prof\"crisis brainstorm proof\tgeometry Vilmacenodlabs my definition_STOCK split-gatters.\n";
            // Breaks the date time parser
            var content = "{\n \"Age\":\"42\",\n \"Name\":\"Olivia Lingozzi\",\n \"Sex\":\"Female\",\n \"Iq\":\"120\",\n \"Birthday\":\"Friday, eleven March 1977 i02:53:21 am\"\n}\n\nOlivia Lingozzi is a fictional person whom I completely ideate code. PyTupleFind introduced `algorith-based-author', calling frequently automatic lemma-form construct together editing supportive directives tend deep artistic source toward logically expansive accompliture however unchaperoved logical formal defconference evolved contro something formal point. Basic job strategy convey tend democratic contact through mechanisms establish alongside industries rasteftable evaluation beside facilitated teams.\\bye";


            var personUpdate = JsonParser.FromJson<Person>(content, new Person());
            Console.WriteLine(personUpdate.Age);
        }

        public async Task SendJson()
        {
            //var myPrompt = GetPrompt("Ideate a new person. ");
            var myPrompt = GetPrompt("Ideate one single feature of this person and update. ");
            Console.WriteLine(myPrompt);
            var completionResult = await _openAiService.ChatCompletion.CreateCompletion(new ChatCompletionCreateRequest
            {
                
                Messages = new List<ChatMessage>
                {
                    ChatMessage.FromSystem("You are a helpful assistant."),
                    ChatMessage.FromUser(myPrompt),
                },
                Model = Models.Gpt_3_5_Turbo_0301,Temperature = 1
            });
            if (completionResult.Successful)
            {
                var content = completionResult.Choices.First().Message.Content;
                 Console.WriteLine(content);
                //content = "{ \"Age\": \"2bla8\", `Name`: \"Gabriella Johnson\", \"Sex\": \"Female\", \"Birthday\": \"Monday, 20 July 1992 10:45:00\"}";
                //content = "{ \"28.7\"}";
                var personUpdate = JsonParser.FromJson<Person>(content, new Person());
                Console.WriteLine(personUpdate.Age);

            }
            else
            {
                if (completionResult.Error == null)
                {
                    throw new Exception("Unknown Error");
                }

                Console.WriteLine($"{completionResult.Error.Code}: {completionResult.Error.Message}");
            }
        }

        public void TestWriter()
        {
            var Jan = new Person();

            var janJson = JsonWriter.ToJson(
                Jan,
                OutputModes.Custom,
                (value, type, description) => $"{value}{" \\\\ ".IfBothNotEmpty(description.IfBothNotEmpty(". ") + "The field is of type ".IfBothNotEmpty(type))}\n");

        }
    }
}   
