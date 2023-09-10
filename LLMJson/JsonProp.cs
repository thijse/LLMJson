
namespace LLMJson
{
    public enum UpdateStates
    {
        Unchanged,
        InvalidUpdate,
        Updated
    };

    public class JsonProp<T> 
    {
        private T _value;


        public string Description       { get; set; } = "";
        public UpdateStates UpdateState { get; set; } = UpdateStates.Unchanged;
        public bool Visible             { get; set; } = true;
        public bool Immutable           { get; set; } = false;

        public T Value {
            get {                return _value;  }
            set { if(!Immutable) _value = value; }
        }

        public JsonProp(T value, string description= "", bool visible = true, bool immutable =false)
        {
            _value      = value;
            Description = description;
            Visible     = visible;  
            Immutable   = immutable;    
        }



        public override string ToString() { try { return Value?.ToString() ?? ""; }catch {return "";} }

        public static implicit operator T(JsonProp<T> instance)
        {
            //if (instance._value == null) { return default(T)! ; }
            return instance._value;
        }
    }
}
