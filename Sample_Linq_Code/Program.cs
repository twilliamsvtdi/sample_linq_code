using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample_Linq_Code
{
    class Program
    {
        static void Main(string[] args)
        {
            //Declaring a variable for random number generation
            Random rand = new Random();

            /*
                This section contains sample code to create a record in a table
             */
            //Declare a database context object. The name doesn't really matter
            var dbcontext = new vtdi_gate_log_dbEntities();
            try
            {
               
                //Build a record object with all the data that will be inserted. 
                //This may be hard coded or data stored in variables
                var record = new User();
                record.Email = "email@email.com";
                record.FirstName = "Test";
                record.LastName = "User";
                record.Username = "testuser1";
                record.Password = "testpassword";
                record.GenderId = rand.Next(1, 2); //randomly generating a gender id
                record.DateCreated = DateTime.Now;

                //Add the record to the list corresponding with the datatype being saved.
                dbcontext.Users.Add(record);

                //Save Changes in the data context. 
                dbcontext.SaveChanges();

                Console.Write($"New User Record added successfully. /r/n{record}");
            }
            catch (Exception)
            {
                Console.WriteLine("A very serious error has ocurred...");
                Console.ReadLine();
            }

            /*
              This section contains sample code to Update data in a table
           */
            // SingleOrDefault() will retrieve one record of null
            // The parentheses plays home to the lambda expression where
            // q => declare a variable that will store the fields that are required
            // for the condition comparisons.
            // In this case, we want to check if the Id of any record is equivalent to 1
            // user will either contain data relating to the matching record, or be 
            try
            {
                var user = dbcontext.Users.SingleOrDefault(q => q.Id == rand.Next(1,2));
                if (user != null)
                {
                    //Make the needed edits to the data
                    user.Email = "testuser@mail.com";
                    //Save Changes
                    dbcontext.SaveChanges();
                    Console.WriteLine($"Changes made to user with ID {user.Id}");
                }
                else
                {
                    Console.WriteLine("No data was returned.");
                }
            }
            catch (Exception)
            {
                Console.WriteLine("A very serious error has ocurred...");
                Console.ReadLine();
            }

            /*
                This section contains sample code to Select data in a table
            */
            try
            {
                //Retrieve one Record ;
                // Below, we will see that 
                var firstUser = dbcontext.Users.FirstOrDefault();
                Console.WriteLine($"{firstUser.FirstName}");

                //Adding a lambda condition in the () will specify what single record you wish to retrieve.
                var firstGenderUser = dbcontext.Users.FirstOrDefault(q => q.GenderId == 1);
                Console.WriteLine($"{firstGenderUser.FirstName}");

                //Youy may retrieve all records in a table and cast to a list datatype (C#'s best version of an array)
                //This is recommended as certain controls require that you cast to a list datatype

                //Get all users as a List
                var users = dbcontext.Users.ToList();
                foreach (var item in users)
                {
                    Console.WriteLine($"{item.FirstName} - {item.LastName}");
                }

                //Get all users with a gender of value (WHERE | FILTER)
                var users2 = dbcontext.Users.Where(q => q.GenderId == 2).ToList();
                foreach (var item in users2)
                {
                    Console.WriteLine($"{item.FirstName} - {item.Gender.Name}");
                }
                //Check if any record in users matches a condition. We want true or false
                var user3 = dbcontext.Users.Any(q => q.GenderId == 2);
                foreach (var item in users2)
                {
                    Console.WriteLine($"{item.FirstName} - {item.Gender.Name}");
                }

                //Check how many records are in the users table
                //You may also add a lambda expression to see how many records meet a condition
                var userCount = dbcontext.Users.Count();
                Console.WriteLine($"{userCount}");
            }
            catch (Exception)
            {
                Console.WriteLine("A very serious error has ocurred...");
                Console.ReadLine();
            }


            /*
                This section will outline you remove a record from the database
             */
            try
            {
                //Retrieve the first record that contains the word 'test' in the username
                var userToRemove = dbcontext.Users.FirstOrDefault(q => q.Username.Contains("test")); 
                if (userToRemove != null)
                {
                    //Remove the user from the Users table.
                    dbcontext.Users.Remove(userToRemove);
                    //Save Changes.
                    dbcontext.SaveChanges();
                    Console.WriteLine($"User {userToRemove.Id} was removed!");
                }
                else
                {
                    Console.WriteLine("No data was returned.");
                }
            }
            catch (Exception)
            {
                Console.WriteLine("A very serious error has ocurred...");
                Console.ReadLine();
            }
            
            Console.ReadLine();
        }

        
        
    }


}
