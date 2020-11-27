
#include <iostream>

#include "JansList.hpp"

int main(void)
{
    int returnStatus = 0;

    try
    {
        JansList<int> list1;
        JansList<int> List2;

        for (int z = 0; z < 3; z++)
        {
            list1.append(z);
        }
    }
    catch (std::exception &e)
    {
        std::cout << "Exception: " << e.what() << std::endl;

        returnStatus = 1;
    }
    catch (...)
    {
        std::cout << "Unknown Exception: " << std::endl;
        returnStatus = 1;
    }

    return returnStatus;
}