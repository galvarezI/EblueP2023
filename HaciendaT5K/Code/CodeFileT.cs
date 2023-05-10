using mscorlib;

[module:modulationT()]
namespace ModuleT
{

    public class classBasicT {
        private string feed;

        public string display {
            get => feed;
            set => feed = value;
        }
        public classBasicT()
        { }
    }

}