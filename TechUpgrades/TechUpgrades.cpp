
//
//  TechUpgrades.cpp
//
//  EvilTeach && Arcanocromatic
//
//  12/16/2017
//
//  This is a silly program to aid in deciding how to spend tech upgrades.
//


#include <iostream>     /* cout     */
#include <iomanip>      /* setw...  */
#include <vector>       /* vector   */
#include <algorithm>    /* min      */
#include <math.h>       /* modf     */

const char *license = "MPL 1.0";

/*                                                                      
    This class holds one group of tech, along with the methods needed   
    to manipulate it.                                                   
                                                                      */
class Combo
{
    public :

        Combo
        (
            int iT1 = 0,
            int iT2 = 0,
            int iT3 = 0,
            int iT4 = 0
        )
        {
            _t[0] = iT1;
            _t[1] = iT2;
            _t[2] = iT3;
            _t[3] = iT4;
        }


        // All good Combos can display themselves
        friend std::ostream &operator<<
        (
            std::ostream &out,
            const Combo &that
        )
        {
            for (size_t z = 0; z < 4; z++)
            {
                out <<"T" << z + 1 << ":" << that._t[z] << " ";
            }

            out << std::endl;

            return out;
        }


        // Applying the tech yields this upgrade factor
        double upgrade_factor
        (
            void
        )
        {
            return  pow(1.01, _t[0]) *
                    pow(1.02, _t[1]) *
                    pow(1.03, _t[2]) *
                    pow(1.05, _t[3]);
        }


        // Remove tech from this combo based on another combo
        const Combo &operator-=
        (
            const Combo &that
        )
        {
            for (size_t z = 0; z < 4; z++)
            {
                _t[z] -= that._t[z];
            }

            return that;
        }


        // is this < that?
        bool operator<
        (
            const Combo &that
        )
        {
            for (int z = 3; z >= 0; z--)
            {
                int sum = _t[z] - that._t[z];
                if (sum < 0) return true;   // is less than
                if (sum > 0) return false;  // is greater than
            }

            // These are equal combos
            return false;
        }


        // Combos are equal if they have the same exact tech
        bool operator==
        (
            Combo &that
        )
        {
            return !( (*this < that) || (that < *this) );
        }


        /*
            BUG Corwin comment this. :)
        */
        double my_score
        (
            const Combo &iCombo
        )
        {
            double score = std::numeric_limits<double>::max();

            for (size_t z = 0; z < 4; z++)
            {
                 score = std::min(score, (double)(iCombo._t[z]) / (double)(_t[z]));
            }

            return score;
        }


        /*                                               
            Eliminate unreasonable command line values   
                                                       */
        void reject_bad_command_line_values
        (
            void
        )
        {
            for (size_t z = 0; z < 4; z++)
            {
                if (_t[z] < 0)
                {
                    std::cout << "<" << _t[z] << ">" << std::endl;
                    throw std::exception("command line arguments are not allowed to have negative values");
                }
                else
                {
                }
            }
        }

        int t(int sub)
        {
            return _t[sub];
        }

    protected :

        int _t[4];
};



// This holds a vector of combos, and methods to manipulate them
class ComboContainer
{
    public :

        /*                                                                     
            This function iterates the combos applying a function to each one. 
            The iCombo and oCombo are passed to this function.  The oCombo may 
            or may not be updated.                                             
                                                                               
            iterate returns a zero when all of the combos have been processed  
            if the loop terminates early, then it returns non-zero, and it     
            returns an updated oCombo.                                         
                                                                             */
        bool find_best_solution
        (
            const Combo   &iTechKit,
                  Combo   &oSolution
        )
        {
            bool returnStatus = true;

            Combo bestSolution;
            double bestScore = 0;

            for (size_t z = 0; z < _combos.size(); z++)
            {
                double newScore = _combos[z].my_score(iTechKit);
                if (newScore > bestScore)
                {
                    bestScore = newScore;
                    bestSolution = _combos[z];
                }
                else
                {
                    // Did not find a better solution.
                }
            }
            if (bestScore >= 1)
            {
                // Valid solution to return
                oSolution = bestSolution;
            }
            else
            {
                //No valid solutions found at all.
                returnStatus = false;
            }

            return returnStatus;
        }


        /*                                 
            Add a Combo to the Container   
                                         */
        friend Combo &operator+=
        (
            ComboContainer &iCC,
            Combo          &iC
        )
        {
            iCC._combos.push_back(iC);

            return iC;
        }


        /*                                           
            Display the container for debugging      
                                                   */
        friend std::ostream &operator<<
        (
            std::ostream &out,
            const ComboContainer &that
        )
        {
            for (size_t z = 0; z < that._combos.size(); z++)
            {
                const Combo &tmp = that._combos[z];

                out << z << ") " << tmp;
            }

            return out;
        }


