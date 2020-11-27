/*
    consider good exception
    consider find
    consider iterator class.
    consider automatically invalidating iterators
    consider move constructor

    no thread safety is required
*/

#include <exception>
#include <cassert>

// First off, Build as a template for reusability and type safety.
template<typename T>
class JansList
{
    public :

        // Always have a base exception derivved from Exception so you can generate good error messages
        class JansNodeException : public std::exception
        {
            char const *what
            (
            ) const
            {
                return "not implemented";
            }
        };


    protected:

        // This class let's us access individual nodes at the linked list level.
        // They also carry the iterator functionality.  As a result, the act
        // of deleting a node may invalid another node.  Don't do that.
        class JansNode
        {
            public :

                JansNode
                (
                    T iValue
                ) :
                    _next(NULL)
                {
                    _value = iValue;
                }

                JansNode *next
                (
                )
                {
                    JansNodeException jne;
                    throw jne;
                    return NULL;
                }

                JansNode *remove
                (
                )
                {
                    JansNodeException jne;
                    throw jne;
                }

                JansNode *append
                (
                    JansNode *iNewNode
                )
                {
                    this->_next = iNewNode;

                    return iNewNode;
                }

                T &value
                (
                )
                {
                    JansNodeException jne;
                    throw jne;
                    return T();
                }

                const T &value
                (
                ) const
                {
                    JansNodeException jne;
                    throw jne;
                    return T();
                }

        private :

            // This is the data that the node carries
            T         _value;

            // This is the next node in the list, or NULL
            JansNode *_next;
        };

        // This implementation carries a head and tail to allow for faster
        // appends
        JansNode *_head;
        JansNode *_tail;


        void swap
        (
            JansNode &lhs, JansNode &rhs
        )
        {
            // Consider what happens if they are the same object

            JansNode *head = lhs._head;
            JansNode *tail = lhs._tail;

            lhs._head = rhs._head;
            lhs._tail = rhs._tail;

            rhs._head = head;
            rhs._tail = tail;
        }


    public :

        // Standard constructor
        JansList
        (
        ) :
            _head(NULL),
            _tail(NULL)
        {
        }

        // Standard copy constructor
        JansList
        (
            const JansList &that
        )
        {
            JansNodeException jne;
            throw jne;
        }

        // Standard assignment operator
        const JansList operator=
        (
            const JansList &rhs
        )
        {
            // Create a copy of the right hand side
            JansList tmp(rhs);

            // Swap the guts of this object with the copy
            tmp.swap(*this);
            // let the temp object go out of scope

            return rhs;
        }


        // Returns true, if there are no elements in the list
        // Returns false, if there is at least one element in the list
        bool is_empty
        (
        )
        {
            JansNodeException jne;
            throw jne;
            return true;
        }

        // Returns a pointer to the actual head
        JansNode *head
        (
        )
        {
            JansNodeException jne;
            throw jne;
            return _head;
        }

        // Returns a pointer tot he actual tail
        JansNode *tail
        (
        )
        {
            JansNodeException jne;
            throw jne;
            return _tail;
        }

        // Adds a new node to the end of the list,
        // and initializes it with the Value
        // returns the new tail
        JansNode *append
        (
            T& iValue
        )
        {
            if (_head == NULL)
            {
                _head =
                _tail = new JansNode(iValue);
            }
            else
            {
                assert(_head != NULL);
                assert(_tail != NULL);
                _tail->append(new JansNode(iValue));

            }

            return _tail;
        }

        // Removes all nodes from the list
        void clear
        (
        )
        {
            JansNodeException jne;
            throw jne;
        }
};