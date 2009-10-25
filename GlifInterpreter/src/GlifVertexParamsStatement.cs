namespace SP2.Glif.Interpreter
{
    class GlifVertexParamsStatement : GlifStatement
    {
        public GlifVertexParamsStatement(GlifContext context, string from, string to, GlifParameter parameterList) : base(context)
        {
            From = from;
            To = to;
            Parameters = parameterList;
        }

        public string From { get; private set; }
        public string To { get; private set; }
        public GlifParameter Parameters { get; private set; }

        public override void Execute()
        {
            Context.Workflow.AddVertexParam(From,To,Parameters.Attribute.Name, Parameters.Value.Value);
        }
    }
}
