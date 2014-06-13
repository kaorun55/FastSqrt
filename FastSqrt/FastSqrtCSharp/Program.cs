using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastSqrtCSharp
{
    class Program
    {
        static void Main( string[] args )
        {
            Stopwatch sw = new Stopwatch();

            int count = 1000000;

            var buffer = new double[count];

            sw.Restart();
            for ( int i = 0; i < count; i++ ) {
                buffer[i] = Math.Sqrt( (double)i );
            }

            var MathSqrt = sw.ElapsedMilliseconds;

            sw.Restart();
            for ( int i = 0; i < count; i++ ) {
                buffer[i] = t_sqrtF( (float)i );
            }

            var FastSqrtF = sw.ElapsedMilliseconds;

            sw.Restart();
            for ( int i = 0; i < count; i++ ) {
                buffer[i] = t_sqrtD( (double)i );
            }

            var FastSqrtD = sw.ElapsedMilliseconds;

            Console.WriteLine("");
            Console.WriteLine( "MathSqrt  : " + MathSqrt );
            Console.WriteLine( "FastSqrtF : " + FastSqrtF );
            Console.WriteLine( "FastSqrtD : " + FastSqrtD );
        }

        static unsafe float t_sqrtF( float x )
        {
　　        float xHalf = 0.5f * x;
　　        int   tmp   = 0x5F3759DF - ( *(int*)&x >> 1 ); //initial guess
　　        float xRes  = *(float*)&tmp;

　　        xRes *= ( 1.5f - ( xHalf * xRes * xRes ) );
　　        //xRes *= ( 1.5f - ( xHalf * xRes * xRes ) );//コメントアウトを外すと精度が上がる
　　        return xRes * x;
        }

        static unsafe double t_sqrtD( double x) 
        {
　　        double         xHalf = 0.5 * x;
　　        long  tmp   = 0x5FE6EB50C7B537AAL - ( *(long*)&x >> 1);//initial guess
　　        double         xRes  = * (double*)&tmp;

　　        xRes *= ( 1.5 - ( xHalf * xRes * xRes ) );
　　        //xRes *= ( 1.5 - ( xHalf * xRes * xRes ) );//コメントアウトを外すと精度が上がる
　　        return xRes * x;
        }
    }
}
