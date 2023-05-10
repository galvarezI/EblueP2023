



namespace mnscorlib
{

    namespace montanosoft {
    
    }
    
    namespace engage {
        using System;
        using System.Threading.Tasks;
        using static EnumUFlag64;


        #region modeling

        /*
        1(1, 1), 2, 4, 8,                   ord[4]{1, 2, 3, 4}      card[]{1, 2, 3, 4}
        16, 32, 64,                   ord[3]{5, 6, 7}         card[]{5, 6, 7}
        128, 256, 512,                ord[3]{8, 9, 10}        card[]{8, 9, 10}

        1024(11, 1), 2048, 4096, 8192,       ord[4]{11, 12, 13, 14}  card[]{1, 2, 3, 4}
        16.384, 32.768, 65.536,       ord[3]{} card[]{}
        131.072, 262.144, 524.288     ord[3]{} card[]{}


      (1, 2, 4, 8, ) (16, 32, 64,) (128, 256, 512,)
(1024, 2048, 4096, 8192,)(16384, 32768, 65536, ) (131072, 262144, 524288)
1, 2, 4, 8, 
10, 20, 40, 80, 100, 200, 400, 800, 1000, 2000, 4000, 8000, 10000, 20000, 40000, 80000

0x10 > 16
0x20 > 32
0x40 > 64
0x80  > 128
0x100  > 256
x200 512
x400 1024
x800 2048
x1000  4096
x2000  8192
x4000  16384
x8000 32768
x10000 65536
x20000 131072
      x40000 262144
      x80000 524288

        x1 : x8
        x10 -:- x80

      c a b g r

      c  b  g  r
      a  c  g  r
      a  b  c  r
      a  b  g  c

      3, 4, 4, 4,
      4, 3, 4, 4,
      4, 4, 3, 4,
      4, 4, 4, 3,


      A<b|g|r[3]> [12][c; l: v]
      B<a|g|r[3]> [12][c; l: v]
      G<a|b|r[3]> [12][c; l: v]
      R<a|b|g[3]> [12][c; l: v]
      T<w|s|l[3]> w -> like white, s-> like silver as black, l -> light

      1, 2, 4 , 8                 -> 1, 2, 4, 8
      16, 32, 64, 128             -> 10, 20 , 40 , 80
      256, 512, 1024, 2048        -> 100, 200, 400, 800
      4096, 8192, 16384, 32768    -> 1000, 2000, 4000, 8000

      1, 2, 4 , 8                 -> 1.0000, 2.0000, 4.0000, 8.0000
      16, 32, 64, 128             -> 10.0000, 20.0000, 40.0000, 80.0000
      256, 512, 1024, 2048        -> 100.0000, 200.0000, 400.0000, 800.0000
      4096, 8192, 16384, 32768    -> 1000.0000, 2000.0000, 4000.0000, 8000.0000

      1, 2, 4 , 8                 -> 1.0000.0000, 2.0000.0000, 4.0000.0000, 8.8000.0000,
      16, 32, 64, 128             -> 10.0000.0000, 20.0000.0000, 40.0000.0000, 80.8000.0000,
      256, 512, 1024, 2048        -> 100.0000.0000, 200.0000.0000, 400.0000.0000, 800.8000.0000,
      4096, 8192, 16384, 32768    -> 1000.0000.0000, 2000.0000.0000, 4000.0000.0000, 8000.8000.0000,

      1, 2, 4 , 8                 -> 1.0000.0000.0000, 2.0000.0000.0000, 4.0000.0000.0000, 8.0000.0000.0000,
      16, 32, 64, 128             -> 10.0000.0000.0000, 20.0000.0000.0000, 40.0000.0000.0000, 80.0000.0000.0000,
      256, 512, 1024, 2048        -> 100.0000.0000.0000, 200.0000.0000.0000, 400.0000.0000.0000, 800.0000.0000.0000,
      4096, 8192, 16384, 32768    -> 1000.0000.0000.0000, 2000.0000.0000.0000, 4000.0000.0000.0000, 8000.0000.0000.0000,


      type ordinal        > abrev. ord    --> ord[1:63]
      type cardinal       > abrev. card   --> card(<a >> d >> e >> i >> o >> u, d |đ: d >[l:p]
      type goglean        > abrev. gog    --> gog( < (A ^ <b|g|r>|B|G|R) ^ T(w vsv s vsv l)> 
      type nineball       > abrev. ninb   --> ninb(<_; a:z ; altGo | đ : altGo >[1:9]
      type xheximal       > abrev. xhex   --> xhex()
      .
      .
      .
      .
      type eonic          > abrev. eoc    --> eoc(<f-[x:z]>([1:9]|[c; l:v]



         [10](1, 2, 4, 8) (16, 32, 64) (128, 256, 512)
         [10](1024, 2048, 4096, 8192) (16384, 32768, 65536) (...?, ...?, ...?)
         10 x 6 > 60 + (3) = 63  -->[l:p] (a, (d), e, i , o , u, )
         1, 2, 3, 4              
         4 x 1 > 4    --> v; đ = v,   


          a, b, g, r (1, 2, 4, 8)
          a, b, g, r (16, 32, 64, 128)
          a, b, g, r (256, 512, 1024, 2048)

      l, z, m, y
       ?  A4{?>[l:y]} B4{} G4{} R4{}
       a  A 4{ac} , B 4 {ab}, G 4 {ag} , R 4 {ar}
       b  B 4{bc} , A 4 {ba}, G 4 {bg} , R 4 {br}
       g  G 4{gc} , B 4 {gb}, A 4 {ag} , R 4 {ar}
       r  R 4{rc} , B 4 {rb}, G 4 {ag} , A 4 {ar}

        _1: _9    9 x 7 = 63
        a1: a9
        b1: b9
        d1: d9
        h1: h9
        p1: p9
        đ1: đ9

        fx 1:9, c; l:v  -> 21 x 3 = 63
        fy
        fz

      đ -> alt+273 (256|integer(leadingwhite|trailingwhite|leadingsimbol))

      //fx1,
          //fx2,
          //#region fx2 by [1]
          //fx2a,
          //#endregion
          //fx3,
          //#region fx3 by [3]
          //fx3a, fx3b, fx3c,
          //#endregion
          //fx4,
          //#region fx4 by [7]
          //fx4a, fx4b, fx4c, fx4d, fx4e, fx4f, fx4g,
          //#endregion
          //fx5,
          //#region fx5 by [15]

          // fx5 > ...fx5 (x.safepoint[a:z]:y.safepoint[_;1.?:9.?])

          //#endregion
          //fx6 = fx5 * 2,
          //fx7 = fx6 * 2,
          //fx8 = fx7 * 2,
          //fx9 = fx8 * 2,
          //fxl = fx9 * 2,
          //fxz = fxl * 2,
          //fxm = fxz * 2,
          //fxy = fxm * 2,
          //fxs = fxy * 2,
          //fxb = fxs * 2,
          //fxj = fxb * 2,
          //fxg = fxj * 2,
          //fxq = fxg * 2,
          //fxp = fxq * 2,
          //fxv = fxp * 2,

       */

