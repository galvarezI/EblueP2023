using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static System.Math;
namespace Eblue.Code
{

    //_, a, b, c, d, e, f
    //?, l, z, m, y, s, b
    [Flags]
    public enum @enum: sbyte { 

        scope = sbyte.MinValue,

    
        zero,
        one, two, 
        three = 4,
        #region three composite blop(s)
        three_s =one | three,
        #endregion
        four = 8, five = 16, 
        six = 32,
        #region six composite blop(s)
           six_l = three_s| six,
        #endregion
        seven = 64
    //
    }

    public class EnumConstants
    {

        
    }

    [Flags]
    //public enum DateTimeFlags: ulong
    public enum enumFlags :long
    {
        _None,
        //None = 0b_0000_0000,  // 0
        #region [a.1...9] @09
        a1,
        a2, a3= a2 * 2, a4 = 2 * a3, a5 = 2 * a4,
        a6 = 2 * a5, a7 = 2 * a6, a8 = 2 * a7, a9 = 2 * a8,


        #endregion
        #region [b.1...9] @18
        b1 = 2 * a9,
        b2 = 2 * b1, b3 = b2 * 2, b4 = 2 * b3, b5 = 2 * b4,
        b6 = 2 * b5, b7 = 2 * b6, b8 = 2 * b7, b9 = 2 * b8,
        #endregion
        #region [c.1...9] @27
        c1 = 2 * b9,
        c2 = 2 * c1, c3 = c2 * 2, c4 = 2 * c3, c5 = 2 * c4,
        c6 = 2 * c5, c7 = 2 * c6, c8 = 2 * c7, c9 = 2 * c8,
        #endregion
        #region [d.1...9] @36
        d1 = 2 * c9,
        d2 = 2 * d1, d3 = d2 * 2, d4 = 2 * d3, d5 = 2 * d4,
        d6 = 2 * d5, d7 = 2 * d6, d8 = 2 * d7, d9 = 2 * d8,
        #endregion
        #region [e.1...9] @45
        e1 = 2 * d9,
        e2 = 2 * e1, e3 = e2 * 2, e4 = 2 * e3, e5 = 2 * e4,
        e6 = 2 * e5, e7 = 2 * e6, e8 = 2 * e7, e9 = 2 * e8,
        #endregion
        #region [f.1...9] @54
        f1 = 2 * e9,
        f2 = 2 * f1, f3 = f2 * 2, f4 = 2 * f3, f5 = 2 * f4,
        f6 = 2 * f5, f7 = 2 * f6, f8 = 2 * f7, f9 = 2 * f8,
        #endregion
        #region [g.1...9] @63
        g1 = 2 * f9,
        g2 = 2 * g1, g3 = g2 * 2, g4 = 2 * g3, g5 = 2 * g4,
        g6 = 2 * g5, g7 = 2 * g6, g8 = 2 * g7, g9 = 2 * g8,
        #endregion

        //#region [h.1...1] @64
        //h1 = 2 * g9,
        //#endregion
        #region [i.1...1] @64
        h1 = ~(long.MaxValue),
        #endregion
        //#region [i.1...1] @65
        //i1 = ~(long.MaxValue),
        //#endregion

        //Monday = 0b_0000_0001,  // 1
        //Tuesday = 0b_0000_0010,  // 2
        //Wednesday = 0b_0000_0100,  // 4
        //Thursday = 0b_0000_1000,  // 8
        //Friday = 0b_0001_0000,  // 16
        //Weeklon = Monday..|.. Friday
        //Saturday = 0b_0010_0000,  // 32
        //Sunday = 0b_0100_0000,  // 64
        //Weekend = Saturday | Sunday
    }


    /// <summary>
    /// this is the info class
    /// </summary>
    public class TestOverLoading
    {

        #region fail for before c#8
        //public void @do(ref int u) { }
        //public void @do(out int v) { }
        #endregion


    }
    public class handlerEventArgs : EventArgs
    {
        public string Args { get; set; }
        public handlerEventArgs() :base() { }
    }
    public delegate void actionHandler(object sender, handlerEventArgs e );

    
    public class Dome
    {

