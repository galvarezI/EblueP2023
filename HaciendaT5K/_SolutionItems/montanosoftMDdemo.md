
montanosoft demostration of viability 



    /*
     about entity

     about agent

     about entity.model

     about entity.method
        for parameter  <x: Type, y: [Flags]parameterattributes>
        .signature = $generateSymbolFor<method>([Flags]x.target: (new|uuid)|signature|method --> signatureMethod
           donde esta signatura es unica e irrepetible
        .methodAgent = $generateSymbolFor<method,agent>
            ([Flags]x.target: (new|uuid)|instance|method|agent --> instanceMETHODagent, [Flags]y.target: (new|uuid)|signature|method --> signatureMethod 
           donde esta instancia es unica e irrepetible
           y donde la direccion de su signatura puede ser igualable a otra direccion de otro metodo
     
    about conventions
        > method
                > bits
                    raw, data, model, facet,                        [4]raw, data, model, facet,
                    size[9]{rl*, rz**, rm***, ...., rq}             [9]size[9]{rl*, rz**, rm***, ...., rq}
                    safemode, unsafemode,                           [2]safemode, unsafemode,
                    one, oneMore, argList                           [3]one, oneMore, argList
                    clouse:(), reach:[]                             [2]clouse:(), reach:[]
                    assign                                          [1]assign
                    null, new(), default(), typeof(),               [5]null, new(), default(), typeof(), literal
                    (in), out, ref, params                          [4](in), out, ref, params
                    vsv, last                                       [2]vsv, last
                                                                    [32] like color.from<(8, 8 , 8, 8)>

                   like
                        >(/(in)vsv, ref, out > params ?|(?,?||..?)|...?|(?,...?)|(?,?||..?, ...?))
                        > taking [] { a.one:?, b.oneMore: ?,?||..?, c.argList:...?  }

                > calling
                    > symbolId<method.calling.convention>
                        > think about this
                        
                    > symbolId<method.calling.convention.?>    
     instance methods globales
       >> like 
       >> typeof(), nameof( (mem.member{var.variable|field|property|verb}| arg:baseon<var>.argument --> parameter)|blop), sizeof(struct.blop), default(), 
       >> new(?|(?,?||..?)|...?|(?,...?)|(?,?||..?, ...?))
        1.targetof | targedof | targedfor
        2.targed, keyged<keyword>,                              usedby, haved.has
            (naming.identifier ^ blop.identifier) => blopFORnamingFORidentifier
            
            nested<nuid:  keyword with workspaceFORaddress:namingFORidentifier 
            placed<caret: keyword with blopFORnamingFORidentifier   nested on ?body.targetWORKSPACE
            collected<quid: blopFORnamingFORidentifier with naming.identifier  
                nested on ?body.targetBLOP:bodyable | ?body.targetMEM:BLOP, (bodyable|argumentable)
        . target, keyget, placet, nest , collect 
        . by, for, to, from, at
     */
    public interface IbaseOn<blop> { 
    
    }
    public interface facet { 
    
    }
    public interface facetFORstruct<blop> : IbaseOn<facet> where blop:struct 
    {

    }

    public interface facetFORclass<blop>
        where blop: class, new()
    { }

    public interface facetFORinterface<blop>
        where blop : class
    { }

    public interface facetFORformatter { 
    
    
    }


    public static class @clas<variant, kinde>
        where variant: class, new()
    {

       


        

    
    }

    public static class @interface<variant, kinde>
        where variant : class, new()
    {



        public static class @for {




            public static class facettype
            {

                public interface Iformatteror
                {
                   
                    variant FormatVariantFor(kinde expKinde);

                }

                


            }


            public static class modeltype
            {

                public interface Ireflect
                {
                    //esto devolvera la clase que extiende la clase abstract Type
                    kinde thisKinde { get; }

                }

                public interface Ikinde
                {
                    //extended type
                    kinde thisReflective { get; }

                    //based type
                    kinde thisDeclarative { get; }

                }



            }



            public static class datatype
            {



            }

        }

        



    }

    public static class @struct<
        variant, kinde,
        formatteror,
        money, real, single>
        where variant: class, new()
        //TODO in classKinde implement interface for @interface<variant, kinde>.@for.modeltype.Ikinde, @interface<variant, kinde>.@for.modeltype.Ireflect
        where kinde : class, variant 
        where formatteror: class, @interface<variant, kinde>.@for.facettype.Iformatteror
        where money:struct
        where real:struct
        where single: struct
    {

        public interface convertible<blop> where blop : struct
        {
            //Type
            blop thisMe { get;  }

            money MoneyFor(formatteror provider = null);
            real RealFor(formatteror provider = null);
            single SingleFor(formatteror provider = null);

            formatteror FormatterorFor();
        }
    }

    public struct structMoney : 
        @struct<
            classVariant, classKinde, 
            @interface<classVariant, classKinde>.@for.facettype.Iformatteror,
            structMoney, structReal, structSingle
            >.convertible<structMoney>
    {
        private decimal feed;
    public decimal display 
        {
            get => feed;
            set => feed = value;
        }

        #region Iconvertive implementations
        public structMoney thisMe => this;

        public structMoney MoneyFor(@interface<classVariant, classKinde>.@for.facettype.Iformatteror provider = null) => this;
        public structReal RealFor(@interface<classVariant, classKinde>.@for.facettype.Iformatteror provider = null) => new structReal();
        public structSingle SingleFor(@interface<classVariant, classKinde>.@for.facettype.Iformatteror provider = null) => new structSingle();

        public @interface<classVariant, classKinde>.@for.facettype.Iformatteror FormatterorFor() => null;
        #endregion
    }

    public struct structReal
    {
        private double feed;
        public double display
        {
            get => feed;
            set => feed = value;
        }
    }

    public struct structSingle
    {
        private float feed;
        public float display
        {
            get => feed;
            set => feed = value;
        }
    }

    

    /// <summary>
    /// ?
    /// used for controller.methodFORgettng|quering<?T<blop>|?<blop>> 
    /// >> native for incase[] {array, arrayList, collectionList} 
    /// >> extended for incase[] {?& Tsecuency, Tset, Tdocument } 
    /// thinks more about that
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class matrix<T> {
        //T[]
        //Dictionary<string, T> display;
      
    }

    /// <summary>
    /// ?
    /// used for getting ?T<blop> array 
    /// like array 
    /// >> getValue(index) //expresado como exp[index]
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class blox<T> : newOn<matrix<T>>
    {
        private readonly matrix<T> feedBase;

    }

    /// <summary>
    /// ?
    /// used for quering ?T<blop> array list
    /// like array list  >> this[index]
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class index<T> : newOn<matrix<T>>
    {
        //private readonly matrix<T> feedBase;

    }

    /// <summary>
    /// ?
    /// used for quering ?T<blop> collection list 
    /// like collection list  
    /// >> this[index]| this[model]
    /// >> clear()
    /// >> insert(model) | insertAt(index, model)
    /// >> remove(model) | removeAt(index)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class organix<T> : newOn<matrix<T>>
    {
        private readonly matrix<T> feedBase;

    }


    /// <summary>
    /// ?
    /// used for quering ?Tsecuency
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class flux<T> : newOn<matrix<T>>
    {
        private readonly matrix<T> feedBase;

    }


    /// <summary>
    /// ?
    /// for quering ?Tset
    /// like linq.firtOrDefault|where|max|min|any|ToLookup
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class enumex<T> : newOn<matrix<T>>
    {
        //crashs
        //public async unsafe void|System.Threading.Tasks.Task<int> 
        //test(void* argA, byte[]*|object*|delegate|delegate.blop*|string*|interface|interface.blop|bool?*[c#7.3]|void?|void?*|void argB) {

        //    //return new System.Threading.Tasks.Task<int>()
        //}
        public unsafe delegate void method(void* exp);

        public unsafe void test(
            void* argA,
            //[nestedOn(typeof(calendar))]
            DateTimeOffset* argB, DateTime * argC, TimeSpan argD,
            decimal* a, double* b, 
            //EnumBit64, EnumUBit64
            long* c, int* d, short e, 
            byte* f, sbyte* g,
            bool* h, string[] i,
            char* j, int********** k,
            ConsoleKey* arg_, 
            out int********** arg_a, //okay[] {void*|void**||..*: struct.blop*|struct.blop*|**||..*, string}
            ref void********** arg_b, //okay[] {void*|void**||..*: struct.blop*|struct.blop*|**||..*, string}
            params void******[] args) //okay[] {void*|void**||..*: struct.blop*|struct.blop*|**||..*, string}
        {

            arg_a = k;
            //return new System.Threading.Tasks.Task<int>()
        }

        public unsafe  System.Threading.Tasks.Task<int> testB(void* exp)
        {

            return new System.Threading.Tasks.Task<int>(() => 1);
        }

    }

    /// <summary>
    /// ?
    /// for quering  ?Tdocument
    /// like window.getselection()
    /// like window.document.queryselector(<enumFORstring>selectors)
    /// like window.document.queryselectorAll(<enumFORstring>selectors)
    /// like window.document.queryselectorAll(<enumFORstring>selectors)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class nodex<T> : newOn<matrix<T>>
    {

    }


    public class classVariant
    {
       
        

    }

    public abstract class baseOn<T> : classVariant
    where T : class
    {

    }

    public abstract class newOn<T>: baseOn<T>
    where T : class, new()
    {

    }

    


    public abstract class member: baseOn<System.Reflection.MemberInfo>
    { 
    
    
    }

    public class classKinde : member
    {


    }

    public abstract class attribute : baseOn<System.Attribute>
    {


    }

    public abstract class array : baseOn<System.Array>
    {


    }

    public class crash : newOn<System.Exception> { 
    
    }

    



    public class classFormatter { 
    
    
    }