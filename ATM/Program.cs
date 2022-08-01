// See https://aka.ms/new-console-template for more informati
using ATM;
using System.Text.RegularExpressions;

int amount;
Console.WriteLine("Enter Card Number: ");
long cardNum = Convert.ToInt64(Console.ReadLine());
if (!Verifications.verifyCardNumber(cardNum))
{
    Console.WriteLine("Error: Invalid Card Number");
    return;
}

Console.WriteLine("Enter pin: ");
int pin = Convert.ToInt32(Console.ReadLine());


if (Verifications.verifyUser(cardNum, pin))
{
    string tablename = DBCrud.readName(cardNum) + cardNum.ToString();
    //Regex.Replace(tablename, @"\s+", "");
    tablename = tablename.Replace(" ", String.Empty);


    Console.Clear();
    Console.WriteLine("Verification");
    Console.WriteLine(tablename);
    Console.WriteLine($"\n\n\t\t\tWelcome {DBCrud.readName(cardNum)}!\n\n");
    Console.WriteLine("\n\t\t\tSELECT OPTION\n\t\t\t1. Withdraw Cash\n\t\t\t2. Check history");
    int option;
    option = Convert.ToInt32(Console.ReadLine());

    if(option == 1)
    {
        Console.Write("\n\t\tEnter amount: ");
        amount = Convert.ToInt32(Console.ReadLine());

        if (amount > DBCrud.readBalance(cardNum))
        {
            Console.WriteLine("Invalid Amount");
            return;
        }

        int newbal = DBCrud.readBalance(cardNum) - amount;
        DBCrud.updateBalance(cardNum, newbal);
        DBCrud.writeTransaction(cardNum, tablename, amount);
    
    }
    else if(option == 2)
    {
        DBCrud.readTransaction(tablename);
        Console.WriteLine("\n\n\n\n\n\n\n\t\t\t$$$$^^^^^^^~~~~~~~~~~~~~  Thank you for using our service   ~~~~~~~~~~~~~^^^^^^^$$$$");
    }
}