        [Flags]
        public enum EnumUFlag64 : ulong
        {

            undefined = ~zero,
            zero = 0,

            #region fx args
            _1,
            fx1 = _1,
            _2,
            fx2 = _2,
            _3 = fx2 * 2,
            fx3 = _3,
            _4 = fx3 * 2,
            fx4 = _4,
            _5 = fx4 * 2,
            fx5 = _5,
            _6 = fx5 * 2,
            fx6 = _6,
            _7 = fx6 * 2,
            fx7 = _7,
            _8 = fx7 * 2,
            fx8 = _8,
            _9 = fx8 * 2,
            fx9 = _9,
            a1 = fx9 * 2,
            fxc = a1,
            a2 = fxc * 2,
            fxl = a2,
            a3 = fxl * 2,
            fxz = a3,
            a4 = fxz * 2,
            fxm = a4,
            a5 = fxm * 2,
            fxy = a5,
            a6 = fxy * 2,
            fxs = a6,
            a7 = fxs * 2,
            fxb = a7,
            a8 = fxb * 2,
            fxj = a8,
            a9 = fxj * 2,
            fxg = a9,
            fxq = fxg * 2,
            fxp = fxq * 2,
            fxv = fxp * 2,
            #endregion
            #region fy args
            fy1 = fxv * 2,
            fy2 = fy1 * 2,
            fy3 = fy2 * 2,
            fy4 = fy3 * 2,
            fy5 = fy4 * 2,
            fy6 = fy5 * 2,
            fy7 = fy6 * 2,
            fy8 = fy7 * 2,
            fy9 = fy8 * 2,
            fyc = fy9 * 2,
            fyl = fyc * 2,
            fyz = fyl * 2,
            fym = fyz * 2,
            fyy = fym * 2,
            fys = fyy * 2,
            fyb = fys * 2,
            fyj = fyb * 2,
            fyg = fyj * 2,
            fyq = fyg * 2,
            fyp = fyq * 2,
            fyv = fyp * 2,
            #endregion
            #region fz args
            fz1 = fyv * 2,
            fz2 = fz1 * 2,
            fz3 = fz2 * 2,
            fz4 = fz3 * 2,
            fz5 = fz4 * 2,
            fz6 = fz5 * 2,
            fz7 = fz6 * 2,
            ord50 = fz7 * 2,
            fz8 = ord50,
            ord51 = fz8 * 2,
            fz9 = ord51,
            ord52 = fz9 * 2,
            fzc = ord52,
            ord53 = fzc * 2,
            fzl = ord53,
            ord54 = fzl * 2,
            fzz = ord54,
            ord55 = fzz * 2,
            fzm = ord55,
            ord56 = fzm * 2,
            fzy = ord56,
            ord57 = fzy * 2,
            fzs = ord57,
            ord58 = fzs * 2,
            fzb = ord58,
            ord59 = fzb * 2,
            fzj = ord59,
            ord60 = fzj * 2,
            fzg = ord60,
            ord61 = fzg * 2,
            fzq = ord61,
            ord62 = fzq * 2,
            fzp = ord62,
            ord63 = fzp * 2,
            fzv = ord63,
            #endregion

