#include <stdio.h>


int all_positive(int a[], unsigned int alen)
{
    for (int i = 0; i < alen; i++)
    {
        if (a[i] < 0)
        {
            return 0;
            break;
        }
        else if (i == alen - 1)
        {
            return 1;
        }
    }
}

int all_positive1(int a[], unsigned int alen)
{
    if (alen > 0)
    {
        for (int i = 0; i <= alen - 1; i++)
        {
            if (a[i] < 0)
            {
                return 0;
                break;
            }
            else
            {
                /* Since this is positive we can just keep looking */
            }
        }
    }
    else
    {
        /* Empty arrays imply all are positive */
    }

    return 1;
}


int main()
{
    int a[] = { 1, -1 }; printf("%d\n", all_positive(a, 2));
                         printf("%d\n", all_positive1(a, 2));

    return 0;

}