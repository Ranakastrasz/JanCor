#include <iostream>
#include <fstream>
#include <string>
#include <exception>

#include "HTMLLoader.h"


const std::string inputFileName  = "C:/JanCor/OrganizeBookmarks/input_bookmarks.html";
const std::string outputFileName = "C:/JanCor/OrganizeBookmarks/output_bookmarks.html";

int main
(
    void
)
{
    int returnStatus = 1;

    try
    {
        HTMLLoader htmlLoader;

        htmlLoader.load(inputFileName);

        std::cout << htmlLoader << std::endl;

        returnStatus = 0;
    }
    catch (std::exception &e)
    {
        std::cout << "exception " << e.what() << std::endl;
    }
    catch (...)
    {
        std::cout << "unknown exception" << std::endl;
    }

    return returnStatus;
}