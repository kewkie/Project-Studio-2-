namespace SP2.Glif.Interpreter
{
    class GlifParameterList : GlifParameter
    {
        private readonly GlifParameter _current;
        private readonly GlifParameter _next;
        
        public GlifParameterList(GlifContext context, 
            GlifParameter currentParameter, 
            GlifParameter nextParameter) : base(context,currentParameter.Attribute,currentParameter.Value)
        {
            _current = currentParameter;
            _next = nextParameter;
        } 
    }
}
