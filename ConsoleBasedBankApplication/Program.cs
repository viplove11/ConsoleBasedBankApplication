using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO.Pipes;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleBasedBankApplication
{
    internal class Program
    {
        // global variable declaration
        string user, pass;
        long first4Digit_accNo = 0;
        long acc_no;
        double acc_balanace;
        DateTime LastWithdrawUpdate, LastDepositUpdate;

        // date time class instance
        DateTime dt = new DateTime();

        // login function 
        public bool login()
        {
            Console.WriteLine("\t\t----------------------------------LOGIN FORM-----------------------------------\n\n");
            Console.Write("\t\t\t\tEnter username: ");
            string userName = Console.ReadLine();

            Console.Write("\t\t\t\tEnter Password: ");
            string passWord = Console.ReadLine();

            Console.WriteLine("\n\n");

            if (string.Compare(userName, this.user) == 0 && string.Compare(passWord, this.pass) == 0)
            {
                Console.WriteLine("\t\t\tLogged In Successfull");
                return true;
            }
            else
            {
                Console.WriteLine("\t\t\tTry again.. wrong credentials");
                return false;
            }
            Console.WriteLine("\n\n\t\t-------------------------------------------------------------------------------");
        }


        // registration function
        public bool registration()
        {
            try
            {
                Console.WriteLine("\t\t-----------------------------Registration Form------------------------------\n\n");
                Console.Write("\t\t\t\tEnter username: ");
                string userName = Console.ReadLine();

                Console.Write("\t\t\t\tEnter password: ");
                string password = Console.ReadLine();
                Console.WriteLine("\n\n\t\t----------------------------------------------------------------------------");
                Console.Clear();
                Console.WriteLine("Generating the 8 digit Account No. ....");
                Random r = new Random();

                this.acc_no = r.Next(10000000, 99999999);
                Thread.Sleep(5000);

                this.first4Digit_accNo = this.acc_no / 10000;

                this.user = userName;
                this.pass = password;
                return true;
            }
            catch (Exception e)
            {
                Console.Write($"Exception occured : {e}");
                return false;
            }
        }


        // depositMoney function
        public void depositMoney()
        {
            Console.Clear();
            Console.Write("Enter your 8 digit Account no. XXXXXXXX : ");
            long.TryParse(Console.ReadLine(), out long Acc);
            double balance;

            Console.Clear();
            Console.WriteLine("\t\t-----------------------------DEPOSIT AMOUNT------------------------------");
            if (Acc == acc_no)
            {
                try
                {
                    Console.WriteLine("Enter the amount to deposit: ");
                    double.TryParse(Console.ReadLine(), out balance);

                    this.acc_balanace = this.acc_balanace + balance;
                    LastDepositUpdate = DateTime.Now;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            Console.WriteLine("\t\t------------------------------------------------------------------------");
        }


        // withdraw amount function
        public void withdraw()
        {
            double amount;
            long acc_no;

            Console.Clear();
            Console.WriteLine("\t\t---------------------------------WITHDRAW AMOUNT--------------------------");
            try
            {
                Console.Write($"Enter your 8 digit Account no. {this.first4Digit_accNo}XXXX: ");
                long.TryParse(Console.ReadLine(), out acc_no);

                if (this.acc_no == acc_no)
                {
                    Console.WriteLine("Enter the amount to be withdraw: ");
                    double.TryParse(Console.ReadLine(), out amount);
                    Console.Clear();
                    Console.WriteLine("\n\n*************************** Processing your transaction.... ****************************");
                    Thread.Sleep(3000);
                    if (amount <= this.acc_balanace)
                    {
                        this.acc_balanace = this.acc_balanace - amount;
                        LastWithdrawUpdate = DateTime.Now;
                        Console.WriteLine("\t\t\t\tAmount Withdrawn..");
                        Console.Clear();
                    }
                    else
                    {
                        Console.WriteLine("Low Balance!!");
                    }
                }
                Console.WriteLine("\t\t---------------------------------------------------------------------------");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        // mini statement function..
        public void miniStatement()
        {
            Console.Clear();
            try
            {
                Console.WriteLine($"Enter your 8 digit Account no. {this.first4Digit_accNo}XXXX: ");
                long.TryParse(Console.ReadLine(), out long acc);

                Console.WriteLine("Processing Your request..");
                Thread.Sleep(5000);
                Console.Clear();
                if (acc == this.acc_no)
                {
                    Console.WriteLine("\t\t\tWelcome to IDBI Bank!!\n\n");
                    Console.WriteLine($"Account Holder Name :{this.user}");
                    Console.WriteLine($"Account Number: {this.acc_no}");

                    Console.WriteLine("______________________________________________________________________________________\n\n");
                    Console.WriteLine($"\t\t\tAccount Balance : {this.acc_balanace}.00");
                    Console.WriteLine($"\t\t\tLast WithDrawn Updation : {LastWithdrawUpdate}");
                    Console.WriteLine($"\t\t\tLast Deposit Updation : {LastDepositUpdate}");

                    Console.WriteLine("__________________________________________________________________________________________");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }


        // balance enquiry function
        public void balanceEnq()
        {
            Console.Clear();
            try
            {
                Console.WriteLine($"Enter your 8 digit Account no. {this.first4Digit_accNo}XXXX: ");
                long.TryParse(Console.ReadLine(), out long acc);

                Console.WriteLine("Processing Your request..");
                Thread.Sleep(5000);
                Console.Clear();
                if (acc == this.acc_no)
                {
                    Console.WriteLine("\t\t\tWelcome to IDBI Bank!!\n\n");
                    Console.WriteLine($"Account Holder Name :{this.user}");
                    Console.WriteLine($"Account Number: {this.acc_no}");

                    Console.WriteLine("_______________________________________________________________\n\n");
                    Console.WriteLine($"\t\tAccount Balance : {this.acc_balanace}.00\n\n");
                    Console.WriteLine("_______________________________________________________________");
                }
                else
                {
                    Console.WriteLine("Invalid Account No. ...");
                    Console.Clear();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        // retrieve details function
        void retrieveDetails()
        {
            bool isCorrect = false;
            int chance = 0;
            do
            {
                Console.Clear();
                Console.WriteLine("*************************************************************************************");
                Console.Write($"\t\t\tEnter 8 digit Account No. : {this.first4Digit_accNo} XXXX: ");
                long.TryParse(Console.ReadLine(), out long num);

                Console.Write($"\t\t\tEnter Account Holder Name: ");
                string s = Console.ReadLine();
                Console.WriteLine("*************************************************************************************");
                Thread.Sleep(2000);
                Console.Clear();

                if (this.acc_no == num)
                {
                    if (this.user == s)
                    {
                        isCorrect = true;
                        Console.WriteLine("*************************************************************************************\n\n");
                        Console.WriteLine($"\t\t\tAccount Holder Name: {this.user}");
                        Console.WriteLine($"\t\t\tAccount Holder Name: {this.acc_no}");
                        Console.WriteLine($"\t\t\tAccount Password: {this.pass}\n\n");
                        Console.WriteLine("*************************************************************************************");
                        Thread.Sleep(3000);
                        break;
                    }
                }
            } while (chance < 3);


            if (isCorrect)
            {
                Console.WriteLine("Thank You!!");
            }
            else
            {
                Console.WriteLine("Invalid Details!!");
            }
        }

        // main function declaration
        public static void Main(string[] args)
        {

            Program cb = new Program();
            char ans, value;
            int option;
            do
            {
                Console.WriteLine("\t\t________________________________________________________________");
                Console.WriteLine("\t\t\t\t\tWelcome To My Bank :)\n\n");
                Console.WriteLine("\t\t1.For resgistration.");
                Console.WriteLine("\t\t2.For log In");
                Console.WriteLine("\t\t3.For Retrieve Details");
                Console.WriteLine("\t\t________________________________________________________________");
                Console.Write("\t\tEnter your choice: ");

                int.TryParse(Console.ReadLine(), out option);
                Console.Clear();
                switch (option)
                {
                    case 1:
                        if (cb.registration())
                        {
                            Console.WriteLine($"Your Generated 8 digit Account no. is : {cb.acc_no}");
                            Thread.Sleep(6000);
                        }
                        break;

                    case 2:
                        if (cb.login())
                        {
                            do
                            {
                                Console.Clear();
                                Console.Clear();
                                Console.WriteLine("---------------------------------------------------------------------------------------------------------------\n\n");
                                Console.WriteLine($"\t\t\t\t\tWelcome to IDBI Bank Portal\n\nMr. {cb.user}");
                                Console.WriteLine($"Account No. : {cb.first4Digit_accNo}XXXX\n");

                                Console.WriteLine("\t\t\t1.Deposit Money  \t\t\t2.WithDraw Money");
                                Console.WriteLine("\t\t\t3.Balance Enquiry\t\t\t4.Mini Statement");
                                Console.WriteLine("---------------------------------------------------------------------------------------------------------------\n\n");
                                
                                Console.WriteLine("Enter the valid option: ");
                                int.TryParse(Console.ReadLine(), out int val);
                                Console.Clear();
                                switch (val)
                                {
                                    case 1:
                                        cb.depositMoney();
                                        break;
                                    case 2:
                                        cb.withdraw();
                                        break;
                                    case 3:
                                        cb.balanceEnq();
                                        break;
                                    case 4:
                                        cb.miniStatement();
                                        break;
                                }
                                Console.WriteLine("Do you want to continue your transactions: (Y/N): ");
                                char.TryParse(Console.ReadLine(), out value);
                        
                            } while (value == 'Y');
                        }
                        break;
                    case 3:
                        cb.retrieveDetails();
                        break;
                    default:
                        Console.WriteLine("Invalid Option");
                        break;
                }
                Console.Clear();
                Console.WriteLine("Do you want to continue: (y / n): ");
                char.TryParse(Console.ReadLine(), out ans);
                Console.Clear();
            } while (ans == 'y');

            if (ans == 'n')
            {
                Thread.Sleep(3000);
                Console.WriteLine("************************** Thank You for Banking With Us... *****************************");
                Thread.Sleep(2000);
            }
            Console.ReadLine();
        }
    }
}