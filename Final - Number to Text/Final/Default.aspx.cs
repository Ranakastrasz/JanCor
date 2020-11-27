using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class _Default : System.Web.UI.Page
{

    // Seed Strings.
    // Used to correspond a number to the text associated with it, depending on where it is. Thosand places are specifically the 1000^x values.
    // Teens are included because Urgh....
    // And the blank ones are places where nothing exists even if you wanted to draw it.
    string hundred = "Hundred";
    string[] thousandsPlace = { "", "Thousand", "Million","Billion","Trillion"};
    string[] numbers = { "Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine" };
    string[] teens = { "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Ninteen" };
    string[] tens = { "", "Ten", "Twenty", "Thirty", "Fourty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };

    // Decompose the number into each place. 1, 10, 100, 1000, etc
    // Initially, print out "numbers" of each one.
    // Add on Hundred and Thousand Places
    // Add on Tens places
    // Add on Teens.

    // 1 places use numbers
    // 10 places uses tens or Teens. if teens, exclude 1s place.
    // 100 places use number followed by hundred.
    // 1000 places use number followed by thounsand (or million, billion, trillion as scale up)

    // English is really, really horrible.
    // Teens should not exist as such. Also are formed such that base 12 was clearly intended. after 12, it changes to "number Derived - Teen"
    // And seven should be sev, so it is one syllable.
    // Repeat every 3 cycles. 1s place.
    
    protected string parseNumber(string number)
    {
        string ostring = "";

        if (number == "0")
        {
            // If Zero, return zero.
            ostring = numbers[0];
        }
        else
        {
            int[] digits = new int[13];
            // Else tokenize into digits
            // zero is 1s place, 1 is 10s place, 2 is 100s place, etc.
            for (int x = 0; x < 13; x++)
            {
                if (number.Length == 0)
                    break;
                digits[x] = Convert.ToInt32(number.Substring(number.Length-1));
                number = number.Substring(0, number.Length - 1);
            }
            
            // Because formatting is wierd, just use a bunch of flags.
            // There is no way there is NOT a better way to do this, but eh.

            // needThousands says whether or not to write the thousand/million/billion/trillion places when you finish the ones place.
            // It is excluded if the previous hundred, tens, and ones place were all zeros.
            bool needthousands = false;

            // needAnd says to write an "And" after a hundreds place.
            bool needAnd = false;
            
            // Since Teens are weird, this says to, instead of writing the tens place and the ones place, instead draw a custom teens value.
            // Ten through nineteen are considered Teens. So it occurs if the Tens place is 1
            bool isTeen = false;

            // After a thousand, just before something else, you put a comma. If it ends after that thousand (rest are zeros) draw nothing.
            bool needComma = false;
            
            // Between tens and Ones place you need a space. The rest of the cases already have seperators, but this one is also needed.
            bool needSpace = false;

            // 13 places.
            for (int x = 12; x >= 0; x--)
            {
                // Cycle through ones, hundreds, and tens place till you hit the last ones place.
                int mode = x % 3;
                

                if (mode == 0)
                {// Thousands place
                    if (isTeen)
                    {
                        if (needComma)
                        {
                            ostring += ", ";
                            needComma = false;
                        }

                        isTeen = false;
                        ostring += teens[digits[x]];
                        needthousands = true;
                    }
                    else if (digits[x] != 0)
                    {
                        if (needComma)
                        {
                            ostring += ", ";
                            needComma = false;
                        }
                        if (needSpace)
                        {
                            ostring += " ";
                            needSpace = false;
                        }
                        if (needAnd)
                        {
                            ostring += " and ";
                            needAnd = false;
                        }
                        ostring += numbers[digits[x]];
                        needthousands = true;
                    }
                    if (needthousands )
                    {
                        ostring += " " + thousandsPlace[x / 3];
                        needthousands = false;
                        needComma = true;
                    }
                }
                else if (mode == 2)
                {// Hundreds Place
                    if (digits[x] != 0)
                    {
                        if (needComma)
                        {
                            ostring += ", ";
                            needComma = false;
                        }
                        ostring += numbers[digits[x]] + " " + hundred;
                        needAnd = true;
                        needthousands = true;
                    }
                }
                else
                {// Tens Place
                    if (digits[x] != 0)
                    {
                        if (needAnd)
                        {
                            ostring += " and ";
                            needAnd = false;
                        }
                        if (digits[x] == 1)
                        {
                            isTeen = true;
                        }
                        else
                        {
                            if (needComma)
                            {
                                ostring += ", ";
                                needComma = false;
                            }
                            ostring += tens[digits[x]];
                            needthousands = true;
                            needSpace = true;
                        }
                    }
                }
            }

            // Print as test
            // Would only print the digits, no formatting.
            /*for (int x = 13; x >=0; x--)
            {
                ostring += numbers[digits[x]];
            }*/
        }

        return ostring;
    }

    public void refresh(object sender, EventArgs e)
    {
        input_number.Value = "";
        output.Text = "";
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack)
        {

            try
            {
                input_number.Value.Replace(",", "");
                // If you want to put commas in, go ahead.
                // We will just ignore them, because it is not a huge deal.

                // That said, I am not handling any other character specifically, so those will error out somehow.

                if (input_number.Value.Length > 13 || (input_number.Value.Length == 13 && input_number.Value == "1000000000000"))
                    // up to 1 Trillion. Anything not a number will throw an exception
                {
                    // this does not actually detect negatives, because with numbers of this size, I kinda can't fit them in a normal int
                    // Sure, there are larget ints, but it shouldn't matter.
                    // Also, it specifically excludes anything that is larger than 1 trillion by checking length and then matching exactly.
                    output.Text = "Number out of bounds. Must be between 0 and 1 Trillion";
                }
                else
                {
                    // Parse the input number into text, and display it.
                    output.Text = parseNumber(input_number.Value);
                }
                
            }
            catch (Exception exception)
            {
                // Something went wrong.
                output.Text = exception.Message;
            }
        }
    }
}