#define _CRT_SECURE_NO_WARNINGS

#pragma warning (disable: 4996)

#include <cstring>

#include <errno.h>
#include <iostream>
#include <io.h>
#include <time.h>
#include <vcruntime.h>
#include <exception>
#include <direct.h>

#include <fstream>
using namespace std;


bool valid_line
(
    char *firstLine
)
{
    char *p = firstLine;
    while (*p)
    {
        if (isascii(*p) && isalnum(*p))
        {
            return true;
        }
        else
        {
        
        }
        p++;
    }
    return false;
}

void format_line
(
    char *firstLine
)
{
    // find "words"
    // Concatinate each "Word" onto new string, with underscore between them
    // Limit to 10 words, break if you exceed

    char *p = firstLine;
    char *q = firstLine;
    bool wordBreak = false;
    bool wordOccurred = false;
    
    //cout << "<" << firstLine << ">" << endl;
    int wordCount = 0;
    while (*p && wordCount < 10)
    {
        if (isascii(*p) && isalnum(*p))
        {
            if (wordBreak)
            {
                cout << '_';
                *q = '_';

                wordBreak = false;
                q++;
            }
            else
            {
                // Inside a word, no underscore for you
            }

            cout << *p;
            *q = *p;
            wordOccurred = true;
            q++;


        }
        else
        {
            if (wordOccurred && ! wordBreak)
            {
                wordBreak = true;
                wordCount++;
            }
            else
            {
                // We will not place an underscore until the first word occurs.
            }
        }

        p++;
    }

    *q = '\0';
    //strcat(q, ".txt");


    cout << endl;
    cout << "firstLine:<"<< firstLine <<">" << endl << endl;

}

int main()
{
    char *path = "fastnote/quotes/";


    char *inBase = "C:/JanCor/FileRename/DataIn/";
    char *outBase = "C:/JanCor/FileRename/DataOut/";



    char inPath[500];
    char outPath[500];
    
    strcpy(inPath, inBase);
    strcat(inPath, path);
    strcpy(outPath, outBase);
    strcat(outPath, path);

    char fileSearchPath[500];
    strcpy(fileSearchPath, inPath);
    strcat(fileSearchPath, "*.txt");
    
    mkdir(outPath);

    struct _finddata_t data;
    intptr_t handle;
    int done = -1;
    int count = 0;

    try
    {
        //  Iterate input files
        handle = _findfirst(fileSearchPath, &data);
        if (handle != -1L)
        {
            do
            {
                count++;

                char inputFileName [1000];
                strcpy(inputFileName, inPath);
                strcat(inputFileName, data.name);

                //      load 1 file
                fstream inFile (inputFileName, ios::in);
                if (inFile)
                {
                    cout << "\"" << inputFileName << "\"" << endl;

                    char inLine[500] = {0};

                    while(inFile.getline(inLine, 500))
                    {
                        if (valid_line(inLine))
                        {
                            cout << "<" << inLine << ">" << endl;
                            break;
                        }
                        else
                        {
                            // Line is invalid, try again
                        }
                    }
                    //      validate and reformat line

                    format_line(inLine);


                    //      save file
                    


                    char outputFileName [1000];
                    char countStr [1000];
                    itoa(count,countStr,10);
                    

                    strcpy(outputFileName, outPath); // Output Folder
                    strcat(outputFileName, inLine); // First Line Contents, Reformatted.
                    strcat(outputFileName,"__"); // Spacing between number and text.
                    strcat(outputFileName,countStr); // File Number ID to avoid collision.
                    strcat(outputFileName,".txt"); // File Type.
                    fstream outFile (outputFileName, ios::out);

                    inFile.clear();
                    inFile.seekg(0);
                    


                    outFile << inFile.rdbuf();

                    done = _findnext(handle, &data);

                    //  display statistics on # file such

                    //  indicate if the whole process was successful
                }
                else
                {
                    perror("failed to open file");
                }
            }
            while(done != -1);
        }
        else
        {
            cout << "Can't find data" << endl;
        }

        _findclose(handle);

        cout << "We found " << count << " files" << endl;

    }
    catch(exception &e)
    {
        cout << e.what() << endl;
    }
}
