using LLMJson;

namespace LLMJson_sample
{
    public enum Sex { Male, Female, Other}

    public class Person
    {
        [DescriptionAttribute("Firstname only")]
        public string                  Name     { get; set; }

        [DescriptionAttribute("Age of person. Range between 0 and 100")]
        public int                     Age      { get; set; }

        public Sex                     Sex      { get; set; }

        public JsonProp<int>           Iq       { get; set; }

        public DateTime                Birthday { get; set; }

        [DescriptionAttribute("A list of character traits, e.g. [\"optimistic\", \"smart\"]")]
        public List<string>            Traits   { get; set; }

        [DescriptionAttribute("A dictionary of character statistics, e.g. {{\"bravery\", 100}, { \"nimbleness\", 100}}")]
        public Dictionary<string, int> Stats    { get; set; }

        public Person()
        {
            Name     = "";
            Age      = 0;
            Sex      = Sex.Male;
            Iq       = new JsonProp<int>(0, "");
            Birthday = DateTime.MinValue;
            Traits   = new List<string>() { };
            Stats    = new Dictionary<string, int> { };
        }

        public void SetNigel()
        {
            Name     = "Nigel Thornberry";
            Age      = 47;
            Sex      = Sex.Male;
            Iq       = new JsonProp<int>(130, "Intelligence coefficient");
            Birthday = DateTime.Today;
            Traits   = new List<string>() { "sneaky", "funny" };
            Stats    = new Dictionary<string, int> { { "bravery", 80 }, { "nimbleness", 70 } };
        }
    }



}
