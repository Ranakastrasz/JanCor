
//  Corwin - some magic

#include <iostream>

int sqrti(int x)
{
    union { float f; int x; } v; 

    v.f = (float)x;

    v.x  -= 1 << 23;
    v.x >>= 1;      
    v.x  += 1 << 29;

    return (int) v.f;
}

int main
(
    void
)
{
    for (int z = 0; z < 100; z++)
    {
        std::cout << z << " " << sqrti(z) << std::endl;
    }

    return 0;
}