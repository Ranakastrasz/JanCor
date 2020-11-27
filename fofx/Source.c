/*                                                                     
    fofx.c                                                             
                                                                       
    EvilTeach                                                          
                                                                       
    04/10/2017                                                         
                                                                       
    A utility for estimating a polynomial out of a set of data points  
                                                                     */

#include <stdio.h>      /* printf */
#include <string.h>     /* memset */
#include <stdlib.h>     /* atof */

#define kN 7

double matrix[kN][kN];
double derivative = 0.0;
int    degree     = 0;

const char *ordinal
(
    int iInt
)
{
    switch (iInt)
    {
        case 1  : return "st";
        case 2  : return "ed";
        case 3  : return "rd";
        default : return "th";
    };
}

void zero_matrix
(
    void
)
{
    memset(matrix, '\0', sizeof matrix);
}

void show_matrix
(
    char *iTitle
)
{
    printf("\n%s\n\n", iTitle);

    for (int z = 0; z < kN; z++)
    {
        printf("%8d ", z);
    }
    printf("\n");

    printf("---");
    for (int z = 0; z < kN; z++)
    {
        printf("---------");
    }
    printf("\n");

    for (int z = 0; z < kN; z++)
    {
        printf("%3d ", z);

        for (int y = 0; y < kN; y++)
        {
            printf("%8.3lf ", matrix[z][y]);
        }

        printf("\n");
    }

    printf("\n");
}



int estimate_derivative
(
    int          iArgC,
    const char **iArgV
)
{
    int returnStatus = 0;

    zero_matrix();

    int columns = iArgC - 1;

    for (int z = 1; z < iArgC; z++)
    {
        matrix[0][z - 1] = atof(iArgV[z]);
    }

    for (int z = 0; z < columns; z++)
    {
        /* Make row z + 1 the differences of row z */

        for (int y = 0; y < columns - z - 1; y++)
        {
            matrix[z + 1][y] = matrix[z][y + 1] - matrix[z][y];
        }
    }

    /*                                                                      
        Alrighty then.                                                      
                                                                            
        In a perfect world one of the rows of the matrix has values which   
        are all the same.  Any value from that row is an estimate of the    
        Nth derivative.  Let's find it.  If we can't then there is no nice  
        polynomial type fit.                                                
                                                                          */

    show_matrix("Differences");

    int z;
    for (z = 1; z < columns; z++)
    {
        derivative = matrix[z][0];
        int y;
        for (y = 1; y < columns - z; y++)
        {
            // Ya ya.  Floating point compares 'n all that.

            if (matrix[z][y] == derivative)
            {
            }
            else
            {
                break;
            }
        }

        if (y != columns - z)
        {
            /* It's not this row */
        }
        else
        {
            break;
        }
    }

    if (z != columns)
    {
        printf("The %d%s derivative is %8.3f\n", z, ordinal(z), matrix[z][0]);
        degree = z;
    }
    else
    {
        show_matrix("Failed to find Nth derivative");
        returnStatus = 1;
    }

    return returnStatus;
}



int integrate_to_equation
(
    void
)
{
    int returnStatus = 0;

    zero_matrix();

    matrix[0][0] = derivative;

    show_matrix("Ready to integrate");

    printf("Degree %d\n", degree);

    for (int z = 1; z <= degree; z++)
    {
        for (int y = 0; y < z; y++)
        {
            matrix[z][y] = matrix[z - 1][y];
            matrix[z][y] /= z - y;
        }

        matrix[z][z] = 1.0;
    }

    show_matrix("After Integration");

    return returnStatus;
}



int estimate_coefficients
(
    char **iArgV
)
{
    int returnStatus = 0;

    for (int z = degree - 1; z >= 0; z--)
    {
        for (int y = 0; y <= degree; y++)
        {
            matrix[z][y] = matrix[z + 1][y];
        }
    }

    for (int z = 0; z <= degree; z++)
    {
        double v = atof(iArgV[z + 1]);
        matrix[z][degree + 1] = v;
    }

    show_matrix("Prep for gauss jordan");

    return returnStatus;
}



int display_results
(
    void
)
{
    int returnStatus = 0;

    return returnStatus;
}



int main            /* returns 0 on sucess, and 1 on failure */
(
    int    iArgC,
    char **iArgV
)
{
    int returnStatus = 1;

    if (iArgC > 1)
    {
        if (iArgC <= kN)
        {
                                   returnStatus = estimate_derivative(iArgC, iArgV);
            if (returnStatus == 0) returnStatus = integrate_to_equation();
            if (returnStatus == 0) returnStatus = estimate_coefficients(iArgV);
            if (returnStatus == 0) returnStatus = display_results();
        }
        else
        {
            printf("The program is hardcoded to accept up to %d numbers on the command line\n", kN);
        }
    }
    else
    {
        printf("Try fofx 0 1 4 9 16\n");
    }

    return returnStatus;
}