        public void doit()
        {
            
            
            //int[] someArray = new int[5] { 1, 2, 3, 4, 5 };
            //int lastElement = someArray[^1]; // lastElement = 5

            enumFlags evalue = enumFlags.g9;
            long uvalue = (long)evalue;

            object target = "sender";
            handlerEventArgs eventArgs = new handlerEventArgs() { Args = "(1)(4)(8)" };
            Ttype type = new Ttype(target: target, eventArgs: eventArgs);
            //bool flagDoClick;
            try
            {
                type.doClick();
                //flagDoClick = true;
            }
            catch (Exception ex)
            {

            }

            type.clickFor( (x,y) => {
                bool stop = true;
                if (stop)
                    Console.WriteLine("stop");
            });

            type.doClick();

            Ttypesafe typesafe = new Ttypesafe(target: target, eventArgs: eventArgs);
            //bool flagDoClick;
            try
            {
                typesafe.doClick();
                //flagDoClick = true;
            }
            catch (Exception ex)
            {

            }

            typesafe.clickFor((x, y) => {
                bool stop = true;
                if (stop)
                    Console.WriteLine("stop");
            });

            typesafe.doClick();

        }
    }
    public class Ttype
    {
        private event actionHandler _click;
        public event actionHandler Click {
            add {
                _click += value;
            }
            remove {
                _click -= value;
            }
        }

        public readonly object target;
        public readonly handlerEventArgs eventArgs;
        public Ttype(object target, handlerEventArgs eventArgs)
        {
            this.target = target;
            this.eventArgs = eventArgs;

        }
        public Ttype()
        {

            
        
        }

        public void clickFor(actionHandler actionHandler) 
        {

            Click += actionHandler;
        }

        public void doClick() {

            _click(sender:target, e: eventArgs);
        }
    }

    public class Ttypesafe
    {
        private event actionHandler _click = delegate {
            bool doit = true;
            if (doit)
                Console.WriteLine("do nothing");
        };
        public event actionHandler Click
        {
            add
            {
                _click += value;
            }
            remove
            {
                _click -= value;
            }
        }

        public readonly object target;
        public readonly handlerEventArgs eventArgs;
        public Ttypesafe(object target, handlerEventArgs eventArgs)
        {
            this.target = target;
            this.eventArgs = eventArgs;

        }
        public Ttypesafe()
        {



        }

        public void clickFor(actionHandler actionHandler)
        {

            Click += actionHandler;
        }

        public void doClick()
        {

            _click(sender: target, e: eventArgs);
        }
    }
}


/// <summary>
/// this the info namespace
/// </summary>
namespace mscorlib
{
    using static AttributeTargets;

    //note  mark caret -> ‸ | ⁁ 
    //(‸, ⁁) placed below the line to indicate a proposed insertion in a printed or written text.

    public static class @const 
    {

        public const AttributeTargets blopTargets = Class | Event | Delegate | Interface | Struct | Enum;

        /// <summary>
        /// remember ()the type is like <blop> then @object { evalBlop ()=> this.GetType();   }
        /// in the mechanic of the msc [attribute] [attribute]-usage(validOn<[Attribute]Targets>):
        /// 0. event is whenFalse a target type
        /// 1. type? class|struct|enum|delegate|interface is whenTrue a target
        /// 2. field come from(class|struct).feed
        /// 3. method -> member.method.action come from(class|struct).exec|display.set
        /// 4. return -> member.method.func come from(class|struct).eval|display.get
        /// 5. param come from(class|struct).<action|func>|indexer.subj|flag with evalIndexer
        /// 6. event  -> member.listner come from(class|struct).<evalJet|evalJob>
        /// 7. property come from(class|struct).display
        /// 8. module 
        /// 9. assembly 
        /// </summary>
        public const AttributeTargets typeTargets = Class | Delegate | Interface | Struct | Enum;
    }

    [AttributeUsage(validOn: Module, AllowMultiple = false, Inherited = true)]
    public class modulationTAttribute : Attribute { }

    [AttributeUsage(validOn: Module, AllowMultiple = false, Inherited = true)]
    public class modulationUAttribute : Attribute { }

    [AttributeUsage(validOn: AttributeTargets.All, AllowMultiple = false, Inherited =true)]
    public abstract class anotationBlop : Attribute
    { 
    
    }

    #region about ?

    #endregion

    #region about member Blop method.delegate.IncludeOn

    #region about func(s)
    [AttributeUsage(validOn: AttributeTargets.ReturnValue, AllowMultiple = false, Inherited = true)]
    public abstract class funcBlop : anotationBlop { }

    /// <summary>
    /// used for boilerplate
    /// </summary>
    public class funcAttribute : funcBlop { }
    #endregion

