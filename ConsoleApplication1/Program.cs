using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApplication1
{
    /// <summary>
    /// Simple program showing that Visual Studio's conditional breakpoints do not have to return boolean results.
    /// 
    /// Tested with:
    /// - Visual Studio 2005
    /// - Visual Studio 2010
    /// </summary>
    public class Program
    {
        static void Main(string[] args)
        {
            var userService = new UserService();

            var userJon = new User {Id = 1, Name = "Jon Doe"};
            userService.SaveOrUpdateUser(userJon);

            var userSally = new User { Id = 2, Name = "Sally Sample" };
            userService.SaveOrUpdateUser(userSally);

            foreach (var user in userService.Users) {

                // IMPORTANT: Add a conditional breakpoint on the next line with 'user.id = 1' (instead of 'user.id == 1' or 'user.id.Equals(1)'). See doc folder for screenshots.
                int id = user.Id;
                
                // ...some logic...
                
                user.Id = id;

                // ...some more logic...

                userService.SaveOrUpdateUser(user);
            }

            // Show results of what just happened on command line
            Console.WriteLine("\n\nRESULTS==================================");
            foreach (var user in userService.Users) {
                Console.WriteLine("User Id: " + user.Id);
                Console.WriteLine("User Name: " + user.Name);
            }
            Console.WriteLine("\n\nEND RESULTS==================================");

            // Add a 'normal' breakpoint on the next line to read the console output
        }

    }

    internal class UserService
    {
        public UserService()
        {
            Users = new List<User>();
        }

        public List<User> Users { get; private set; }

        /// <summary>
        /// Imagine this method doing a database insert or update!
        /// </summary>
        public void SaveOrUpdateUser(User user)
        {
            Console.WriteLine("\tSaveOrUpdateUser...");
            Console.WriteLine("\tUser Id: " + user.Id);
            Console.WriteLine("\tUser Name: " + user.Name);
            
            User userAlreadyPresent = Users.FirstOrDefault(u => u.Name.Equals(user.Name)); // dummy find method
            
            if (userAlreadyPresent == null) {
                Console.WriteLine("Adding new user");

                Users.Add(user);    
            }
            else {
                Console.WriteLine("\nUPDATING USER.......................");
                Console.WriteLine("\tOld infos about user:");
                Console.WriteLine("\tUser id: " + userAlreadyPresent.Id);
                Console.WriteLine("\tUser name: " + userAlreadyPresent.Name);
                Console.WriteLine("\tNew infos about user:");
                Console.WriteLine("\tUser id: " + user.Id);
                Console.WriteLine("\tUser name: " + user.Name);

                userAlreadyPresent.Id = user.Id;
            }
        }
    }

    internal class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
