namespace SP2.Glif.Interpreter
{
    class GlifParameter : GlifObject
    {
        public GlifParameter(GlifContext context, GlifAttribute attribute, GlifValue value) : base(context)
        {
            Attribute = attribute;
            Value = value;
        }

        public GlifAttribute Attribute { get; private set; }
        public GlifValue Value { get; private set; }
    }
}