    #region about action(s)
    [AttributeUsage(validOn: AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public abstract class actionBlop : anotationBlop { }

    /// <summary>
    /// used for boilerplate
    /// </summary>
    public class actionAttribute : actionBlop { }
    #endregion

    #region about predicate(s)

    #endregion

    #region about comparition(s)

    #endregion

    #region about selection(s)

    #endregion
    #endregion

    #region about member Blop method.delegate.IncludeOn
    #region about ?

    #endregion
    #endregion
}

namespace system
{
    using mscorlib;
    using System.Runtime.CompilerServices;
    using static @operator;

    [Flags()]
    public enum @operator
    {
        None,
        not,
        and,
        or = 4,
        xor = 8,
        with = 16,
        bits = 32,
        notFORand = bits | with | not | and,
        notFORor = bits | with | not | or,
        notFORxor = bits | with | not | xor,
        //andFORand = bits | with | and | xor,
        andFORor = bits | with | and | or,
        andFORxor = bits | with | and | xor,
        //orFORand = bits | with | or | xor,
        //orFORor,
        orFORxor = bits | with | or | xor,
        //xorFORand,
        //xorFORor,
        //xorFORxor,
    }

    

    public interface Izjet<T> where T : IConvertible
    {
        event @class<T>.funcDelegate  evalJet;
    }
    public interface Izjob<T> :Izjet<T>
        where T:IConvertible
    {
       event @class<T>.actionDelegate execJob;
    }
    public interface Iyjob<U> : Izjet<U>
        where U:IConvertible
    {
        event @class<U>.actionDelegate execJob;
    }

    public class @class<T> : Izjob<T>, Iyjob<T>
        where T : IConvertible
    {
        
        public delegate void actionDelegate(T input = default(T), @class<T> sender = default(@class<T>));
        //public delegate void actionDelegate<U>(U input);

        public delegate T funcDelegate(@class<T> sender = default(@class<T>));
        //public delegate U funcDelegate<U>(U input);

        //job is executado sin esperar respuesta inmediata del servidor
        private actionDelegate _zjob;
        private actionDelegate _yjob;
        private funcDelegate _jet;

        public event funcDelegate evalJet
        {
            add => _jet += value;
            remove => _jet -= value;
        }

        event actionDelegate Izjob<T>.execJob
        {
            //[MethodImpl(methodImplOptions: MethodImplOptions.Synchronized)]
            add => _zjob += value;
            remove => _zjob -= value;
        }

        event actionDelegate Iyjob<T>.execJob
        {
            add => _yjob += value;
            remove => _yjob -= value;
        }



        #region nested types
        #region nested types delegates
        public delegate T evalINz<U>(in U z, in decimal arg) where U : T;
        public delegate T evalINzOUT<U>(in U z, out double arg) where U : T, IConvertible;
        public delegate T evalINy<U>(in U z, ref decimal arg) where U : T;
        public delegate T evalINyREF<U>(in U z, ref double arg) where U : T;
        //info: upper O -> in() like array|list type[]
        public delegate T evalINxO<U>(in U z, in decimal[] args) where U : T;
        public delegate T evalINxOUTO<U>(in U z, out double[] args) where U : T;
        //info: upper M -> params() like array|list   params type[]
        public delegate T evalINwM<U>(in U z, params bool[] args) where U : T;

        #endregion
        #endregion

        private T feed;
        //private evalINzOUT<T> evalz = (double z, out double e) => { e = 1; return 1; };

        public T GetTzout<U>(in U z, out double e)
        {
            e = 1;
            return this.display;
        }

        public T GetTyref<U>(in U z, ref sbyte e)
        {
            e = 1;
            return this.display;
        }

        public T GetTxoutO<U>(in U z, out uint[] e)
        {
            //this below asume a{ $0, $1, $2}
            //var money =  new{  134u,  3u, 4u };
            e = new uint[]{ 0u , 1u };
            return this.display;
        }

        public T GetTwinM<U>(in U z, params bool[] e)
        {
            var args = e.Length;
            
            return this.display;
        }

        public T display {
            get => feed;
            set => feed = value;
        }

        /// <summary>
        /// aply thre rule for combination
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        public T this [bool subj, @operator flag]
        {
            get => feed;
            set 
            {
                if (flag == None)
                {
                    this.feed = default(T);
                }
                else 
                {
                    this.feed = value;
                }

            }
        }

       

        public  @class()
            {}

        

        [return: func()]
        public T eval() => display;

        [return: func()]
        public T eval<U>(in U z, decimal e) where U: T => display;

        [return: func()]
        public T eval<U>(in U z, out double e) => GetTzout<U>(z, out e);

        [return: func()]
        public T eval<U>(in U z, ref sbyte e) => GetTyref<U>(z, ref e);

        [return: func()]
        public T eval<U>(in U z, out uint[] s) => GetTxoutO<U>(z, out s);

        [return: func()]
        public T eval<U>(in U z, params bool[] a) => GetTwinM<U>(z, a);

        [method: action()] 
        public void exec(T value) => display = value;

    }

    public interface ifirst 
    { 
      object myName { get; set; }
    
    }

    public interface isecond
    {
        string myName { get; set; }

    }


    public class classBasic
    {
        #region nested types
        
        #endregion

        private bool feed;
        //private evalINzOUT<T> evalz = (double z, out double e) => { e = 1; return 1; };

        #region eval class.method(s).func

        public bool GetTzout<U>(in U z, out double e)
        {
            e = 1;
            return this.display;
        }

        public bool GetTyref<U>(in U z, ref sbyte e)
        {
            e = 1;
            return this.display;
        }

        public bool GetTxoutO<U>(in U z, out uint[] e)
        {
            //this below asume a{ $0, $1, $2}
            //var money =  new{  134u,  3u, 4u };
            e = new uint[] { 0u, 1u };
            return this.display;
        }

        public bool GetTwinM<U>(in U z, params bool[] e)
        {
            var args = e.Length;

            return this.display;
        }
        #endregion

        #region exec class.method(s).action

        #endregion

        public bool display
        {
            get => feed;
            set => feed = value;
        }

        public bool evalIndexer(bool value, bool subj, @operator flag) 
        
        {
            bool zvalue = this.feed;
            bool yvalue = value;
            bool xvalue = subj;
            bool wvalue = false;

            switch (flag)
            {
                case notFORand:
                    wvalue = !(zvalue) & (yvalue & xvalue); 
                    break;
                case notFORor:
                    wvalue = !(zvalue) | (yvalue | xvalue);
                    break;
                case notFORxor:
                    wvalue = !(zvalue) ^ (yvalue ^ xvalue);
                    break;
                case andFORor:
                    wvalue = (zvalue) & (yvalue | xvalue);
                    break;
                case andFORxor:
                    wvalue = (zvalue) & (yvalue ^ xvalue);
                    break;
                case orFORxor:
                    wvalue = (zvalue) | (yvalue ^ xvalue);
                    break;
                default:
                    wvalue = yvalue ;
                    break;
            }

            return wvalue;

        }

        /// <summary>
        /// aply thre rule for combination
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        public bool this[bool subj, @operator flag]
        {
            get => feed;
            set => evalIndexer(value: value, subj: subj, flag: flag);
        }

       
        public classBasic()
        { }



    }

}


#region region._.?

#region region.a.hubb

#region region.b.hubb
//main(:this) -> main.principal
#endregion
#region region.c.hubb
//principal(recall) -> principal.beta, beta<hotfix> -> {beta.develop}, develop<essential> -> {develop.staging}, staging<feature>
#endregion

#region region.d.hubb
//recall pull from pricipal, detected by ?.. 
//deprecate add to recall, this deprecate-better whatever exception for improve performance 
//resolve add to recall this resolve-z whatever bug for sell correction
//abandon add to recall, this abandon-y whatever halt-caused for libs-pattern-api for well goon evolution

//hotfix pull from  beta, detected by ?.. of QA|?.. at whatever time

//[in feature, you must create the full skeleton scope, like TODO in each $module -> refer to csharp file]
//feature full from staging,  suggested by delivery plan <proposition> 
//capability add to feature,  suggested by delivery plan <construction> 
//functionality add to feature,  suggested by delivery plan <construction> 

//essential pull from develop, suggested by delivery plan <integration> 
//x.? add to essential,  suggested by delivery plan <continuos> 
//y.? add to essential,  suggested by delivery plan <continuos> 
//:determination(s)
/*
 0. the blank solution project was create previusly
 1. feature.pull(staging) -> new feature("ddd.initial-staging")
      skeleton scope for all $module 
      when each $module constain []{//TODO $"{notes}"}
 2. brach(s)|commit(s)|pull(s)|pullrequest(s)|?..
 3. merged to develop.staging
 */
/*
 0. the skeleton scope feature was setting previusly
 1. essential.pull(develop) -> new essential("ddd.initial-develop")
      complete|mark|?.. in skeleton scope for all $module 
      then each $module constain []{//TODO $"{notes}"}
      and must complete with code changes
 2. brach(s)|commit(s)|pull(s)|pullrequest(s)|?..
 3. merged to beta.essential
 */

#endregion

#region region.e.hubb
//br-x?
//ddd.initial-staging
//ddd.initial-develop

#region region.f.hubb
//co-x?
//ddd.initial-staging
//      TODO() $module @z   ns[1] { blop(s).unfilled   }, ns[2]{  blop(s).unfilled  }
//      TODO() $module @y
//      TODO() $module @x
//ddd.initial-develop
//      TODO(marked) $module @z   ns[1] { blop(s).filled   }, ns[2]{  blop(s).filled  }
//      TODO(marked)
//      TODO(marked)
#endregion
#endregion

#region region.g.hubb
//pr-x?-
//ddd.initial-staging
//ddd.initial-develop
#endregion
#endregion



#endregion
namespace DomeNamespace
{

}

