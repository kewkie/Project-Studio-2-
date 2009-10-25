namespace SP2.Glif.Interpreter
{
    class GlifNextNodeStatement : GlifStatement
    {
        public GlifNextNodeStatement(GlifContext context, string from, string to) : base(context)
        {
            From = from;
            To = to;
        }

        public string From { get; private set; }
        public string To { get; private set; }

        public override void Execute()
        {
            Context.Workflow.AddNodeConnection(From, To);
        }
    }
}
