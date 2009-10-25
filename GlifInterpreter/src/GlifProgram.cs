namespace SP2.Glif.Interpreter
{
    class GlifProgram : GlifStatement
    {
        public GlifProgram(GlifContext context, string type, string name, GlifStatement statement) : base(context)
        {
            Type = type;
            Name = name;
            Statement = statement;
        }

        public string Type { get; private set; }
        public string Name { get; private set; }
        public GlifStatement Statement { get; private set; }

        public override void Execute()
        {
            Context.Workflow.Name = Name;
            Context.Workflow.Type = Type;
            Statement.Execute();
        }
    }
}
