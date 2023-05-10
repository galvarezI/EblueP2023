using mscorlib;

[module: modulationU()]
namespace ModuleU
{
    using ModuleT;
    public class classBasicU
    {
        private string feed;
        private classBasicT feed_nested;

        public string display
        {
            get => feed;
            set => feed = value;
        }

        public classBasicT displayNested
        {
            get => feed_nested;
            set => feed_nested = value;
        }
        public classBasicU()
        { }
    }


}