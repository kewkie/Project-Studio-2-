namespace SP2.Glif.Interpreter
{
    class GlifNodeParamsStatement : GlifStatement
    {
        public GlifNodeParamsStatement(GlifContext context, string id, GlifParameter parameterList) : base(context)
        {
            Id = id;
            Parameters = parameterList;
        }

        public string Id { get; private set; }
        public GlifParameter Parameters { get; private set; }

        public override void Execute()
        {
            Context.Workflow.AddNodeParam(Id, Parameters.Attribute.Name, Parameters.Value.Value);
        }
    }
}
