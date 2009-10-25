namespace SP2.Glif.Interpreter
{
    class GlifValue : GlifObject
    {
        public GlifValue(GlifContext context, string value) : base(context)
        {
            Value = value;
        }

        public string Value { get; private set; }
    }
}
