namespace SP2.Glif.Interpreter
{
    class GlifAttribute : GlifObject
    {
        public GlifAttribute(GlifContext context, string name) : base(context)
        {
            Name = name;
        }

        public string Name { get; private set; }
    }
}