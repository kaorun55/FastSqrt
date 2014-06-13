// FastSqrtCpp.cpp : コンソール アプリケーションのエントリ ポイントを定義します。
//

#include "stdafx.h"

#include <Windows.h>
#include <iostream>
#include <vector>

#include <mmsystem.h>
#pragma comment(lib, "winmm.lib")

inline float t_sqrtF( const float& x )
{
    float xHalf = 0.5f * x;
    int   tmp = 0x5F3759DF - (*(int*)&x >> 1); //initial guess
    float xRes = *(float*)&tmp;

    xRes *= (1.5f - (xHalf * xRes * xRes));
    //xRes *= ( 1.5f - ( xHalf * xRes * xRes ) );//コメントアウトを外すと精度が上がる
    return xRes * x;
}

inline double t_sqrtD( const double &x )
{
    double         xHalf = 0.5 * x;
    long long int  tmp = 0x5FE6EB50C7B537AAl - (*(long long int*)&x >> 1);//initial guess
    double         xRes = *(double*)&tmp;

    xRes *= (1.5 - (xHalf * xRes * xRes));
    //xRes *= ( 1.5 - ( xHalf * xRes * xRes ) );//コメントアウトを外すと精度が上がる
    return xRes * x;
}

int _tmain(int argc, _TCHAR* argv[])
{
    int count = 10000000;
    std::vector<double> buffer( count );

    int _new = ::timeGetTime();
    for ( int i = 0; i < count; i++ ) {
        buffer[i] = sqrt( (float)i );
    }

    auto MathSqrt = ::timeGetTime() - _new;

    _new = ::timeGetTime();
    for ( int i = 0; i < count; i++ ) {
        buffer[i] = t_sqrtF( (float)i );
    }

    auto FastSqrtF = ::timeGetTime() - _new;

    _new = ::timeGetTime();
    for ( int i = 0; i < count; i++ ) {
        buffer[i] = t_sqrtD( (double)i );
    }

    auto FastSqrtD = ::timeGetTime() - _new;

    std::cout << "MathSqrt  : " << MathSqrt << std::endl;
    std::cout << "FastSqrtF : " << FastSqrtF << std::endl;
    std::cout << "FastSqrtD : " << FastSqrtD << std::endl;

    return 0;
}