            unsigned = fzv * 2,
        }

        [Flags]
        public enum EnumFlag64 : long
        {
            signed = ~long.MaxValue,
            đ = eon | signed,

            undefined = ~zero,
            zero = 0,
            eon,
            one,
            two = (long)fx3,
            thre = (long)fx4,
            four = (long)fx5,
            five = (long)fx6,
            six = (long)fx7,
            seven = (long)fx8,
            eight = (long)fx9,
            nine = (long)fxc,
            ten = (long)fxl,
            eleven = (long)fxz,
            twelve = (long)fxm,
            thirdteen = (long)fxy,
            fourteen = (long)fxs,
            fifteen = (long)fxb,
            sixteen = (long)fxj,
            seventeen = (long)fxg,
            eighteen = (long)fxq,
            nineteen = (long)fxp,
            twenty = (long)fxv,
            //----------------------------
            twentyone = (long)fy1,

            //TODO set these below flags
            //twentytwo,
            //twentythre,
            //twentyfour = (long)fx3,
            //twentyfive = (long)fx4,
            //twentysix = (long)fx5,
            //twentyseven = (long)fx6,
            //twentyeight = (long)fx7,
            //twentynine = (long)fx8,

            //thirdty = (long)fx9,
            //thirdtyone = (long)fxv,
            //thirdtytwo,
            //thirdtythre,
            //thirdtyfour = (long)fx3,
            //thirdtyfive = (long)fx4,
            //thirdtysix = (long)fx5,
            //thirdtyseven = (long)fx6,
            //thirdtyeight = (long)fx7,
            //thirdtynine = (long)fx8,

            //fourty = (long)fx9,
            //fourtyone = (long)fxv,
            //fourtytwo,
            //fourtythre,
            //fourtyfour = (long)fx3,
            //fourtyfive = (long)fx4,
            //fourtysix = (long)fx5,
            //fourtyseven = (long)fx6,
            //fourtyeight = (long)fx7,
            //fourtynine = (long)fx8,


            fifty = (long)ord50,
            fiftyone = (long)ord51,
            fiftytwo = (long)ord52,
            fiftythre = (long)ord53,
            fiftyfour = (long)ord54,
            fiftyfive = (long)ord55,
            fiftysix = (long)ord57,
            fiftyseven = (long)ord58,
            fiftyeight = (long)ord59,
            fiftynine = (long)ord60,


            sixty = (long)ord61,
            sixtyone = (long)ord62,
            sixtytwo = (long)ord63,


        }

        public class Mathematical
        {
            private string \u1f51;

            //public async Task<int> DoSum(int a, int b)
            //{

            //    return await Sum(a, b);

            //}
            public Task<int> Sum(int a, int b)
            {
                // \u016e
                //var xString = $"\ufe23";

                System.Func<int> func = () => { return Sumarize(a: a, b: b); };

                return new Task<int>(func);

            }

            protected int Sumarize(int a, int b) => a + b;
            protected long Sumarize(long c, long d) => c + d;

            public Task<long> Sum(long c, long d)
            {
                System.Func<long> func = () => { return Sumarize(c: c, d: c); };

                return new Task<long>(func);

            }

        }

        #region public(s)
        #region methods.generic like actions
        public delegate Task<sub> exec<sub>();
        public delegate Task<sub> exec<sub, message>(message exp);
        public delegate Task<sub> exec<sub, message, arg>(message exp, params arg[] args) where arg : message;
        #endregion
        #region methods.generic like funcs
        delegate Task<demmand> eval<demmand>();// where demmand: System.Object;
        delegate Task<demmand> eval<demmand, message>(message exp);
        delegate Task<demmand> eval<demmand, message, arg>(message exp, params arg[] args) where arg : message;
        #endregion

        #region valuetypes like ??
        public struct flager
        {
            private EnumFlag64 feed;
            public EnumFlag64 display
            {
                get => feed;
                set => feed = value;
            }
        }

        public struct uflager
        {
            private EnumUFlag64 feed;
            public EnumUFlag64 display
            {
                get => feed;
                set => feed = value;
            }
        }
        #endregion

        #endregion

        #region internal(s)

        #endregion
        #endregion


        #region abstraction
        public static class encode<T> 
        {
        
        
        }
        #endregion

    }

}