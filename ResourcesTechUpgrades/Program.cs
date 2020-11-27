using System;
using System.Collections.Generic;
using System.Linq;

namespace ResourcesTechUpgrades
{
    class Combo
    {
        public Combo
        (
            Double iT1 = 0,
            Double iT2 = 0,
            Double iT3 = 0,
            Double iT4 = 0
        )
        {
            _t[0] = iT1;
            _t[1] = iT2;
            _t[2] = iT3;
            _t[3] = iT4;
        }

        public int t
        (
            int iSub
        )
        {
             return Convert.ToInt32(_t[iSub - 1]);
        }

        public Double upgrade_factor
        (
        )
        {
            return  Math.Pow(1.01, _t[0]) *
                    Math.Pow(1.02, _t[1]) *
                    Math.Pow(1.03, _t[2]) *
                    Math.Pow(1.05, _t[3]);
        }

        public void display
        (
            String lable = ""
        )
        {
            Console.Write(lable + " ");
            for (int z = 0; z < 4; z++)
            {
                Console.Write("T{0}:{1,-3} ",(z + 1), _t[z]);
            }
            Console.WriteLine();
        }

        Double[] _t = new Double[4];
    }

    class ComboContainer
    {

        public ComboContainer
        (
            String iName = "jan"
        )
        {
            _name = iName;
        }

        public void add_to_container
        (
            Combo iCombo
        )
        {
            _combos.Add(iCombo);
        }

        public void display
        (
        )
        {
            Console.WriteLine("\n" + _name + "\n");

            if (_combos.Count > 0)
            {
                Console.Write("{0,3}) ", 1);
                _combos[0].display();
                
                int oldValue = _combos[0].t(4);

                for (int z = 1; z < _combos.Count; z++)
                {
                    int newValue = _combos[z].t(4);
                    if (oldValue == newValue)
                    {
                    }
                    else
                    {
                        Console.WriteLine("--------------------------------");
                        oldValue = newValue;
                    }

                    Console.Write("{0,3}) ", z + 1);
                    _combos[z].display();
                }
            }
            else
            {
                Console.WriteLine("There are no solutions");
            }
        }

        public void sort
        (
        )
        {
            // Mismatch.  Knowledge on the order belongs in combo
            _combos = _combos.OrderBy(o => o.t(4))
                             .ThenBy (o => o.t(3))
                             .ThenBy (o => o.t(2))
                             .ThenBy (o => o.t(1))
                             .ToList();

        }

        private List<Combo> _combos = new List<Combo>();
        private String _name;
    }


    class Program
    {
        private static Double initialMineMultiplier = 0.0;

        private static Combo availableTechKits = new Combo();

        private static ComboContainer possibleCombos = new ComboContainer("Possible Combos");

        private static ComboContainer solutions = new ComboContainer("Solutions");

        static void display_solutions
        (
            ComboContainer iCC
        )
        {
            iCC.display();
        }


        static void find_solutions
        (
        )
        {
        }

        static void load_combos
        (
        )
        {
            Console.WriteLine("Building Upgrade Combinations");

            int maxT1 = Math.Min(163, availableTechKits.t(1));
            int maxT2 = Math.Min( 83, availableTechKits.t(2));
            int maxT3 = Math.Min( 57, availableTechKits.t(3));
            int maxT4 = Math.Min( 23, availableTechKits.t(4));

            for (int t4 = 0; t4 <= maxT4; t4++)
            {
                Console.WriteLine("Building combinations with T4 = " + t4);

                for (int t3 = 0; t3 <= maxT3; t3++)
                {
                    for (int t2 = 0; t2 <= maxT2; t2++)
                    {
                        for (int t1 = 0; t1 <= maxT1; t1++)
                        {
                            Combo tmp = new Combo(t1, t2, t3, t4);

                            Double upgradeFactor = tmp.upgrade_factor();

                            if (upgradeFactor >= 5.045 && upgradeFactor <= 5.05)
                            {
                                possibleCombos.add_to_container(tmp);
                            }
                            else
                            {
                            }
                        }
                    }
                }
            }
        }

        static void get_arguments
        (
            string[] iArgV
        )
        {
            Double[] t = new Double[4];

            if (iArgV.Length > 0)
            {
                if (iArgV.Length >= 4)
                {
                    for (int z = 0; z < 4; z++)
                    {
                        t[z] = Convert.ToDouble(iArgV[z]);
                    }

                    availableTechKits = new Combo(t[0], t[1], t[2], t[3]);
                    availableTechKits.display("Available Tech");

                    if (iArgV.Length == 5)
                    {
                        initialMineMultiplier = Convert.ToDouble(iArgV[4]);
                        Console.WriteLine("Initial Mine Multiplier " + initialMineMultiplier + "\n");
                    }
                    else
                    {
                    }
                }
                else
                {
                    throw new Exception("Not enough arguments");
                }
            }
            else
            {
                throw new Exception ("No arguments supplied");
            }
        }

        static void Main
        (
            string[] args
        )
        {
            try
            {
                get_arguments(args);
                load_combos();
                if (initialMineMultiplier == 0.0)
                {
                    // The solution is simply the list of combos
                }
                else
                {
                    find_solutions();
                }

                display_solutions(possibleCombos);
            }
            catch (Exception e)
            {
                Console.WriteLine("Caught Exception " + e.Message);
            }
        }
    }
}

