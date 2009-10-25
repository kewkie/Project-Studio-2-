namespace SP2.Glif.Interpreter
{
    class GlifObject
    {
        public GlifObject(GlifContext context)
        {
            Context = context;
        }

        public GlifContext Context
        {
            get; private set;
        }
    }
}