        /*                                                            
            Used to show solutions, in a nicer, easier to use form.   
                                                                    */
        void show_summary
        (
            void
        )
        {
            std::cout << "Solutions\n";

            if (_combos.size() > 0)
            {
                std::sort(_combos.begin(), _combos.end());

                int count = 1;
                Combo oldCombo = _combos[0];
                for (size_t z = 1; z < _combos.size(); z++)
                {
                    Combo &newCombo = _combos[z];
                    if (oldCombo == newCombo)
                    {
                        count++;
                    }
                    else
                    {
                        std::cout << count << " X " << oldCombo << std::endl;
                        oldCombo = newCombo;
                        count = 1;
                    }
                }

                std::cout << count << " X " << oldCombo << std::endl;
            }
            else
            {
                std::cout << "Sorry. No solutions were found." << std::endl;
            }
        }

    protected :

        std::vector<Combo> _combos;
};



/*                                                                
    Holds the actual number of tech that you have for upgrades    
                                                                */
Combo gTechKits;

/*                                                                           
    Holds the list of tech upgrades that yield the upgrade factor desired    
                                                                           */
ComboContainer gValidCombos;

/*                                                                     
    Holds the list of tech upgrades that can be done, based on the     
    tech you have for upgrades                                         
                                                                     */
ComboContainer gSolutions;



/*                                                                     
    This populates the gValidCombos list based on the upgrade limits.  
    The default limits yield a 5.05 upgrade.                           
                                                                     */
void generate_valid_combos
(
    double iLowLimit,
    double iHighLimit,
    Combo  iCommandLine
)
{
    std::cout << "Building Upgrade Combinations" << std::endl;

    int t1Limit = 200;    int t1 = 0;     // FUTURE these are not calculated based on the limits
    int t2Limit = 200;    int t2 = 0;     // They are based on the default values
    int t3Limit = 200;    int t3 = 0;
    int t4Limit =  24;    int t4 = 0;

    if (iCommandLine.t(0) < t1Limit) t1Limit = iCommandLine.t(0);
    if (iCommandLine.t(1) < t2Limit) t2Limit = iCommandLine.t(1);
    if (iCommandLine.t(2) < t3Limit) t3Limit = iCommandLine.t(2);
    if (iCommandLine.t(3) < t4Limit) t4Limit = iCommandLine.t(3);

    for (t4 = 0; t4 < t4Limit; t4++)
    {
        std::cout << "Building combinations with T4 = " << t4 << std::endl;

        for (t3 = 0; t3 < t3Limit; t3++)
        {
            for (t2 = 0; t2 < t2Limit; t2++)
            {
                for (t1 = 0; t1 < t1Limit; t1++)
                {
                    Combo tmp(t1, t2, t3, t4);
                    double upgradeFactor = tmp.upgrade_factor();

                    if (upgradeFactor <= iHighLimit && upgradeFactor >= iLowLimit)
                    {
                        gValidCombos += tmp;
                    }
                }
            }
        }
    }
}



/*                                                                      
    Based on your tech kits, try to find a list of Combos to use        
    to get maximum return.  It returns all found solutions              
    in gSolutions                                                       
                                                                      */
void find_all_solutions
(
    void
)
{
    Combo aSolution;

    while (gValidCombos.find_best_solution(gTechKits, aSolution))
    {
        gSolutions += aSolution;
        gTechKits  -= aSolution;
    }
}




/*                                           
    The entry point.                         
                                             
    Arguments #tech1 #tech2 #tech3 #tech4    
                                           */
int main
(
          int    argC,
    const char **argV
)
{
    int returnStatus = 0;

    std::cout << "This program is licensed under " << license << std::endl;
    try
    {
        if (argC < 5)
        {
            throw std::exception("Put your counts T1 T2 T3 T4 on the command line.");
        }
        else
        {
        }

        Combo commandLineCombo
        (
            atoi(argV[1]),
            atoi(argV[2]),
            atoi(argV[3]),
            atoi(argV[4])
        );

        std::cout << "\nTech Kits Available: " << commandLineCombo << std::endl;

        commandLineCombo.reject_bad_command_line_values();

        gTechKits = commandLineCombo;

        generate_valid_combos(5.049500, 5.050000, commandLineCombo);  // FUTURE would anyone want different values?

        find_all_solutions();

        gSolutions.show_summary();

        std::cout << "Remaining TechKits\n" << gTechKits << std::endl;
        //std::cout << "Remaining Techkits Upgrade Factor\n" << gTechKits.upgrade_factor() << std::endl;
    }
    catch(const std::exception &that)
    {
        std::cerr << "Caught Exception: " << that.what() << std::endl;
        returnStatus = 1;
    }

    return returnStatus;
}
