using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM
{
    internal class Verifications
    {
        static public bool verifyCardNumber(long x)
        {
            if (Math.Floor(Math.Log10(x) + 1) >= 9)
                return true;
            
            return false;
        }

        static public bool verifyUser(long cardNum, int pin)
        {
            if(DBCrud.readPin(cardNum) == pin)
                return true;
            
            return false ;
        }

        static public bool verifyBalance(long cardNum, int amount)
        {
            if (DBCrud.readBalance(cardNum) < amount)
                return false;
            return true;
        }
    }
}
