

namespace CodeFileClass
{

    /*
     env.lang = USA|DOP from Globalization.CultureInfo.Current
     env.getresourceString<out string>(string symbolString )
     env.getresourcesImage<out Image>(string symbolString )
     used for 
            throw new Iexeption (...?)
                exception-1 (x)
                exception-2 (x,y)
                exception-3 (x,y,x)

    Color > Aw, Rx, By, Gz  [8.0], [8.0], [8.0], [8.0] (32uint + 4) > 36
    Alpha   > A[4x15 opt(s)] ?[4x15/3 > 5]x(5), y(5), z(5)
    Red     > R[4x15 opt(s)] ?[4x15/3 > 5]w(5), y(5), z(5)
    Blue    > B[4x15 opt(s)] ?[4x15/3 > 5]w(5), x(5), z(5)
    Green   > G[4x15 opt(s)] ?[4x15/3 > 5]w(5), x(5), y(5)
     */


    public interface Iuri<This, T>
    where This : class, T, new()
    where T : basicUri
    {

    }

    public class QUri {

        /// <summary>
        /// ()
        /// </summary>
        public QUri(basicUri expUri)
        {

            
            

        }

        //public QUri(Iuri<This, T> exp):this(expUri: exp.GetUri())
        //{
        //generado por Interface of Iuri

        //}

        public static QUri Parse(System.Uri uri) => QUriParser.Parse();
        public static bool TryParse(System.Uri uri, out QUri output) => QUriParser.TryParse(expOutput: out output);

    }

    public class QUriResult {

        private QUri feed_parsedValue;
        public QUri Display_ParsedValue
        {
            get => feed_parsedValue;
            set => feed_parsedValue = value;
        }
    }

    public class basicUri : System.Uri {
        //[deprecated]
        //public basicUri(System.Uri expUri):base(baseUri: expUri, relativeUri: string.Empty, dontEscape: false)
        public basicUri(System.Uri expUri) : base(baseUri: expUri, relativeUri: string.Empty)
        {

        }

    }

    public static class QUriParser {



        public static QUri Parse() => null;

        public static bool TryParse(out QUri expOutput) { expOutput = null; return false; } 

        public static QUri ParseExact(object target, params string[] args) => null;

        public static bool TryParseExact(out QUri expOutput, object target, params string[] args) { expOutput = null; return false; }


    }

    public static class QUriParserHelper
    {
        private static bool flag;
        public static void helperFor(this QUriResult result) => flag =true;
    
    }

    }