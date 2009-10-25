using System;
using System.IO;
using System.Reflection;
using System.Text;
using SP2.Glif.Model;
using GoldParser;

namespace SP2.Glif.Interpreter
{
    public class GlifInterpreter
    {
        private readonly string _fileText;
        private readonly Grammar _grammar;
        private readonly Parser _parser;
        private readonly GlifContext _context;

        internal GlifStatement Program { get; private set; }

        public GlifInterpreter(string filename)
        {
            var streamReader = new StreamReader(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "\\" + filename);
            _fileText = streamReader.ReadToEnd();
            streamReader.Close();

            var currentAssembly = Assembly.GetExecutingAssembly();
            var stream = currentAssembly.GetManifestResourceStream("SP2.Glif.Interpreter.res.glifgrammar.cgt");
            if (stream != null)
            {
                var binaryReader = new BinaryReader(stream);
                _grammar = new Grammar(binaryReader);
            }
            else
            {
                throw new FileNotFoundException("Grammar file not found!");
            }
            
            _parser = new Parser(new StringReader(_fileText), _grammar) {TrimReductions = true};
            _context = new GlifContext(_parser);
        }
        
        public void Parse()
        {
            while(true)
            {
                switch (_parser.Parse())
                {
                    case ParseMessage.Empty:
                        break;
                    case ParseMessage.TokenRead:
                        _parser.TokenSyntaxNode = _context.GetTokenText();
                        break;
                    case ParseMessage.Reduction:
                        _parser.TokenSyntaxNode = _context.ExecuteReductionRule();
                        break;
                    case ParseMessage.Accept:
                        Program = (GlifStatement)_parser.TokenSyntaxNode;
                        return;
                    case ParseMessage.LexicalError:
                        throw new InvalidDataException("LEXICAL ERROR. Line " + _parser.LineNumber + ". Cannot recognize token: " + _parser.TokenText);
                    case ParseMessage.SyntaxError:
                        var text = new StringBuilder();
                        foreach (Symbol tokenSymbol in _parser.GetExpectedTokens())
                        {
                            text.Append(' ');
                            text.Append(tokenSymbol.ToString());
                        }
                        throw new InvalidDataException("SYNTAX ERROR. Line " + _parser.LineNumber + ". Expecting:" + text);
                    case ParseMessage.InternalError:
                        throw new InvalidOperationException("INTERNAL ERROR! Something is horribly wrong");
                    case ParseMessage.NotLoadedError:
                        throw new InvalidDataException("NOT LOADED ERROR! Compiled Grammar Table not loaded");
                    case ParseMessage.CommentError:
                        throw new InvalidDataException("COMMENT ERROR! Unexpected end of file");
                    case ParseMessage.CommentBlockRead:
                        break;
                    case ParseMessage.CommentLineRead:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        public GlifWorkflow CompileWorkflow()
        {
            ((GlifStatement)_parser.TokenSyntaxNode).Execute();
            return _context.Workflow;
        }
    }
}
