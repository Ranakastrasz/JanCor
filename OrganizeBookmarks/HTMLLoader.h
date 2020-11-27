
#pragma once;

#include <iostream>
#include <fstream>
#include <streambuf>
#include <string>
#include <vector>

class Bookmark
{
    public :

        Bookmark
        (
            void
        ) : 
            _href(),
            _addDate(),
            _icon()
        {
        }

        ~Bookmark
        (
            void
        )
        {
        }

    protected :

        std::string _href;
        std::string _addDate;
        std::string _icon;
};


class HTMLLoader
{
    public :

        HTMLLoader
        (
        ) :
            _text()
        {
        }

        ~HTMLLoader
        (
        )
        {
        }

        void load
        (
            const std::string &iFileName
        )
        {
            load_file(iFileName);
            extract_bookmarks();
        }

        friend std::ostream &operator<<
        (
                  std::ostream &out,
            const HTMLLoader   &that
        )
        {
            std::cout << that. _text << std::endl;

            return out;
        }

    protected :

        void load_file
        (
            const std::string &iFileName
        )
        {
            std::ifstream t(iFileName.c_str());

            std::string str((std::istreambuf_iterator<char>(t)),
                             std::istreambuf_iterator<char>());
            _text = str;
        }

        void extract_bookmarks
        (
            void
        )
        {
            std::string lookFor = "<A HREF";

            std::vector<size_t> positions;

            size_t pos = _text.find(lookFor, 0);
            while(pos != std::string::npos)
            {
                positions.push_back(pos);
                pos = _text.find(lookFor,pos+1);
            }
        }

        std::string _text;
        std::vector<Bookmark> _bookmarks;
};

