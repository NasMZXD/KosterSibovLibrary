using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace KosterSibovLibrary
{
    public static class Customer
    {
        static List<string> customerIds = new List<string>();
        static string dataBase = @"C:\Projects\KosterSibovLibrary\DataBase";
        static string customerDirectory = @"C:\Projects\KosterSibovLibrary\DataBase\Customers";
        public static void AddCustomerToClub(string customerId)
        {
            DoesIdExistInClub(customerId);
            customerIds.Add(customerId);
        }

        public static void DoesIdExistInClub(string customerId)
        {
            if (customerIds.Contains(customerId))
            {
                throw new IllegalIdException("This id already exists in the club!");
            }
        }

        public static void ActivateDiscount(string customerId)
        {
            CheckForCustomerExistence(customerId);
        }

        public static void RemoveCustomerFromClub(string customerId)
        {
            CheckForCustomerExistence(customerId);
            customerIds.Remove(customerId);
        }

        public static void IsIdValid(string customerId)
        {
            string idPattern = @"^\d{9}$";
            Regex IsIdValid = new Regex(idPattern);

            if (!IsIdValid.IsMatch(customerId)) throw new IllegalIdException("ID must be exaclty 9 digits!");
        }

        public static void SaveCustomer(string customerId)
        {
            CheckForDbExistence();

            if (!Directory.Exists(customerDirectory)) throw new DirectoryNotFoundException("Customers directory could not be found!1");

            string directory = @"C:\Projects\KosterSibovLibrary\DataBase\Customers\Customer " + " " + customerId;
            string fileName = "CustomerId.txt";
            Directory.CreateDirectory(directory);
            string filePath = Path.Combine(directory, fileName);
            File.WriteAllText(filePath, customerId);
        }

        public static void LoadCustomers()
        {
            CheckForDbExistence();

            if (Directory.Exists(customerDirectory))
            {
                foreach(string filePath in Directory.GetFiles(customerDirectory, "CustomerId.txt", SearchOption.AllDirectories))
                {
                    string customerId = File.ReadAllText(filePath);
                    AddCustomerToClub(customerId);
                }
            }
            else throw new DirectoryNotFoundException("Customers directory could not be found!2*");
        }

        public static void DeleteFile(string customerId)
        {
            CheckForDbExistence();

            string directory = @"C:\Projects\KosterSibovLibrary\DataBase\Customers\Customer " + " " + customerId;
            if (Directory.Exists(directory)) Directory.Delete(directory, true);
            else throw new DirectoryNotFoundException("The directory could not be found!");
        }

        public static int GetCustomersCount()
        {
            return customerIds.Count;
        }

        public static void CheckForDbExistence()
        {
            if (!Directory.Exists(dataBase)) throw new DirectoryNotFoundException("Database directory could not be found!");
        }

        public static void CheckForCustomerExistence(string customerId)
        {
            if (!customerIds.Contains(customerId))
                throw new IllegalIdException("This ID does not exist in the club!");
        }
    }
}
