namespace ParserTests
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var interpreter = new SP2.Glif.Interpreter.GlifInterpreter(@"res\breast-mass-glif.glif");
            interpreter.Parse();
            var workflow = interpreter.CompileWorkflow();
        }
    }
}
